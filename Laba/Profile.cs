using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;

namespace Laba
{
    public class Profile
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Organisation { get; set; }
        public string Position { get; set; }
        public string Other { get; set; }

        public static Dictionary<string, int> maxLenStr = new Dictionary<string, int>();

        static Profile()
        {
            maxLenStr.Add("Name", 4);
            maxLenStr.Add("SecondName", 11);
            maxLenStr.Add("Patronymic", 0);
            maxLenStr.Add("Phone", 5);
            maxLenStr.Add("Country", 0);
            maxLenStr.Add("DateOfBirth", 10);
            maxLenStr.Add("Organisation", 0);
            maxLenStr.Add("Position", 0);
            maxLenStr.Add("Other", 0);
        }

        public Profile(string name, string secondName, string country, string phone)
        {
            this.Name = name;
            this.SecondName = secondName;
            this.Phone = phone;
            this.Country = country;
            this.Patronymic = "";
            this.Organisation = "";
            this.Position = "";
            this.Other = "";
        }

        public string GetShortInfo()
        {
            return WrapStr(this.Name, maxLenStr["Name"]) + WrapStr(this.SecondName, maxLenStr["SecondName"]) + WrapStr(this.Phone, maxLenStr["Phone"]);
        }

        public static string WrapStr(string str, int cntSymbol)
        {
            string s = "";
            
            if (str.Length == cntSymbol)
            {
                s = str;
            } else
            {
                for (int i = 0; i < (cntSymbol - str.Length) / 2; i++)
                {
                    s += " ";
                }
                s = s + str + s;
                if ((cntSymbol - str.Length) % 2 != 0)
                {
                    s += " ";
                }
            }
            s = "| " + s + " |";
            return s;
        }

        public override string ToString()
        {
            int maxLenLabel = 18;
            int maxLenData = 0;

            foreach (var item in maxLenStr)
            {
                maxLenData = item.Value > maxLenData ? item.Value : maxLenData;
            }
            string s = WrapStr("Фамилия: ", maxLenLabel) + WrapStr(this.SecondName, maxLenData) + "\n";
            s += WrapStr("Имя: ", maxLenLabel) + WrapStr(this.Name, maxLenData)  + "\n";
            s += WrapStr("Отчество: ", maxLenLabel) + WrapStr(this.Patronymic, maxLenData) + "\n";
            s += WrapStr("Номер телефона:", maxLenLabel) + WrapStr(this.Phone, maxLenData) + "\n";
            s += WrapStr("Страна: ", maxLenLabel) + WrapStr(this.Country, maxLenData) + "\n";
            s += WrapStr("Дата рождения: ", maxLenLabel) + WrapStr((this.DateOfBirth.ToString() != DateTime.MinValue.ToString() ? this.DateOfBirth.ToString("dd.MM.yyyy") : ""), maxLenData) + "\n";
            s += WrapStr("Организация: ", maxLenLabel) + WrapStr(this.Organisation, maxLenData) + "\n";
            s += WrapStr("Должность: ", maxLenLabel) + WrapStr(this.Position, maxLenData) + "\n";
            s += WrapStr("Другое: ", maxLenLabel) + WrapStr(this.Other, maxLenData) + "\n";
            return s;
        }
    }
}
