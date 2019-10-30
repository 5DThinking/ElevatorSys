using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.UI
{
    class ElevatorInfo
    {
        /// <summary>
        /// 电梯数量
        /// </summary>
        public static int ElevatorCount { get; set; } = 2;
        /// <summary>
        /// 第一个楼层信息坐标
        /// </summary>
        public static System.Drawing.Point ConsoleFirstPoint { get; } = new System.Drawing.Point(25, 35);
        public static System.Drawing.Size ConsoleSize { get; } = new System.Drawing.Size(45, 30);
        public static System.Drawing.Font LcdFloorFont { get; } = new System.Drawing.Font("微软雅黑", 14);
        public static System.Drawing.Font LcdPersonFont { get; } = new System.Drawing.Font("微软雅黑", 8);
        public static System.Drawing.Font FloorViewFont { get; } = new System.Drawing.Font("微软雅黑", 10);
        public static int LcdWidth { get; } = 150;
    }
}
