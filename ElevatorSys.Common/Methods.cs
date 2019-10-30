using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElevatorSys.Common
{
    /// <summary>
    /// 运行状态
    /// </summary>
    public enum StatusStyle
    {
        Error,
        Running,
        Arrive,
        Free,
        Opening,
        Opened,
        Closing,
        Closed,
        Call,
        PersonIn,
        PersonOut,
        PersonList,
        PowerOn,
        PowerOff
    }

    public enum DirectionStyle
    {
        None,
        Up,
        Down
    }

    public static class Methods
    {
        public static int RandomValue(int min, int max)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random random = new Random(iSeed);
            return random.Next(min, max);
        }
        public static int RemoveEx<T>(this List<T> list, T[] arr)
        {
            return list.RemoveAll(x => arr.Contains(x));
        }
        public static int RandomValue(int max) => RandomValue(1, max);

        /// <summary>
        /// 扩展方法：删除空格及特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeleteBlank(this string str)
        {
            return Regex.Replace(str, @"[\s\t\r\n]", string.Empty);
        }
    }
}
