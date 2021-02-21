using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Svetliakov
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) //Основной цикл
            {
                Console.WriteLine("Доступные команды:");

                Console.WriteLine("1) Вывести информацию");
                Console.WriteLine("2) Создать json");
                Console.WriteLine("3) Поиск");
                Console.WriteLine("4) Выход");
                Console.WriteLine();

                Console.WriteLine("Введите номер команды");
                string readLine = Console.ReadLine();
                Console.WriteLine();

                var tanks = GetTanks();
                var units = GetUnits();
                var factories = GetFactories();

                
                Populate(factories, units, tanks); //Заполняем пустые объекты - создаём связи в обе стороны


                if (readLine == "1")
                {
                    Console.WriteLine($"Количество {tanks.Length} резервуаров"); // должно быть 

                    foreach (Tank tank in tanks)
                    {
                        var foundUnit = FindUnit(units, tank.Unit.Name);
                        var factory = FindFactory(factories, foundUnit);

                        Console.WriteLine($"{tank.Name} принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
                    }

                    var totalVolume = GetTotalVolume(tanks);
                    Console.WriteLine($"Общий объем резервуаров: {totalVolume}");




                }
                else if (readLine == "2")
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
                else if (readLine == "3")
                {
                    Console.WriteLine("Доступные команды:");

                    Console.WriteLine("1) Поиск по заводам");
                    Console.WriteLine("2) Поиск по установкам");
                    Console.WriteLine("3) Поиск по резервуарам");
                    Console.WriteLine();

                    Console.WriteLine("Введите номер команды");
                    readLine = Console.ReadLine();


                    bool found = false;

                    switch (readLine)
                    {
                        case "1":
                            Console.WriteLine("Введите название завода");
                            readLine = Console.ReadLine();

                            foreach (Factory factory in factories)
                                if (factory.Name == readLine)
                                {
                                    string unitString = "";
                                    foreach (Unit unit in factory.Units)
                                        unitString += unit.Name + ", ";
                                    unitString = unitString.Remove(unitString.Length - 2);
                                    Console.WriteLine($"Найден завод {factory.Name}. Описание: {factory.Description}. Установки: {unitString}");
                                    found = true;
                                    break;
                                }
                            break;

                        case "2":
                            Console.WriteLine("Введите название установки");
                            readLine = Console.ReadLine();

                            foreach (Unit unit in units)
                                if (unit.Name == readLine)
                                {
                                    string tankString = "";
                                    foreach (Tank tank in unit.Tanks)
                                        tankString += tank.Name + ", ";
                                    tankString = tankString.Remove(tankString.Length - 2);
                                    Console.WriteLine($"Найдена установка {unit.Name}, принадлежащая заводу {unit.Factory.Name}. Резервуары: {tankString}");
                                    found = true;
                                    break;
                                }
                            break;

                        case "3":
                            Console.WriteLine("Введите название резервуара");
                            readLine = Console.ReadLine();

                            foreach (Tank tank in tanks)
                                if (tank.Name == readLine)
                                {
                                    Console.WriteLine($"Найден резервуар {tank.Name}, принадлежащий установке {tank.Unit.Name} и заводу {tank.Unit.Factory.Name}." +
                                        $" Загрузка: {tank.Volume}. Максимальная загрузка: {tank.MaxVolume}");
                                    found = true;
                                    break;
                                }
                            break;
                    }

                    if (!found)
                        Console.WriteLine("Совпадений не найдено");
                }
                else if (readLine == "4")
                    break;
                
                Console.WriteLine();
            }
            
        }

        // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенной таблице
        public static Tank[] GetTanks()
        {
            // ваш код здесь

            Tank[] tanks = new Tank[6];

            tanks[0] = new Tank("Резервуар 1", 1500, 2000, 1);
            tanks[1] = new Tank("Резервуар 2", 2500, 3000, 1);
            tanks[2] = new Tank("Дополнительный резервуар 24", 3000, 3000, 2);
            tanks[3] = new Tank("Резервуар 35", 3000, 3000, 2);
            tanks[4] = new Tank("Резервуар 47", 4000, 5000, 2);
            tanks[5] = new Tank("Резервуар 256", 500, 500, 3);

            return tanks;
        }

        public static Unit[] GetUnits()
        {
            // ваш код здесь
            Unit[] units = new Unit[3];

            units[0] = new Unit("ГФУ-1", 1);
            units[1] = new Unit("ГФУ-2", 1);
            units[2] = new Unit("АВТ-6", 2);

            return units;
        }

        public static Factory[] GetFactories()
        {
            // ваш код здесь
            Factory[] factories = new Factory[2];

            factories[0] = new Factory("МНПЗ", "Московский нефтеперерабатывающий завод");
            factories[1] = new Factory("ОНПЗ", "Омский нефтеперерабатывающий завод");

            return factories;
        }

        // реализуйте этот метод, чтобы он возвращал найденную в массиве установку по имени
        public static Unit FindUnit(Unit[] units, string unitName)
        {
            // ваш код здесь

            for (int i = 0; i < units.Length; i++)
                if (units[i].Name == unitName)
                    return units[i];

            return null;
        }

        // реализуйте этот метод, чтобы он возвращал объект завода, которому принадлежит установка
        public static Factory FindFactory(Factory[] factories, Unit unit)
        {
            // ваш код здесь

            return unit.Factory;
        }

        // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
        public static int GetTotalVolume(Tank[] tanks)
        {
            // ваш код здесь

            int volume = 0;

            foreach (Tank tank in tanks)
                volume += tank.Volume;

            return volume;
        }

        public static void Populate(Factory[] factories, Unit[] units, Tank[] tanks)
        {
            //Для каждой фабрики создаём List и ищем установки, FactoryId которых совпадает с id этого завода. 
            for (int i = 0; i < factories.Length; i++)
            {
                Factory factory = factories[i];
                List<Unit> listOfFactoryUnits = new List<Unit>();
                for (int j = 0; j < units.Length; j++)
                {
                    Unit unit = units[j];
                    if (unit.FactoryId - 1 == i)
                    {
                        //Добавляем их в List, а им самим даём объект завода
                        listOfFactoryUnits.Add(unit);
                        unit.Factory = factory;
                    }

                    //То же самое для установок и резервуаров
                    List<Tank> listOfUnitTanks = new List<Tank>();
                    foreach (Tank tank in tanks)
                    {
                        if (tank.UnitId - 1 == j)
                        {
                            listOfUnitTanks.Add(tank);
                            tank.Unit = unit;
                        }

                    }
                    unit.Tanks = listOfUnitTanks.ToArray(); //Конвертируем List резервуаров в Array и кладём его в объект установки

                }

                factory.Units = listOfFactoryUnits.ToArray();

            }

        }
    }

    /// <summary>
    /// Установка
    /// 
    /// Создаем конструктор класса
    /// 
    /// Tanks[] означает массив объектов резервуаров в установке
    /// Factory означает объект завода, к которому принадлежит установка
    /// И Tanks[], и Factory заполняются в методе Populate()
    /// 
    /// Чтобы получилась строгая иерархия в json, сериализируем связи только в одну сторону (get set)
    /// </summary>
    public class Unit
    {
       

        public string Name { get; set; }
        public int FactoryId { get; set; }
        public Tank[] Tanks { get; set; }

        public Factory Factory;

        public Unit(string name, int factoryId)
        {
            Name = name;
            FactoryId = factoryId;
        }
    }

    /// <summary>
    /// Завод
    /// 
    /// Создаем конструктор класса
    /// 
    /// Units[] означает массив объектов установок в заводе
    /// Units[] заполняются в методе Populate()
    /// 
    /// Чтобы получилась строгая иерархия в json, сериализируем связи только в одну сторону (get set)
    /// </summary>
    public class Factory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Unit[] Units { get; set; }

        public Factory(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

    /// <summary>
    /// Резервуар
    /// 
    /// Создаем конструктор класса
    /// 
    /// Unit означает установку, к которой относится резервуар
    /// Unit заполняются в методе Populate()
    /// 
    /// Чтобы получилась строгая иерархия в json, сериализируем связи только в одну сторону (get set)
    /// </summary>
    public class Tank
    {
        //..

        public string Name { get; set; }
        public int Volume { get; set; }
        public int MaxVolume { get; set; }
        public int UnitId { get; set; }

        public Unit Unit;

        public Tank(string name, int volume, int maxVolume, int unitId)
        {
            Name = name;
            Volume = volume;
            MaxVolume = maxVolume;
            UnitId = unitId;
        }
    }

}
