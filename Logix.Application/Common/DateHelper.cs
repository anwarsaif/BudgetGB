using Logix.Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
namespace Logix.Application.Common
{
    public static class DateHelper
    {
        public static IMainRepositoryManager mainRepositoryManager { get; set; }
        public static void Initialize(IMainRepositoryManager _mainRepositoryManager)
        {
            mainRepositoryManager = _mainRepositoryManager;
        }
        public static ISessionHelper sessionHelper { get; set; }
        public static string DateToString(DateTime date)
        {
            return date.ToString("yyyy/MM/dd");
        }

        public static string DateToString(DateTime date, CultureInfo culture)
        {
            return date.ToString("yyyy/MM/dd", culture);
        }
        public static DateTime StringToDate1(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString) || dateString.Length != 10)
            {
                throw new ArgumentException("Invalid date string format.");
            }
            string[] dateParts = dateString.Split('/');
            if (dateParts.Length != 3)
            {
                throw new ArgumentException("Invalid date string format.");
            }

            int year, month, day;
            if (!int.TryParse(dateParts[0], out year) || dateParts[0].Length != 4 || !int.TryParse(dateParts[1], out month) || !int.TryParse(dateParts[2], out day))
            {
                throw new ArgumentException("Invalid date string format.");
            }

            return new DateTime(year, month, day);
        }

        public static DateTime StringDashToDate(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString) || dateString.Length != 10)
            {
                throw new ArgumentException("Invalid date string format.");
            }
            string[] dateParts = dateString.Split('-');
            if (dateParts.Length != 3)
            {
                throw new ArgumentException("Invalid date string format.");
            }

            int year, month, day;
            if (!int.TryParse(dateParts[0], out year) || dateParts[0].Length != 4 || !int.TryParse(dateParts[1], out month) || !int.TryParse(dateParts[2], out day))
            {
                throw new ArgumentException("Invalid date string format.");
            }

            return new DateTime(year, month, day);
        }
        public static DateTime StringToDate2(string dateString)
        {
            DateTime parsedDate;
            string[] formats = { "yyyy/MM/dd", "MM/dd/yyyy", "dd/MM/yyyy" };

            if (!DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                throw new ArgumentException("Invalid date string format.");
            }

            return parsedDate;
        }

        public static async Task<string> DateFormattYYYYMMDD_H_G(string DateH)
        {
            try
            {
                var getDate = await mainRepositoryManager.SysCalendarRepository.GetOne(x => x.GDate, x => x.HDate == DateH);
                return getDate ?? "";
            }
            catch (Exception)
            {
                return "Error In DateFormattYYYYMMDD_H_G Function.";
            }
        }

        public static string DateFormattYYYYMMDD_H_G2(string dateH)
        {
            string year = dateH.Substring(0, 4);
            string month = dateH.Substring(5, 2);
            string day = dateH.Substring(8, 2);

            if ((day == "30" && month == "06") || (day == "30" && month == "02") || (day == "30" && month == "04") ||
                (day == "30" && month == "08") || (day == "30" && month == "10") || (day == "31" && month == "10") ||
                (day == "30" && month == "12") || (day == "31" && month == "12") || (day == "30" && month == "11"))
            {
                day = "29";
            }

            string newDateHi = $"{year}/{day}/{month}";

            DateTime newDate = DateTime.ParseExact(newDateHi, "yyyy/dd/MM", CultureInfo.InvariantCulture);

            return $"{newDate.Year}/{newDate.Month.ToString("00")}/{newDate.Day.ToString("00")}";
        }

        public static async Task<string> DateFormattYYYYMMDD_G_H(string DateG)
        {
            try
            {
                var getDate = await mainRepositoryManager.SysCalendarRepository.GetOne(x => x.HDate, x => x.GDate == DateG);
                return getDate ?? "";
            }
            catch (Exception)
            {
                return "Error In DateFormattYYYYMMDD_G_H Function.";
            }
        }
        public static string DateFormattYYYYMMDD_G_H2(string dateG)
        {
            // Splitting the date string into year, month, and day components
            string[] parts = dateG.Split('/');

            if (parts.Length != 3)
                throw new ArgumentException("Invalid date format");

            string year = parts[0];
            string month = parts[1];
            string day = parts[2];

            // Adjusting the day if necessary
            if ((day == "30" && (month == "06" || month == "02" || month == "04" || month == "08" || month == "10" || month == "12")) ||
                (day == "31" && (month == "10" || month == "12")) ||
                (day == "30" && month == "11"))
            {
                day = "29";
            }

            // Formatting and returning the date string
            return $"{year}/{month.PadLeft(2, '0')}/{day.PadLeft(2, '0')}";
        }

        /// <summary>
        /// to use it you must call this:::
        //DateHelper.Initialize(mainRepositoryManager);

        /// </summary>

        /// <returns></returns>
        public async static Task<int> DateDiff_day2(string SDate, string EDate)
        {
            try
            {
                var getData = await mainRepositoryManager.SysCalendarRepository.GetAll(x => x.GDate != null && (x.GDate.Contains(SDate.Substring(0, 4)) || x.GDate.Contains(EDate.Substring(0, 4))));
                var getDatacOUNT = getData.AsEnumerable()
                    .Where(x => x.GDate != null && DateHelper.StringToDate1(x.GDate) >= DateHelper.StringToDate1(SDate) && DateHelper.StringToDate1(x.GDate) <= DateHelper.StringToDate1(EDate)
                             );
                return getDatacOUNT.Count();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int DateDiff_day(string oldDate, string newDate)
        {
            int d1 = int.Parse(oldDate.Substring(8, 2));
            int m1 = int.Parse(oldDate.Substring(5, 2));
            int y1 = int.Parse(oldDate.Substring(0, 4));
            int d2 = int.Parse(newDate.Substring(8, 2));
            int m2 = int.Parse(newDate.Substring(5, 2));
            int y2 = int.Parse(newDate.Substring(0, 4));

            int years = y2 - y1;

            if (m2 < m1)
            {
                years--;
                int months = (m2 + 12) - m1;
                if (d2 < d1)
                {
                    months--;
                    int days = (d2 + 30) - d1;
                    return days + (months * 30) + (years * 360);
                }
                else
                {
                    int days = d2 - d1;
                    return days + (months * 30) + (years * 360);
                }
            }
            else
            {
                int months = m2 - m1;
                if (d2 < d1)
                {
                    months--;
                    int days = (d2 + 30) - d1;
                    return days + (months * 30) + (years * 360);
                }
                else
                {
                    int days = d2 - d1;
                    return days + (months * 30) + (years * 360);
                }
            }
        }

        public static async Task<bool> CheckDate(string curDate)
        {
            bool ret = false;
            try
            {
                //------------------------------------نوع التقويم المعتمد
                string CalendarType = "0";
                var Calendar = await mainRepositoryManager.SysPropertyValueRepository.GetByProperty(19, sessionHelper.FacilityId);
                if (Calendar.PropertyValue != null)
                {
                    CalendarType = Calendar.PropertyValue;
                }


                int year = int.Parse(curDate.Substring(0, 4));
                if (CalendarType == "1")
                {
                    if (year >= 1900 && year <= 2100)
                        ret = true;
                    else
                        return false;
                }
                else
                {
                    if (year >= 1300 && year <= 1500)
                        ret = true;
                    else
                        return false;
                }

                int month = int.Parse(curDate.Substring(5, 2));
                if (month < 1 || month > 12)
                    return false;

                int day = int.Parse(curDate.Substring(8, 2));
                if (day < 1 || day > 31)
                    return false;

                if (curDate[4] != '/' || curDate[7] != '/')
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid date specified", ex);
            }

            return ret;
        }

        public static string? FixConvertDateFormate(string str_date)
        {
            DateTime date;
            if (
                DateTime.TryParseExact(str_date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)

           )
            {


            }
            else
            {
                return null;
            }



            return date.ToString("yyyy/MM/dd");
        }
        public static string GetMonthName(int month)
        {
            try
            {
                if (month < 1 || month > 12)
                {

                    throw new ArgumentException($"Invalid Month Number");
                }
                switch (month)
                {
                    case 1:
                        return "January";
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                    default:
                        throw new ArgumentException($"Invalid Month Number");

                }
            }

            catch (Exception exp)
            {

                throw new ArgumentException($"Exception in : {exp.Message.ToString()}");
            }
        }

        public static string? FixConvertDateFormateToDash(string str_date)
        {
            DateTime date;
            if (
                DateTime.TryParseExact(str_date, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)
                || DateTime.TryParseExact(str_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)

           )
            {


            }
            else
            {
                return null;
            }

            return date.ToString("yyyy-MM-dd");
        }
    }
}

