using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    public class NoticeEventArgs : EventArgs
    {
        public int ElevatorID { get; set; } = 0;
        public DirectionStyle Direction { get; set; } = DirectionStyle.Up;
        public StatusStyle Status { get; set; } = StatusStyle.Free;
        public string Msg { get; set; } = string.Empty;
        public int Floor { get; set; } = 1;
        public int PersonCount { get; set; } = 0;
    }
}
