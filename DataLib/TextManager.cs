using System;
using System.Collections.Generic;
using System.Text;
using DataLib.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.IO;
using System.Diagnostics;

namespace DataLib
{
    public  class TextManager
    {
        DBManager dbmanager = new DBManager();

        public TextManager(DBManager manager)
        {
            dbmanager = manager;
        }

        public  IList<string> Information()
        {
            List<string> output = new List<string>();
            output.Add($"Количество {dbmanager.tanks.Count} резервуаров"); // должно быть 

            foreach (Tank tank in dbmanager.tanks)
            {
                Debug.WriteLine(tank.Name);
                var foundUnit = dbmanager.FindByName(dbmanager.units, tank.Unit.Name);
                var factory = dbmanager.FindFactory(foundUnit);

                output.Add($"{tank.Name} принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
            }

            var totalVolume = dbmanager.GetTotalVolume();
            output.Add($"Общий объем резервуаров: {totalVolume}");

            return output.ToArray();
        }


        public delegate void SampleEventHandler(string s);

        public event SampleEventHandler Event;

        public string ReadLine()
        {
            int number = 0;
            string str = Console.ReadLine();
            if(int.TryParse(str,out number))
                Event?.Invoke($"Пользователь ввёл число {number} в время {DateTime.Now}"); ;

            return str;

        }





    public  void SearchDialog(IEnumerable<Unit> units, IEnumerable<Tank> tanks, IEnumerable<Factory> factories)
        {
            Console.WriteLine("Доступные команды:");

            Console.WriteLine("1) Поиск по заводам");
            Console.WriteLine("2) Поиск по установкам");
            Console.WriteLine("3) Поиск по резервуарам");
            Console.WriteLine();

            Console.WriteLine("Введите номер команды");
            string readLine = Console.ReadLine();


            bool found = false;

            switch (readLine)
            {
                case "1":
                    Console.WriteLine("Введите название завода");
                    readLine = Console.ReadLine();

                    Factory factory = dbmanager.FindByName(factories, readLine);


                    if (factory != null)
                    {
                        string unitString = "";
                        foreach (Unit u in factory.Units)
                            unitString += u.Name + ", ";
                        unitString = unitString.Remove(unitString.Length - 2);

                        Console.WriteLine($"Найден завод {factory.Name}. Описание: {factory.Description}. Установки: {unitString}");

                        found = true;
                    }
                    break;


                case "2":
                    Console.WriteLine("Введите название установки");
                    readLine = Console.ReadLine();

                    Unit unit = dbmanager.FindByName(units, readLine);

                    if (unit != null)
                    {
                        string tankString = "";
                        foreach (Tank t in unit.Tanks)
                            tankString += t.Name + ", ";
                        tankString = tankString.Remove(tankString.Length - 2);

                        Console.WriteLine($"Найдена установка {unit.Name}, принадлежащая заводу {unit.Factory.Name}. Резервуары: {tankString}");

                        found = true;
                    }
                    break;

                case "3":
                    Console.WriteLine("Введите название резервуара");
                    readLine = Console.ReadLine();

                    Tank tank = dbmanager.FindByName(tanks, readLine);

                    if (tank != null)
                    {
                        Console.WriteLine($"Найден резервуар {tank.Name}, принадлежащий установке {tank.Unit.Name} и заводу {tank.Unit.Factory.Name}." +
                                    $" Загрузка: {tank.Volume}. Максимальная загрузка: {tank.MaxVolume}");

                        found = true;
                    }
                    break;
            }

            if (!found)
                Console.WriteLine("Совпадений не найдено");
        }

        public  void Show(IEnumerable<string> str)
        {
            foreach (string s in str)
                Console.WriteLine(s);
        }

        public void Show(string str)
        {
            Console.WriteLine(str);
        }
    }
}
