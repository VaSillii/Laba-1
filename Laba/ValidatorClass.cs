using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;

namespace Laba
{
    public class ValidatorProfileClass
    {
        private const string PatternStr = @"^[a-zA-Zа-яА-Я1-9\- _]+";
        private const string PatternPhone = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{6,10}$";

        public static string GetStrDataProfile(string message)
        {
            Console.WriteLine(message);
            while (true)
            {
                string value = Console.ReadLine();

                if (Regex.IsMatch(value, PatternStr))
                {
                    return value;
                }
                Console.WriteLine("Введенная вами строка не корректна, " +
                    "Вы можете использовать только русские и латинские буквы, цифры " +
                    "а также пробел, - и _");
            }
        }

        public static string GetPhoneProfile(string msg)
        {
            Console.WriteLine(msg);
            while (true)
            {
                string val = Console.ReadLine().Replace(" ", "");

                if (Regex.IsMatch(val, PatternPhone))
                {
                    return val;
                }
                Console.WriteLine("Введенный вами номер телефона некорректен. Повторите ввод!");
            }
        }

        public static DateTime GetDateProfile(string message)
        {
            Console.WriteLine(message);
            while (true)
            {
                string[] formats = {
                    "d/M/yyyy", "d.M.yyyy",
                    "d/MM/yyy", "d.MM.yyyy",
                    "dd/MM/yyyy", "dd.MM.yyyy",
                    "dd-MM-yyyy", "d-M-yyyy",
                    "d.M.yy", "d/M/yy",
                };

                string dateString = Console.ReadLine();

                if (!DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue))
                {
                    Console.WriteLine("Введенная дата не корректна. Повторите ввод.");
                }
                else
                {
                    return dateValue;
                }
            }
        }
    }
}
