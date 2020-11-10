using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Laba
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Profile> list = new List<Profile>();

            while (true)
            {
                Console.Clear();
                ActionsBase.ListAction();
                Console.Write("Введите команду: ");

                if (!Int32.TryParse(Console.ReadLine(), out int val) && (val < 1 || val > 6))
                {
                    continue;
                }
                else if (val == 6)
                {
                    break;
                }

                switch (val)
                {
                    case 1:
                        list.Add(ActionsBase.CreateProfile());
                        break;
                    case 2:
                        ActionsBase.EditEntryProfile(ref list);
                        break;
                    case 3:
                        ActionsBase.RemoveEntryFromArr(ref list);
                        break;
                    case 4:
                        ActionsBase.PrintOneEntry(list);
                        break;
                    case 5:
                        ActionsBase.PrintAllEnries(list);
                        break;
                }

            }
        }
    }
}
