using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.AppServices;
using Services.QueryHandlers;
using System.Security.Claims;
using Common;
using Microsoft.AspNetCore.Http;

namespace Web.Utils
{
    public class Helper
    {
        public static string GetUserPhotoUrl(ClaimsPrincipal principal)
        {

            var claim = principal.FindFirst(x => x.Type == Constants.ProfileImage);
            return claim != null ? claim.Value : Constants.NoPhoto;
        }

        public static string GetUserId(ClaimsPrincipal principal)
        {
            var claim = principal.FindFirst(x => x.Type == ClaimTypes.PrimarySid);
            return claim != null ? claim.Value : null;
        }

        public static string GetImageUrl(string imageName)
        {
            return $"https://storage.googleapis.com/youit/site/{imageName}";
        }

        /*
         1 минуту
         2,3,4 минуты
         5,6,7,8,9,0 минут
        */


        public static string GetRelativeTime(DateTime dateTime)
        {
            TimeSpan span = DateTime.UtcNow - dateTime;
            int totalMinutes = (int)span.TotalMinutes;
            int totalHours = (int)span.TotalHours;
            int totalDays = (int)span.TotalDays;

            if (totalMinutes < 2)
            {
                return "только что";
            }
            else if (totalMinutes < 60)
            {
                if (totalMinutes >= 11 && totalMinutes <= 19)
                    return totalMinutes + " минут назад";

                int v = totalMinutes % 10;
                if (v == 0) return totalMinutes + " минут назад";
                else if (v == 1) return totalMinutes + " минуту назад";
                else if (v == 2 || v == 3 || v == 4) return totalMinutes + " минуты назад";
                else return totalMinutes + " минут назад";
            }
            else if (totalHours < 24)
            {
                if (totalHours >= 11 && totalHours <= 19) return totalHours + " часов назад";

                int v = totalHours % 10;
                if (v == 0) return " часов назад";
                else if (v == 1) return totalHours + " час назад";
                else if (v == 2 || v == 3 || v == 4) return totalHours + " часа назад";
                else return totalHours + " часов назад";
            }
            else
            {
                if (totalDays >= 11 && totalDays <= 19) return totalDays + " дней назад";

                int v = totalDays % 10;
                if (v == 0) return " дней назад";
                else if (v == 1) return totalDays + " день назад";
                else if (v == 2 || v == 3 || v == 4) return totalDays + " дня назад";
                else return totalDays + " дней назад";
            }
        }
    }
}
