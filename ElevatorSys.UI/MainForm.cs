using ElevatorSys.BLL;
using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElevatorSys.UI
{
    public partial class MainForm : Form
    {
        private List<FloorController> controllerList = new List<FloorController>();
        private List<Displayer> displayerList = new List<Displayer>();
        private Action<object, NoticeEventArgs> LocalThreadAction;

        public MainForm()
        {
            InitializeComponent();
            ElevatorService.NoticeEvent += ElevatorService_NoticeEvent;
            LocalThreadAction = ElevatorService_NoticeEvent;
        }

        private void ElevatorService_NoticeEvent(object sender, NoticeEventArgs e)
        {
            if (txtLog.InvokeRequired)
            {
                Invoke(LocalThreadAction, sender, e);
            }
            else
            {
                string msg = DateTime.Now.ToString("mm:ss fff ");
                Displayer lcd = displayerList.Find(x => x.ID == e.ElevatorID);
                #region 消息判断
                msg += e.ElevatorID > 0 ? $"【{e.ElevatorID}】号电梯 " : "";
                switch (e.Status)
                {
                    case StatusStyle.Running:
                        msg += $"运行中({e.Direction})";
                        lcd.UpdateDockMsg(e.ElevatorID,false);
                        break;
                    case StatusStyle.Arrive:
                        msg += $"到达{e.Floor}层({e.Direction})";
                        lcd.MoveTo(e.Floor);
                        lcd.SetFloorNumberAndDirection(new OutsideCall { Floor = e.Floor, Direction = e.Direction });
                        break;
                    case StatusStyle.Opening:
                        SetCallButton(ElevatorService.GetLightUpList().ToArray());
                        break;
                    case StatusStyle.Closing:
                        lcd.UpdateDockMsg(e.ElevatorID,true);
                        break;
                    case StatusStyle.Call:
                        msg += $"外呼：";
                        SetCallButton(ElevatorService.GetLightUpList().ToArray());
                        break;
                    case StatusStyle.PersonIn:
                        lcd.SetPersonCount(ElevatorService.GetPersonCount(e.ElevatorID));
                        break;
                    case StatusStyle.PersonOut:
                        lcd.SetPersonCount(ElevatorService.GetPersonCount(e.ElevatorID));
                        break;
                }
                #endregion
                msg += e.Msg;
                msg += Environment.NewLine;//换行
                txtLog.AppendText(msg);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreatePerson.Enabled = true;
            btnDispose.Enabled = true;
            btnCreate.Enabled = false;
            Building.AboveFloorCount = int.Parse(txtOverCount.Text);
            Building.UnderFloorCount = int.Parse(txtUnderCount.Text);
            ElevatorInfo.ElevatorCount = int.Parse(txtElevatorCount.Text);
            int floorCount = Building.AllFloorCount;
            int floorNum = Building.AboveFloorCount;
            Height = floorCount * ElevatorInfo.ConsoleSize.Height + 150;
            for (int i = 0; i < floorCount; i++)
            {
                controllerList.Add(new FloorController(groupBox1, floorNum, i));
                floorNum = floorNum == 1 ? -1 : floorNum - 1;
            }
            for (int i = 0; i < ElevatorInfo.ElevatorCount; i++)
            {
                displayerList.Add(new Displayer(panel2, i + 1, GetFloorPosition()));
                int initFloor = Methods.RandomValue(1, Building.AboveFloorCount);
                displayerList[i].MoveTo(initFloor);
                displayerList[i].SetFloorNumberAndDirection(new OutsideCall() { Floor = initFloor, Direction = DirectionStyle.None });
                ElevatorService.CreateDevice(i + 1, initFloor);
            }
            panel2.Width = ElevatorInfo.ElevatorCount * ElevatorInfo.LcdWidth;
            Width = groupBox1.Width + panel2.Width + 500;
            SetFormPosition();
        }

        private void btnDispose_Click(object sender, EventArgs e)
        {
            btnCreatePerson.Enabled = false;
            btnCreate.Enabled = true;
            btnDispose.Enabled = false;

            foreach (var item in controllerList)
            {
                item.Dispose();
            }
            controllerList.Clear();
            foreach (var item in displayerList)
            {
                item.Dispose();
            }
            displayerList.Clear();
            ElevatorService.ClearDevice();
            MainForm_Shown(null, null);
            SetFormPosition();
        }

        private Dictionary<int, int> GetFloorPosition()
        {
            Dictionary<int, int> pos = new Dictionary<int, int>();
            foreach (FloorController item in controllerList)
            {
                pos.Add(item.FloorNum, item.FloorNumLabel.Top);
            }
            return pos;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            panel2.Width = ElevatorInfo.LcdWidth;
            Width = groupBox1.Width + panel2.Width + 500;
            Height = 300;
            SetFormPosition();
        }

        private void btnCreatePerson_Click(object sender, EventArgs e)
        {
            List<OutsideCall> calls = PersonService.CreatePersons();
            foreach (var item in calls)
            {
                SetCallButton(item, true);
            }
        }

        private void SetCallButton(OutsideCall call, bool isLight)
        {
            var xx = controllerList.Select(x => x.FloorNum).ToList();
            FloorController myFloorView = controllerList.Where(x => x.FloorNum == call.Floor).First();
            switch (call.Direction)
            {
                case DirectionStyle.Up:
                    if (isLight)
                        myFloorView.TrunOn(1);
                    else
                        myFloorView.TurnOff(1);
                    break;
                case DirectionStyle.Down:
                    if (isLight)
                        myFloorView.TrunOn(2);
                    else
                        myFloorView.TurnOff(2);
                    break;
            }
        }

        /// <summary>
        /// 批量设置灯状态
        /// </summary>
        /// <param name="arr">亮灯数组</param>
        private void SetCallButton(OutsideCall[] arr)
        {
            var lighted = controllerList.Where(x => x.UpLight || x.DownLight).ToList();
            foreach (var item in lighted)
            {
                item.TurnOff(3);
            }
            foreach (var item in arr)
            {
                SetCallButton(new OutsideCall { Floor = item.Floor, Direction = item.Direction }, true);
            }
        }

        private void SetFormPosition()
        {
            int screenWidth = SystemInformation.PrimaryMonitorSize.Width;
            int screenHeight = SystemInformation.PrimaryMonitorSize.Height;
            Location = new Point((screenWidth - Width) / 2, (screenHeight - Height) / 2);
        }
    }
}
