using System;

namespace SmartSchool.API.Helper
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge (this DateTime dateTime)// Método extensivel 
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if (currentDate < dateTime.AddYears(age))
                age--;

            return age;
        }
    }
}
