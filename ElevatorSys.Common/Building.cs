using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    /// <summary>
    /// 楼房
    /// </summary>
    public class Building
    {
        /// <summary>
        /// 地上楼层数量
        /// </summary>
        public static int AboveFloorCount { get; set; } = 10;
        /// <summary>
        /// 地下楼层数量
        /// </summary>
        public static int UnderFloorCount { get; set; } = 3;
        /// <summary>
        /// 楼层总数
        /// </summary>
        public static int AllFloorCount { get { return AboveFloorCount + UnderFloorCount; } }
        /// <summary>
        /// 楼层列表
        /// </summary>
        public static List<int> FloorList
        {
            get
            {
                if (AllFloorCount == _FloorList.Count)
                {
                    return _FloorList;
                }
                else
                {
                    _FloorList.Clear();
                    for (int i = 0; i < AboveFloorCount; i++)
                    {
                        _FloorList.Add(i + 1);
                    }
                    for (int i = 0; i < UnderFloorCount; i++)
                    {
                        _FloorList.Add(-i - 1);
                    }
                    _FloorList = _FloorList.OrderByDescending(m => m).ToList();
                    return _FloorList;
                }
            }
        }

        private static List<int> _FloorList { get; set; } = new List<int>();
    }
}
