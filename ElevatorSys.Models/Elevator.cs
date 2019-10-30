using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    /// <summary>
    /// 电梯
    /// </summary>
    public class Elevator : IElevator
    {
        public int ID { get; }
        /// <summary>
        /// 电梯运行一层楼所需时间（毫秒）
        /// </summary>
        public int EachFloorTime { get; } = 2588;
        /// <summary>
        /// 电梯开关门所需时间（毫秒）
        /// </summary>
        public int OpenCloseTime { get; } = 1288;
        /// <summary>
        /// 最大承载人数
        /// </summary>
        public int MaxPerson { get; } = 10;
        /// <summary>
        /// 剩余承载人数
        /// </summary>
        public int FreeSpace { get { return MaxPerson - PersonList.Count; } }
        public DirectionStyle Direction { get; set; } = DirectionStyle.None;
        public StatusStyle Status { get; set; } = StatusStyle.Free;
        public int CurrentFloor { get; set; } = 1;
        /// <summary>
        /// 将要停靠的外呼楼层列表
        /// </summary>
        public List<OutsideCall> ReachList { get; set; } = new List<OutsideCall>();
        /// <summary>
        /// 梯内乘客列表
        /// </summary>
        public List<Person> PersonList { get; set; } = new List<Person>();
        public event EventHandler<NoticeEventArgs> NoticeEvent;

        public Elevator(int id) { ID = id; }
        private Elevator() { }

        public async void OpenDoorAsync()
        {
            Status = StatusStyle.Opening;
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Opening, Floor = CurrentFloor });
            await Task.Delay(OpenCloseTime);
            Status = StatusStyle.Opened;
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Opened, Floor = CurrentFloor });
        }

        public async void CloseDoorAsync()
        {
            Status = StatusStyle.Closing;
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Closing, Floor = CurrentFloor });
            await Task.Delay(OpenCloseTime);
            Status = StatusStyle.Closed;
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Closed, Floor = CurrentFloor });
        }

        public void SetReachList(OutsideCall[] floors)
        {
            ReachList.Clear();
            ReachList.AddRange(floors);
        }

        public void AddReachList(OutsideCall[] floors)
        {
            ReachList.AddRange(floors.Except(ReachList));
        }

        public async void RunAsync()
        {
            Status = StatusStyle.Running;
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Running, Floor = CurrentFloor });
            await Task.Delay(EachFloorTime);
            ArriveNextFloor();
        }

        public void RunAsync(DirectionStyle direction)
        {
            Direction = direction;
            RunAsync();
        }

        private void ArriveNextFloor()
        {
            switch (Direction)
            {
                case DirectionStyle.Up:
                    CurrentFloor = CurrentFloor == -1 ? 1 : CurrentFloor + 1;
                    break;
                case DirectionStyle.Down:
                    CurrentFloor = CurrentFloor == 1 ? -1 : CurrentFloor - 1;
                    break;
            }
            NoticeEvent(this, new NoticeEventArgs() { Msg = GetStatusMsg(), ElevatorID = ID, Direction = Direction, Status = StatusStyle.Arrive, Floor = CurrentFloor });
        }

        private string GetLCDInfo()
        {
            return $"{CurrentFloor}层 {Direction.ToString()} {PersonList.Count}人";
        }

        public void SendPersonsInfo()
        {
            string personList = string.Join(",", PersonList.Select(x => $"{x.Name}{x.FromFloor}→{x.ToFloor}").ToList().ToArray());
            NoticeEvent(this, new NoticeEventArgs() { ElevatorID = ID, Direction = Direction, Status = StatusStyle.PersonList, Msg = personList, Floor = CurrentFloor });
        }

        public void PersonIn(Person[] persons)
        {
            PersonList.AddRange(persons);
            string personName = string.Join(",", persons.Select(x => x.Name));
            NoticeEvent(this, new NoticeEventArgs() { ElevatorID = ID, Direction = Direction, Status = StatusStyle.PersonIn, Msg = $"[{personName}]进入【{ID}】号电梯" + GetStatusMsg(), Floor = CurrentFloor });
        }

        public void PersonOut(Person[] persons)
        {
            PersonList.RemoveAll(x => persons.Contains(x));
            string personName = string.Join(",", persons.Select(x => x.Name));
            NoticeEvent(this, new NoticeEventArgs() { ElevatorID = ID, Direction = Direction, Status = StatusStyle.PersonOut, Msg = $"[{personName}]走出【{ID}】号电梯" + GetStatusMsg(), Floor = CurrentFloor });
        }

        public void PowerOn(int floor)
        {
            CurrentFloor = floor;
            Status = StatusStyle.PowerOn;
            NoticeEvent(this, new NoticeEventArgs() { ElevatorID = ID, Direction = Direction, Status = StatusStyle.PowerOn, Msg = $"{ ID}电梯电源开启", Floor = CurrentFloor });
        }
        public void PowerOff()
        {
            Status = StatusStyle.PowerOff;
            NoticeEvent(this, new NoticeEventArgs() { ElevatorID = ID, Direction = Direction, Status = StatusStyle.PowerOff, Msg = $"{ ID}电梯电源关闭", Floor = CurrentFloor });
        }

        public string GetStatusMsg()
        {
            string str = $"状态:{Status} 人数:{PersonList.Count}";
            str += $" 内呼:{string.Join(" ", PersonList.Select(x => x.ToFloor).Distinct())}";
            str += $" 外呼:{string.Join(" ", ReachList.GroupBy(x => new { x.Floor, x.Direction }).Select(x => x.Key.Floor))}";
            return str;
        }
        /// <summary>
        /// 将当前楼层从外呼列表中清除
        /// </summary>
        public void RemoveFromReachList()
        {
            ReachList.RemoveAll(x => x.Floor == CurrentFloor && x.Direction == Direction);
        }
    }

}
