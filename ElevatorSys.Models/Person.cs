using ElevatorSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    /// <summary>
    /// 乘客
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public DirectionStyle Direction { get; set; }
        public int FromFloor { get; set; }
        public int ToFloor { get; set; }
        /// <summary>
        /// 状态：等梯、乘梯
        /// </summary>
        public string State { get; set; }
        public Person()
        {
            Direction = (DirectionStyle)Methods.RandomValue(3);
            if (Direction == DirectionStyle.Up)
            {
                FromFloor = Methods.RandomValue(100) <= 70 ? 1 : Building.FloorList[Methods.RandomValue(1, Building.AllFloorCount)];
                ToFloor = Building.FloorList[Methods.RandomValue(0, Building.FloorList.IndexOf(FromFloor))];
            }
            if (Direction == DirectionStyle.Down)
            {
                FromFloor = Methods.RandomValue(100) <= 70 ? 1 : Building.FloorList[Methods.RandomValue(0, Building.AllFloorCount - 1)];
                ToFloor = Building.FloorList[Methods.RandomValue(Building.FloorList.IndexOf(FromFloor) + 1, Building.AllFloorCount)];
            }
            Name = CreateName();
            State = "等梯";
        }

        private string CreateName()
        {
            string nameStr = "陈钰琪,陈研希,陈紫函,赵丽颖,杨紫,杨幂,杨容,杨曦,宋茜,宋丹丹,宋祖儿,颖儿,郑爽,盖丽丽,迪丽热巴,古力娜扎,唐嫣,唐艺昕,戚薇,谢娜,吴昕,金莎,刘亦菲,刘诗诗,刘涛,李萌萌,邓紫棋,李小露,周冬雨,刘奕儿,沈月,张天爱,佟丽娅,何穗,何泓珊,王珞丹,楚月,高晓菲,朱媛媛,朱庭辰,徐冬冬,林允,李沁,王艳,翁虹,景甜,阿悄,本兮,庄心妍,李金铭,张檬,王媛可,何洁,何曼婷,梁静茹,娄艺潇,邓家佳,赵霁,赵文琪,古丽娜扎,李菲儿,许晴,郑佩佩,张凯丽,高露,李小萌,白百合,杨颖,刘丹萌,孙羽幽,童可可,林心如,王丽坤,李小璐,周迅,高圆圆,李冰冰,范冰冰,张嘉倪,袁姗姗,孙菲菲,朱丹,王智,徐若萱,刘萌萌,李宇春,杨蓉,刘雨欣,刘心悠,陶昕然,程愫,安雅萍,郭采洁,郭碧婷,郁可唯,谢婷婷,舒畅,蔡依林,周笔畅,尚雯婕,马伊利,姚笛,卓文萱,付梦妮,秦岚,王诗安,姚贝娜,王鳞,赵子靓,叶一茜,董洁,钟欣潼,闫妮,柳岩,赵韩樱子,钟洁,吴莫愁,张靓颖,张韶涵,那英,热依扎,罗震环,张含韵,贾玲";
            List<string> nameList = nameStr.Split(',').ToList();
            return nameList[Methods.RandomValue(1, nameList.Count)];
        }
    }
}
