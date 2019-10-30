using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.BLL
{
    public static class ElevatorService
    {
        public static List<Elevator> Devices { get; } = new List<Elevator>();
        public static List<Person> CallPersons { get; set; } = new List<Person>();

        public static event EventHandler<NoticeEventArgs> NoticeEvent;

        public static void CreateDevice(int deviceID, int currentFloor)
        {
            Elevator elev = new Elevator(deviceID);
            elev.CurrentFloor = currentFloor;
            elev.NoticeEvent += Elev_NoticeEvent;
            Devices.Add(elev);
        }

        private static void Elev_NoticeEvent(object sender, NoticeEventArgs e)
        {
            Elevator elev = (Elevator)sender;
            NoticeEvent(sender, e);
            switch (e.Status)
            {
                case StatusStyle.Arrive:
                    #region 到达：如果有下梯或上梯的则“开门”，判断是否要“调头”，是否“前进”
                    if(elev.CurrentFloor==Building.AboveFloorCount|| elev.CurrentFloor == -Building.UnderFloorCount)//电梯到顶或到底
                    {
                        elev.Direction = elev.Direction == DirectionStyle.Up ? DirectionStyle.Down : DirectionStyle.Up;//调头
                        elev.OpenDoorAsync();
                        return;
                    }
                    if (elev.PersonList.Exists(x => x.ToFloor == elev.CurrentFloor) || elev.ReachList.Exists(x => x.Direction == elev.Direction && x.Floor == elev.CurrentFloor))//有下梯或同方向上梯的
                    {
                        elev.OpenDoorAsync();
                        return;
                    }
                    if (elev.PersonList.Count == 0)
                    {
                        if (elev.ReachList.Count == 0)//无内呼 + 无外呼，则“空闲”
                        {
                            elev.Status = StatusStyle.Free;
                            elev.Direction = DirectionStyle.None;
                        }
                        else if ((elev.Direction == DirectionStyle.Up && !elev.ReachList.Exists(x => x.Floor > elev.CurrentFloor)) || (elev.Direction == DirectionStyle.Down && !elev.ReachList.Exists(x => x.Floor < elev.CurrentFloor)))
                        {
                            elev.Direction = elev.Direction == DirectionStyle.Up ? DirectionStyle.Down : DirectionStyle.Up;//调头
                            if (elev.ReachList.Exists(x => x.Floor == elev.CurrentFloor && x.Direction == elev.Direction))
                                elev.OpenDoorAsync();
                            else
                                elev.RunAsync();
                        }
                        else
                            elev.RunAsync();//前进
                    }
                    else
                    {
                        elev.RunAsync();//前进
                    }
                    #endregion
                    break;
                case StatusStyle.Opened:
                    #region 开门后：根据内呼和外呼情况进行“下梯、上梯”动作，最后“关门”
                    Func<Person[], bool> AllIn = (arrIn) =>//如果全部进入电梯则返回True，否则False
                    {
                        bool result = false;
                        if (elev.FreeSpace < arrIn.Count())
                        {
                            var tmpIn = arrIn.Take(elev.FreeSpace).ToArray();
                            CallPersons.RemoveEx(tmpIn);
                            elev.PersonIn(tmpIn);
                        }
                        else
                        {
                            CallPersons.RemoveEx(arrIn);
                            elev.PersonIn(arrIn);
                            result = true;
                        }
                        return result;
                    };
                    if (elev.PersonList.Exists(x => x.ToFloor == elev.CurrentFloor))//有下梯的
                    {
                        elev.PersonOut(elev.PersonList.Where(x => x.ToFloor == elev.CurrentFloor).ToArray());
                    }
                    if (CallPersons.Exists(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor))//有同方向上梯的
                    {
                        Person[] pIn = CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray();
                        if (!AllIn(pIn))
                            Call(CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray());
                    }
                    if (elev.PersonList.Count == 0)//梯内无人，同方向无外呼，反方向有叫梯，则转换方向，上梯
                    {
                        if (elev.Direction == DirectionStyle.Up && !elev.ReachList.Exists(x => x.Floor > elev.CurrentFloor) && elev.ReachList.Exists(x => x.Floor < elev.CurrentFloor))
                        {
                            elev.Direction = DirectionStyle.Down;//调头
                            if (CallPersons.Exists(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor))//有上梯的
                            {
                                Person[] pIn = CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray();
                                if (!AllIn(pIn))
                                    Call(CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray());
                            }
                        }
                        else if (elev.Direction == DirectionStyle.Down && !elev.ReachList.Exists(x => x.Floor < elev.CurrentFloor) && elev.ReachList.Exists(x => x.Floor > elev.CurrentFloor))
                        {
                            elev.Direction = DirectionStyle.Up;//调头
                            if (CallPersons.Exists(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor))//有上梯的
                            {
                                Person[] pIn = CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray();
                                if (!AllIn(pIn))
                                    Call(CallPersons.Where(x => x.Direction == elev.Direction && x.FromFloor == elev.CurrentFloor).ToArray());
                            }
                        }
                    }
                    elev.CloseDoorAsync();
                    #endregion
                    break;
                case StatusStyle.Closed:
                    #region 关门后: 根据内呼和外呼情况决定电梯“前进、调头、停止（空闲）”
                    if (elev.PersonList.Count > 0)//有内呼
                    {
                        elev.RunAsync();
                        return;
                    }
                    else if (elev.ReachList.Count == 0)//无内呼 + 无外呼
                    {
                        elev.Status = StatusStyle.Free;
                        elev.Direction = DirectionStyle.None;
                        return;
                    }
                    else if (elev.ReachList.Exists(x => x.Direction == elev.Direction))//无内呼 + 有外呼 + 同向
                    {
                        if (elev.Direction == DirectionStyle.Up && !elev.ReachList.Exists(x => x.Floor > elev.CurrentFloor) && elev.ReachList.Exists(x => x.Floor < elev.CurrentFloor))//电梯向上 + 上面无外呼 + 下面有外呼
                        {
                            elev.RunAsync(DirectionStyle.Down);//调头
                            return;
                        }
                        if (elev.Direction == DirectionStyle.Down && !elev.ReachList.Exists(x => x.Floor < elev.CurrentFloor) && elev.ReachList.Exists(x => x.Floor > elev.CurrentFloor))//电梯向下 + 下面无外呼 + 上面有外呼
                        {
                            elev.RunAsync(DirectionStyle.Up);//调头
                            return;
                        }
                        elev.RunAsync();
                        return;
                    }
                    else if (elev.ReachList.Exists(x => x.Direction != elev.Direction))//无内呼 + 有外呼 + 反向
                    {
                        elev.Direction = elev.Direction == DirectionStyle.Up ? DirectionStyle.Down : DirectionStyle.Up;
                        elev.RunAsync(elev.Direction);
                    }
                    #endregion
                    break;
                case StatusStyle.Opening:
                    elev.RemoveFromReachList();
                    break;
            }
        }

        public static int[] GetDockFromOutside(int elevatorID)
        {
            if (elevatorID > 0)
            {
                var device = Devices.Find(x => x.ID == elevatorID);
                var q = Devices.Find(x => x.ID == elevatorID).ReachList.Select(x => x.Floor);
                return device.Direction == DirectionStyle.Up ? q.OrderBy(x => x).ToArray() : q.OrderByDescending(x => x).ToArray();
            }
            else return new int[] { };
        }

        public static int[] GetDockFromInside(int elevatorID)
        {
            return elevatorID == 0 ? new int[] { } : Devices.Find(x => x.ID == elevatorID).PersonList.Select(x => x.ToFloor).OrderByDescending(x => x).ToArray();
        }

        public static int GetPersonCount(int elevatorID)
        {
            return Devices.Find(x => x.ID == elevatorID).PersonList.Count;
        }

        public static void Call(Person[] persons)
        {
            var pList = persons.Where(x => !CallPersons.Exists(p => p.Direction == x.Direction && p.FromFloor == x.FromFloor))
                .GroupBy(x => new { x.Direction, x.FromFloor })
                .Select(x => new OutsideCall { Floor = x.Key.FromFloor, Direction = x.Key.Direction })
                .ToList();
            CallPersons.AddRange(persons);
            foreach (var item in pList)
            {
                Elevator device = Algorithm.AssignDock(item);
                AddReachList(device, item);
            }
            string str = string.Join(",", persons.Select(x => $"{x.Name}({x.FromFloor}→{x.ToFloor})").ToArray());
            NoticeEvent(null, new NoticeEventArgs() { Status = StatusStyle.Call, Msg = str });
            StartDevices();
        }

        public static void AddReachList(Elevator device, OutsideCall call)
        {
            if (device.PersonList.Count == 0 && device.ReachList.Count == 0)
            {
                if (device.CurrentFloor == call.Floor)
                    device.Direction = call.Direction;
                else
                    device.Direction = device.CurrentFloor > call.Floor ? DirectionStyle.Down : DirectionStyle.Up;
            }
            device.ReachList.Add(call);
        }

        /// <summary>
        /// 电梯开始工作，如果有上梯或下梯的则开门，如果有内呼或外呼则前进
        /// </summary>
        public async static void StartDevices()
        {
            foreach (Elevator item in Devices)
            {
                if (item.Status != StatusStyle.Free) continue;
                if (item.PersonList.Exists(x => x.ToFloor == item.CurrentFloor) || item.ReachList.Exists(x => x.Floor == item.CurrentFloor))//有上梯或下梯的
                {
                    item.OpenDoorAsync();
                    continue;
                }
                if (item.PersonList.Count > 0)
                {
                    if (item.Direction == DirectionStyle.None)
                        item.RunAsync(item.PersonList[0].Direction);
                    else
                        item.RunAsync();
                }
                else if (item.ReachList.Count > 0)
                {
                    if (item.Direction == DirectionStyle.None)
                        item.RunAsync(item.ReachList[0].Direction);
                    else
                        item.RunAsync();
                }
                await Task.Delay(Methods.RandomValue(300, 800));
            }
        }

        public static void ClearDevice()
        {
            Devices.Clear();
            CallPersons.Clear();
        }
        /// <summary>
        /// 获得外呼亮灯列表
        /// </summary>
        /// <returns></returns>
        public static List<OutsideCall> GetLightUpList()
        {
            var openDevices = Devices.Where(x => x.Status == StatusStyle.Opening).Select(x => new { Floor = x.CurrentFloor, Direction = x.Direction }).ToList();
            return CallPersons.GroupBy(x => new { x.FromFloor, x.Direction })
                .Where(x => !openDevices.Exists(o => o.Floor == x.Key.FromFloor && o.Direction == x.Key.Direction))
                .Select(x => new OutsideCall { Floor = x.Key.FromFloor, Direction = x.Key.Direction }).ToList();
        }
    }
}
