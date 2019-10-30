using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSys.UI
{
    public class FloorController : IDisposable
    {
        public Label FloorNumLabel { get; set; }
        public Label UpLabel { get; set; }
        public Label DownLabel { get; set; }
        /// <summary>
        /// 楼层号
        /// </summary>
        public int FloorNum { get; }
        /// <summary>
        /// 列表索引
        /// </summary>
        public int Index { get; set; }
        public bool UpLight { get; set; }
        public bool DownLight { get; set; }

        private FloorController() { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent">父控件</param>
        /// <param name="floorNum">楼层号</param>
        /// <param name="index">列表索引</param>
        public FloorController(Control parent, int floorNum, int index)
        {
            FloorNumLabel = new Label();
            FloorNumLabel.Parent = parent;
            FloorNumLabel.Location = new Point(ElevatorInfo.ConsoleFirstPoint.X, ElevatorInfo.ConsoleFirstPoint.Y + index * ElevatorInfo.ConsoleSize.Height);
            FloorNumLabel.BorderStyle = BorderStyle.FixedSingle;
            FloorNumLabel.Font = ElevatorInfo.FloorViewFont;
            FloorNumLabel.Size = ElevatorInfo.ConsoleSize;
            FloorNumLabel.ForeColor = Color.Gray;
            FloorNumLabel.Text = string.Format("{0,3}层", floorNum);
            FloorNumLabel.TextAlign = ContentAlignment.MiddleCenter;
            FloorNum = floorNum;
            Index = index;

            UpLabel = new Label();
            UpLabel.Parent = parent;
            UpLabel.Location = new Point(ElevatorInfo.ConsoleFirstPoint.X + ElevatorInfo.ConsoleSize.Width + 5, ElevatorInfo.ConsoleFirstPoint.Y + index * ElevatorInfo.ConsoleSize.Height);
            UpLabel.BorderStyle = BorderStyle.FixedSingle;
            UpLabel.Font = ElevatorInfo.FloorViewFont;
            UpLabel.Size = new Size(ElevatorInfo.ConsoleSize.Width, ElevatorInfo.ConsoleSize.Height);
            UpLabel.ForeColor = Color.Gray;
            UpLabel.Text = "↑";
            UpLabel.TextAlign = ContentAlignment.MiddleCenter;

            DownLabel = new Label();
            DownLabel.Parent = parent;
            DownLabel.Location = new Point(ElevatorInfo.ConsoleFirstPoint.X + ElevatorInfo.ConsoleSize.Width * 2 + 10, ElevatorInfo.ConsoleFirstPoint.Y + index * ElevatorInfo.ConsoleSize.Height);
            DownLabel.BorderStyle = BorderStyle.FixedSingle;
            DownLabel.Font = ElevatorInfo.FloorViewFont;
            DownLabel.Size = new Size(ElevatorInfo.ConsoleSize.Width, ElevatorInfo.ConsoleSize.Height);
            DownLabel.ForeColor = Color.Gray;
            DownLabel.Text = "↓";
            DownLabel.TextAlign = ContentAlignment.MiddleCenter;
        }
        /// <summary>
        /// 按下叫梯键，灯亮
        /// </summary>
        /// <param name="lightIndex">1：向上 2：向下 3：全亮</param>
        public void TrunOn(int lightIndex)
        {
            switch (lightIndex)
            {
                case 1:
                    UpLight = true;
                    UpLabel.BackColor = Color.Red;
                    break;
                case 2:
                    DownLight = true;
                    DownLabel.BackColor = Color.Red;
                    break;
                default:
                    UpLight = true;
                    DownLight = true;
                    UpLabel.BackColor = Color.Red;
                    DownLabel.BackColor = Color.Red;
                    break;
            }
        }
        /// <summary>
        /// 电梯到达时灯熄灭
        /// </summary>
        /// <param name="lightIndex">1：向上 2：向下 3：全熄灭</param>
        public void TurnOff(int lightIndex)
        {
            switch (lightIndex)
            {
                case 1:
                    UpLight = false;
                    UpLabel.BackColor = UpLabel.Parent.BackColor;
                    break;
                case 2:
                    DownLight = false;
                    DownLabel.BackColor = UpLabel.Parent.BackColor;
                    break;
                default:
                    UpLight = false;
                    DownLight = false;
                    UpLabel.BackColor = UpLabel.Parent.BackColor;
                    DownLabel.BackColor = UpLabel.Parent.BackColor;
                    break;
            }
        }

        public void Dispose()
        {
            FloorNumLabel.Dispose();
            UpLabel.Dispose();
            DownLabel.Dispose();
        }
    }
}
