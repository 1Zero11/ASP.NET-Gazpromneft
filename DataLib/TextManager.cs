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
    public static class TextManager
    {
        public static string[] Information()
        {
            List<string> output = new List<string>();
            output.Add($"Количество {DBManager.tanks.Count} резервуаров"); // должно быть 

            foreach (Tank tank in DBManager.tanks)
            {
                Debug.WriteLine(tank.Name);
                var foundUnit = DBManager.FindByName(DBManager.units, tank.Unit.Name);
                var factory = DBManager.FindFactory(foundUnit);

                output.Add($"{tank.Name} принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
            }

            var totalVolume = DBManager.GetTotalVolume();
            output.Add($"Общий объем резервуаров: {totalVolume}");

            return output.ToArray();
        }

        

        public static void SearchDialog(Unit[] units, Tank[] tanks, Factory[] factories)
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

                    Factory factory = DBManager.FindByName(factories, readLine);


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

                    Unit unit = DBManager.FindByName(units, readLine);

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

                    Tank tank = DBManager.FindByName(tanks, readLine);

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

        public static void Show(string[] str)
        {
            foreach (string s in str)
                Console.WriteLine(s);
        }
    }
}
