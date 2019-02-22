using System;
using System.Collections.Generic;
using System.Text;

namespace DataType
{
    public class DataTimeHelper
    {
        public static void GetLastMonth()
        {
            DateTime currentTime = DateTime.Now.Date;
            DateTime EndTime = currentTime.AddDays(-currentTime.Day + 1);
            DateTime StartTime = EndTime.AddMonths(-1);

            Console.WriteLine($"上月开始时间：{StartTime}，上月结束时间：{EndTime}");
        }
    }
}
