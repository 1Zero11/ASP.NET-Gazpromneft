using System;

namespace Svetliakov
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите команду");
                string readLine = Console.ReadLine();

                if (readLine == "Информация")
                {
                    var tanks = GetTanks();
                    Console.WriteLine($"Количество {tanks.Length} резервуаров"); // должно быть 

                    var units = GetUnits();
                    var factories = GetFactories();

                    Populate(factories, units, tanks);

                    foreach (Tank tank in tanks)
                    {
                        var foundUnit = FindUnit(units, tank.Unit.Name);
                        var factory = FindFactory(factories, foundUnit);

                        Console.WriteLine($"{tank.Name} принадлежит установке {foundUnit.Name} и заводу {factory.Name}");
                    }

                    var totalVolume = GetTotalVolume(tanks);
                    Console.WriteLine($"Общий объем резервуаров: {totalVolume}");

                }else if(readLine == "Выход")
                {
                    break;
                }
                Console.WriteLine();
            }
            
        }

        // реализуйте этот метод, чтобы он возвращал массив резервуаров, согласно приложенной таблице
        public static Tank[] GetTanks()
        {
            // ваш код здесь

            Tank[] tanks = new Tank[6];
            for (int i = 0; i < tanks.Length; i++)
                tanks[i] = new Tank();

            tanks[0].Name = "Резервуар 1";
            tanks[0].Volume = 1500;
            tanks[0].MaxVolume = 2000;
            tanks[0].UnitId = 1;

            tanks[1].Name = "Резервуар 2";
            tanks[1].Volume = 2500;
            tanks[1].MaxVolume = 3000;
            tanks[1].UnitId = 1;

            tanks[2].Name = "Дополнительный резервуар 24";
            tanks[2].Volume = 3000;
            tanks[2].MaxVolume = 3000;
            tanks[2].UnitId = 2;

            tanks[3].Name = "Резервуар 35";
            tanks[3].Volume = 3000;
            tanks[3].MaxVolume = 3000;
            tanks[3].UnitId = 2;

            tanks[4].Name = "Резервуар 47";
            tanks[4].Volume = 4000;
            tanks[4].MaxVolume = 5000;
            tanks[4].UnitId = 2;

            tanks[5].Name = "Резервуар 256";
            tanks[5].Volume = 500;
            tanks[5].MaxVolume = 500;
            tanks[5].UnitId = 3;

            return tanks;
        }

        public static Unit[] GetUnits()
        {
            // ваш код здесь
            Unit[] units = new Unit[3];
            for (int i = 0; i < units.Length; i++)
                units[i] = new Unit();

            units[0].Name = "ГФУ-1";
            units[0].FactoryId = 1;

            units[1].Name = "ГФУ-2";
            units[1].FactoryId = 1;

            units[2].Name = "АВТ-6";
            units[2].FactoryId = 2;

            return units;
        }

        public static Factory[] GetFactories()
        {
            // ваш код здесь
            Factory[] factories = new Factory[2];
            for (int i = 0; i < factories.Length; i++)
                factories[i] = new Factory();

            factories[0].Name = "МНПЗ";
            factories[0].Description = "Московский нефтеперерабатывающий завод";

            factories[1].Name = "ОНПЗ";
            factories[1].Description = "Омский нефтеперерабатывающий завод";

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
            foreach (Unit unit in units)
                unit.Factory = factories[unit.FactoryId - 1];

            foreach (Tank tank in tanks)
                tank.Unit = units[tank.UnitId - 1];
        }
    }

    /// <summary>
    /// Установка
    /// </summary>
    public class Unit
    {
        //..
        // Вам нужно продумать, как реализовать связи между установкой и резервуарами, между заводом и установками

        public string Name;
        public int FactoryId;
        public Factory Factory;
    }

    /// <summary>
    /// Завод
    /// </summary>
    public class Factory
    {
        public string Name;
        public string Description;
    }

    /// <summary>
    /// Резервуар
    /// </summary>
    public class Tank
    {
        //..

        public string Name;
        public int Volume;
        public int MaxVolume;
        public int UnitId;
        public Unit Unit;
    }

}
