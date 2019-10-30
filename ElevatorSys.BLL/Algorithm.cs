using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.BLL
{
    public class Algorithm
    {
        /// <summary>
        /// 指派某个电梯到外呼楼层
        /// </summary>
        /// <param name="callFloor">外呼楼层</param>
        /// <param name="type">方向</param>
        public static Elevator AssignDock(OutsideCall outCall)
        {
            //List<decimal> directionWeight = DirectionWeight(type);
            List<decimal> carryWeight = CarryWeight();
            List<decimal> waitTimeWeight = WaitTimeWeight(outCall);
            List<decimal> dockWeight = DockWeight();
            List<decimal> values = new List<decimal>();
            for (int i = 0; i < carryWeight.Count; i++)
            {
                values.Add(waitTimeWeight[i] + dockWeight[i] + dockWeight[i]);
            }
            return ElevatorService.Devices[values.IndexOf(values.Max())];
        }

        /// <summary>
        /// 权重:方向   同向10分 空闲5分 逆向1分
        /// </summary>
        /// <param name="type">外呼方向</param>
        /// <returns></returns>
        private static List<decimal> DirectionWeight(DirectionStyle type)
        {
            List<decimal> result = new List<decimal>();
            for (int i = 0; i < ElevatorService.Devices.Count; i++)
            {
                if (ElevatorService.Devices[i].Direction == DirectionStyle.None)
                {
                    result.Add(5);
                }
                else
                {
                    result.Add(ElevatorService.Devices[i].Direction == type ? 10 : 1);
                }
            }
            return result;
        }

        /// <summary>
        /// 权重:轿厢承载 按百分比，最高10分
        /// </summary>
        /// <returns></returns>
        private static List<decimal> CarryWeight()
        {
            List<decimal> result = new List<decimal>();
            for (int i = 0; i < ElevatorService.Devices.Count; i++)
            {
                result.Add(Convert.ToDecimal(10 * (1 - ElevatorService.Devices[i].PersonList.Count / ElevatorService.Devices[i].MaxPerson)));
            }
            return result;
        }

        /// <summary>
        /// 权重:外呼等待时间    计算每个电梯到达所需时间,再按比例计算，最高10分
        /// </summary>
        /// <param name="callFloor">外呼楼层</param>
        /// <param name="dire">外呼方向</param>
        /// <returns></returns>
        private static List<decimal> WaitTimeWeight(OutsideCall outsideCall)
        {
            List<decimal> result = new List<decimal>();
            for (int i = 0; i < ElevatorService.Devices.Count; i++)
            {
                Common.Elevator elev = ElevatorService.Devices[i];
                if (elev.MaxPerson == elev.PersonList.Count) //满员
                {
                    result.Add(-100000);
                    continue;
                }
                int needTime = 0;
                if (elev.Direction == DirectionStyle.None)
                {
                    needTime = Math.Abs(elev.CurrentFloor - outsideCall.Floor) * elev.EachFloorTime;
                }
                else if (elev.Direction == outsideCall.Direction)//同向
                {
                    //判断是否包含
                    if (elev.Direction == DirectionStyle.Up)
                    {
                        if (outsideCall.Floor < elev.CurrentFloor)//外呼在下面，需要到最顶端折返回来
                        {
                            int max = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Max();//内呼+外呼，求最大值
                            int num = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Count();
                            needTime = (2 * max - elev.CurrentFloor - outsideCall.Floor) * elev.EachFloorTime + 2 * elev.OpenCloseTime * num;
                        }
                        else //外呼在中间或上面
                        {
                            needTime = (outsideCall.Floor - elev.CurrentFloor) * elev.EachFloorTime + elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Where(x => x.Direction == DirectionStyle.Up).Select(x => x.Floor)).Count(x => x < outsideCall.Floor) * 2 * elev.OpenCloseTime;
                        }
                    }
                    else
                    {
                        if (outsideCall.Floor < elev.CurrentFloor)//外呼在上面，需要到最底端折返回来
                        {
                            int min = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Min();
                            int num = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Count();
                            needTime = (outsideCall.Floor + elev.CurrentFloor - 2 * min) * elev.EachFloorTime + 2 * elev.OpenCloseTime * num;
                        }
                        else//外呼在中间或下面
                        {
                            needTime = Math.Abs(outsideCall.Floor - elev.CurrentFloor) * elev.EachFloorTime + elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Where(x => x.Direction == DirectionStyle.Down).Select(x => x.Floor)).Count(x => x > outsideCall.Floor) * 2 * elev.OpenCloseTime;
                        }
                    }
                }
                else//逆向
                {
                    int max = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Max();
                    int min = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Min();
                    needTime = (2 * (max - min) + Math.Abs(elev.CurrentFloor - outsideCall.Floor)) * elev.EachFloorTime + elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Select(x => x.Floor)).Count() * 2 * elev.OpenCloseTime;
                }
                result.Add(needTime);
            }
            decimal sum = result.Sum();
            for (int i = 0; i < result.Count; i++)
            {
                result[i] = 10 * (1 - result[i] / sum);
            }
            return result;
        }

        /// <summary>
        /// 权重:停靠次数 每增加一次停靠则减1.5分，最高10分，最低0分
        /// </summary>
        /// <returns></returns>
        private static List<decimal> DockWeight()
        {
            List<decimal> result = new List<decimal>();
            for (int i = 0; i < ElevatorService.Devices.Count; i++)
            {
                Elevator elev = ElevatorService.Devices[i];
                int count = elev.PersonList.Select(x => x.ToFloor).Union(elev.ReachList.Where(x => x.Direction == elev.Direction).Select(x => x.Floor)).Count();
                decimal value = 10 - Convert.ToDecimal(1.5 * count);
                result.Add(value < 0 ? 0 : value);
            }
            return result;
        }
    }
}
