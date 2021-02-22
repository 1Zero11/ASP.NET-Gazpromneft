using System;
using System.Collections.Generic;
using System.Text;
using Lesson1.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.IO;

namespace Lesson1
{
    public static class Routing
    {
        public static void ShowInformation(Tank[] tanks, Unit[] units, Factory[] factories)
        {
            Console.WriteLine($"Количество {tanks.Length} резервуаров"); // должно быть 

            foreach (Tank tank in tanks)
            {
                var foundUnit = DBManager.FindUnitByName(units, tank.Unit.Name);
                var factory = DBManager.FindFactory(factories, foundUnit);

                Console.WriteLine($"{tank.Name} принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
            }

            var totalVolume = DBManager.GetTotalVolume(tanks);
            Console.WriteLine($"Общий объем резервуаров: {totalVolume}");
        }

        public static void GenerateJson(Factory[] factories)
        {
            //Пишем json в консоль и в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(factories, options);

            Console.WriteLine(jsonString);
            File.WriteAllText(@".\text.json", jsonString);
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

                    Factory factory = DBManager.FindFactoryByName(factories, readLine);


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

                    Unit unit = DBManager.FindUnitByName(units, readLine);

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

                    Tank tank = DBManager.FindTankByName(tanks, readLine);

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
    }
}
