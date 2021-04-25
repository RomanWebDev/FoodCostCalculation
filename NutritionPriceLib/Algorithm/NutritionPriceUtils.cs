using System;
using System.Collections.Generic;

namespace NutritionPriceLib.Algorithm
{
    public class NutritionPriceUtils
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimeStamp(DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeSeconds();
        }

        /// <summary>This method generates a sequence of working days. 
        ///   You can specify whether to include weekends or not
        ///     <example>For example:
        ///         <code>
        ///             List<double> days = NutritionPriceUtils.GenerateWorkingDays(new DateTime(2021, 05, 1), new DateTime(2021, 05, 31));
        ///             var algorithm = new NutritionPriceAlgorithm(days, 200.0);
        ///         </code>
        ///     </example>
        /// </summary>
        public static List<double> GenerateWorkingDays(DateTime FromDate, DateTime ToDate, bool ExceptWeekend = true)
        {
            return GenerateWorkingDays(DateTimeToUnixTimeStamp(FromDate), DateTimeToUnixTimeStamp(ToDate), ExceptWeekend);
        }

        /// <summary>This method generates a sequence of working days. 
        ///   You can specify whether to include weekends or not
        ///     <example>For example:
        ///         <code>
        ///             List<double> days = NutritionPriceUtils.GenerateWorkingDays(1619816400, 1622408400);
        ///             var algorithm = new NutritionPriceAlgorithm(days, 200.0);
        ///         </code>
        ///     </example>
        /// </summary>
        public static List<double> GenerateWorkingDays(double FromDate, double ToDate, bool ExceptWeekend = true)
        {
            var result = new List<double>();

            for (var dt = UnixTimeStampToDateTime(FromDate); dt <= UnixTimeStampToDateTime(ToDate); dt = dt.AddDays(1))
            {
                if (ExceptWeekend && !IsWeekend(dt.DayOfWeek))
                {
                    result.Add(DateTimeToUnixTimeStamp(dt));
                    continue;
                }

                if (!ExceptWeekend)
                {
                    result.Add(DateTimeToUnixTimeStamp(dt));
                }
            }

            return result;
        }

        public static bool IsWeekend(DayOfWeek dayToday)
        {
            return (dayToday == DayOfWeek.Saturday) || (dayToday == DayOfWeek.Sunday);
        }
    }
}
