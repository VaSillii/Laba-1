using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Laba
{
    public class ActionsBase
    {
        public static void ListAction()
        {
            Console.WriteLine("Достпупные команды:");
            Console.WriteLine("1 - Создание новой записи");
            Console.WriteLine("2 - Редактирование существующей записи");
            Console.WriteLine("3 - Удаление созданной записи");
            Console.WriteLine("4 - Просмотр созданной записи");
            Console.WriteLine("5 - Просмогтр всех созданных записей");
            Console.WriteLine("6 - Завершить программу!\n");
        }

        public static Profile CreateProfile()
        {
            Console.Clear();
            Console.WriteLine("Создание новой записи!!");

            string name = ValidatorProfileClass.GetStrDataProfile("Введите имя контакта:");
            string secondName = ValidatorProfileClass.GetStrDataProfile("Введите фамилию:");
            string phone = ValidatorProfileClass.GetPhoneProfile("Введите номер телефона:");
            string country = ValidatorProfileClass.GetStrDataProfile("Введите название страны:");

            Profile profile = new Profile(name, secondName, country, phone);

            while (true)
            {
                Console.WriteLine("Вы хотите заполнить остальные поля? (y/n)");
                string answerUser = Console.ReadLine();
                if (answerUser.ToLower().Equals("y"))
                {
                    profile.Patronymic = ValidatorProfileClass.GetStrDataProfile("Введите отчество контакта:");
                    profile.DateOfBirth = ValidatorProfileClass.GetDateProfile("Введите дату рождения:");
                    profile.Organisation = ValidatorProfileClass.GetStrDataProfile("Введите название организации контакта:");
                    profile.Position = ValidatorProfileClass.GetStrDataProfile("Введите должность контакта:");
                    profile.Other = ValidatorProfileClass.GetStrDataProfile("Введите дополнительную информацию о пользователе:");
                    break;
                }
                else if (answerUser.ToLower().Equals("n"))
                {
                    break;
                } else
                {
                    Console.WriteLine("Некорректное значение. Повторите ввод!");
                }

            }
            UpdateLenArrStr(profile);
            return profile;
        }

        public static void PrintOneEntry(List<Profile> list)
        {
            while (true)
            {
                if (PrintAllDataWithId(list))
                {
                    Console.Write("\nВведите номер записи для детального просмотра или -1 для выхода в главное меню: ");

                    string answerUser = Console.ReadLine();
                    if (!int.TryParse(answerUser, out int result) || String.IsNullOrEmpty(answerUser) || (result < -1 || result > list.Count - 1))
                    {
                        Console.WriteLine("Введенное значение некорректно! Повторте ввод!");
                        continue;
                    }
                    if (result == -1)
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Подробная информация записи {result}: \n");
                        Console.WriteLine(list[result] + "\n");
                        Console.WriteLine("Для продолжения нажмите на любую клавишу!");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("\nДля выхода в меню нажмите любую клавишу!");
                    Console.ReadLine();
                    break;
                }
            }
        }


        private static bool PrintAllDataWithId(List<Profile> list)
        {
            Console.Clear();
            Console.WriteLine("Список контактов:");
            if (list.Count != 0)
            {
                int lenId = list.Count.ToString().Length;
                Console.WriteLine(Profile.WrapStr("№", list.Count.ToString().Length) + Profile.WrapStr("Name", Profile.maxLenStr["Name"]) + Profile.WrapStr("Second name", Profile.maxLenStr["SecondName"]) + Profile.WrapStr("Phone", Profile.maxLenStr["Phone"]));
                
                for (int i = 0; i < list.Count; i++) {
                    string baseStr = (string)(Profile.WrapStr(i.ToString(), lenId) + list[i].GetShortInfo());

                    for (int j = 0; j < baseStr.Length; j++)
                    {
                        Console.Write("-" + (j== baseStr.Length-1? "\n":""));
                    }
                    Console.Write(baseStr + "\n");
                }
                return true;
            }
            else
                Console.WriteLine("Список контактов пуст! Добавьте записи!");
            return false;
        }

        public static void PrintAllEnries(List<Profile> list)
        {
            ActionsBase.PrintAllDataWithId(list);
            Console.WriteLine("Для выхода в меню нажмите любую клавишу!");
            Console.ReadLine();
        }

        public static void EditEntryProfile(ref List<Profile> list)
        {
            while (true)
            {
                if (!ActionsBase.PrintAllDataWithId(list))
                {
                    Console.ReadLine();
                    break;
                }

                Console.Write("Введите номер записи для редактирования или -1 для отмены операции редактирования и выхода в меню: ");

                string userAnswer = Console.ReadLine();

                try
                {
                    int indexData = int.Parse(userAnswer);
                    if (indexData == -1)
                    {
                        break;
                    }
                    list[indexData] = ActionsBase.EditProfile(list[indexData]);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nВведенное значение некорректно. Повторите ввод!");
                    continue;
                }
                                
            }
        }

        private static Profile EditProfile(Profile profile)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Редактирование записи");
                Console.WriteLine(profile);

                Console.WriteLine("Выберите команду, для редактирования поля в профиле:");
                Console.WriteLine("1 - Имя");
                Console.WriteLine("2 - Фамилия");
                Console.WriteLine("3 - Отчество");
                Console.WriteLine("4 - Номер телефона");
                Console.WriteLine("5 - Страна");
                Console.WriteLine("6 - Дата рождения");
                Console.WriteLine("7 - Организация");
                Console.WriteLine("8 - Позиция");
                Console.WriteLine("9 - Другое");
                Console.WriteLine("10 - Завершить редактирование");


                if (int.TryParse(Console.ReadLine(), out int cmd) && (cmd < 1 || cmd >10))
                {
                    continue;
                } else if (cmd == 10)
                {
                    break;
                }

                switch (cmd)
                {
                    case 1:
                        profile.Name = ValidatorProfileClass.GetStrDataProfile("Введите имя контакта:");
                        break;
                    
                    case 2:
                        profile.SecondName = ValidatorProfileClass.GetStrDataProfile("Введите фамилию контакта:");
                        break;
                    case 3:
                        profile.Patronymic = ValidatorProfileClass.GetStrDataProfile("Введите отчество контакта:");
                        break;
                   case 4:
                        profile.Phone = ValidatorProfileClass.GetPhoneProfile("Введите номер телефона пользователя:");
                        break;
                    
                    case 5:
                        profile.Country = ValidatorProfileClass.GetStrDataProfile("Введите страну контакта:");
                        break;
                    case 6:
                        profile.DateOfBirth = ValidatorProfileClass.GetDateProfile("Введите дату рождения:");
                        break;
                   case 7:
                        profile.Organisation = ValidatorProfileClass.GetStrDataProfile("Введите название организации контакта:");
                        break;
                   case 8:
                        profile.Position = ValidatorProfileClass.GetStrDataProfile("Введите должность контакта:");
                        break;
                    
                    case 9:
                        profile.Other = ValidatorProfileClass.GetStrDataProfile("Введите дополнительную информацию о пользователе:");
                        break;
                }
            }
            UpdateLenArrStr(profile);
            return profile;
        }

        private static void UpdateLenArrStr(Profile profile)
        {
            var myPropertyInfo = typeof(Profile).GetProperties();
            int i = 0;
            Dictionary<string, int> maxLenStrNew = new Dictionary<string, int>();
            foreach (var item in Profile.maxLenStr)
            {
                if (item.Key != "DateOfBirth")
                {
                    int propertyLen = myPropertyInfo[i].GetValue(profile).ToString().Length;

                    maxLenStrNew.Add(item.Key, propertyLen > Profile.maxLenStr[item.Key] ? propertyLen : Profile.maxLenStr[item.Key]);
                } else
                {
                    maxLenStrNew.Add(item.Key, 10);
                }
                i++;
            }

            Profile.maxLenStr = maxLenStrNew;
        }

        public static void RemoveEntryFromArr(ref List<Profile> list)
        {
            while (true)
            {
                Console.WriteLine("Удаление записи!");

                if (!ActionsBase.PrintAllDataWithId(list))
                {
                    Console.ReadLine();
                    break;
                }


                Console.Write("\nВведите номер записи, которую хотите удалить или -1 для завершения редактирования и выхода в меню: ");
                if (Int32.TryParse(Console.ReadLine(), out int delEntry) && delEntry >= 0 && delEntry < list.Count)
                {
                    list.RemoveAt(delEntry);
                    Console.WriteLine($"Запись под номером {delEntry} удалена!\nДля продолжения нажмите любую клавишу.");
                    Console.ReadLine();
                }
                else if (delEntry == -1)
                {
                    break;
                }

            }
        }
    }
}
