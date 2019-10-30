using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    interface IElevator
    {
        void RunAsync(DirectionStyle direction);
        void OpenDoorAsync();
        void CloseDoorAsync();
        void PowerOn(int floor);
        void PowerOff();
        void SetReachList(OutsideCall[] floors);
        void AddReachList(OutsideCall[] floors);
        void PersonIn(Person[] persons);
        void PersonOut(Person[] persons);
        void SendPersonsInfo();
    }
}
