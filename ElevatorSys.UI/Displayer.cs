using ElevatorSys.BLL;
using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSys.UI
{
    public class Displayer : IDisposable
    {
        public int ID { get; }
        private GroupBox LCDGroupBox { get; set; }
        private Panel LcdPanel { get; set; }
        private Label LcdFloor { get; set; }
        private Label LcdDockFromInside { get; set; }
        public Label LcdPerson { get; set; }
        private Label LcdDockFromOutside { get; set; }
        public string Msg { get; set; } = "电梯内无人！";
        public ToolTip MsgTool { get; set; }
        Dictionary<int, int> FloorPosition;
        public Displayer(Panel panel, int id, Dictionary<int, int> pos)
        {
            ID = id;
            LCDGroupBox = new GroupBox();
            LCDGroupBox.Text = $"【{ID}号】电梯";
            LCDGroupBox.Width = ElevatorInfo.LcdWidth;
            LCDGroupBox.Parent = panel;
            LCDGroupBox.Dock = DockStyle.Left;

            LcdPanel = new Panel();
            LcdPanel.Parent = LCDGroupBox;
            LcdPanel.Width = LCDGroupBox.Width - 2;
            LcdPanel.Height = ElevatorInfo.ConsoleSize.Height;
            LcdPanel.Left = 1;
            LcdPanel.BackColor = System.Drawing.Color.Black;
            LcdPanel.Font = ElevatorInfo.LcdFloorFont;

            LcdFloor = new Label();
            LcdFloor.Parent = LcdPanel;
            LcdFloor.Dock = DockStyle.Left;
            LcdFloor.Width = 50;
            LcdFloor.ForeColor = System.Drawing.Color.Red;
            LcdFloor.Text = "01 ↓";
            LcdFloor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            LcdDockFromInside = new Label();
            LcdDockFromInside.Parent = LcdPanel;
            LcdDockFromInside.Left = LcdFloor.Width + 5;
            LcdDockFromInside.Top = 0;
            LcdDockFromInside.Height = Convert.ToInt32(LcdPanel.Height / 2);
            LcdDockFromInside.ForeColor = System.Drawing.Color.Fuchsia;
            LcdDockFromInside.Text = "停靠";
            LcdDockFromInside.Font = ElevatorInfo.LcdPersonFont;
            LcdDockFromInside.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            LcdPerson = new Label();
            LcdPerson.Parent = LcdPanel;
            LcdPerson.Height = LcdDockFromInside.Height;
            LcdPerson.Left = LcdDockFromInside.Left;
            LcdPerson.Top = LcdDockFromInside.Height;
            LcdPerson.ForeColor = System.Drawing.Color.Gold;
            LcdPerson.Text = "0人";
            LcdPerson.AutoSize = true;
            LcdPerson.Font = LcdDockFromInside.Font;
            LcdPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            LcdDockFromOutside = new Label();
            LcdDockFromOutside.Parent = LcdPanel;
            LcdDockFromOutside.ForeColor = System.Drawing.Color.LightSkyBlue; ;
            LcdDockFromOutside.Text = "外呼";
            LcdDockFromOutside.Font = LcdDockFromInside.Font;
            LcdDockFromOutside.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            LcdDockFromOutside.Top = LcdPerson.Top;
            LcdDockFromOutside.Left = LcdPerson.Left + LcdPerson.Width + 5;
            LcdDockFromOutside.Height = LcdPerson.Height;

            MsgTool = new ToolTip();
            MsgTool.InitialDelay = 200;
            MsgTool.AutoPopDelay = 10 * 800;
            MsgTool.ReshowDelay = 200;
            MsgTool.ShowAlways = true;
            MsgTool.IsBalloon = true;
            MsgTool.SetToolTip(LcdFloor, Msg);
            MsgTool.SetToolTip(LcdDockFromInside, Msg);
            FloorPosition = pos;
            MoveTo(1);
        }
        /// <summary>
        /// 更新内呼和外呼信息
        /// </summary>
        /// <param name="elevatorID"></param>
        internal void UpdateDockMsg(int elevatorID, bool isClosing)
        {
            if (isClosing || LcdDockFromInside.Text.DeleteBlank() == "停靠") SetDockFromInside(ElevatorService.GetDockFromInside(elevatorID));
            if (isClosing || LcdDockFromOutside.Text.DeleteBlank() == "外呼") SetDockFromOutside(ElevatorService.GetDockFromOutside(elevatorID));
        }

        public void MoveTo(int floor)
        {
            LcdPanel.Top = FloorPosition[floor];
        }

        public void SetFloorNumberAndDirection(OutsideCall call)
        {
            string[] str = new string[] { "　", "↑", "↓" };
            LcdFloor.Text = $"{call.Floor,2} {str[(int)call.Direction]}";
        }

        public void SetDockFromInside(int[] floors)
        {
            LcdDockFromInside.Text = $"停靠 {string.Join(" ", floors.Distinct())}";
        }

        public void SetPersonCount(int count)
        {
            LcdPerson.Text = $"{count}人";
        }

        public void SetDockFromOutside(int[] floors)
        {
            LcdDockFromOutside.Text = $"外呼 {string.Join(" ", floors.Distinct())}";
        }

        public void Dispose()
        {
            foreach (Control item in LcdPanel.Controls)
            {
                item.Dispose();
            }
            LcdPanel.Dispose();
            MsgTool.Dispose();
            LCDGroupBox.Dispose();
        }
    }
}
