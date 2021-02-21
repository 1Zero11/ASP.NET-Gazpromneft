using System;
using System.Collections.Generic;
using System.Text;
using Svetliakov.Models;

namespace Svetliakov
{
    public static class DBManager
    {
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
        public static Unit FindUnitByName(Unit[] units, string unitName)
        {
            // ваш код здесь

            for (int i = 0; i < units.Length; i++)
                if (units[i].Name == unitName)
                    return units[i];

            return null;
        }

        public static Tank FindTankByName(Tank[] tanks, string tankName)
        {
            // ваш код здесь

            for (int i = 0; i < tanks.Length; i++)
                if (tanks[i].Name == tankName)
                    return tanks[i];

            return null;
        }

        public static Factory FindFactoryByName(Factory[] factories, string factoryName)
        {
            // ваш код здесь

            for (int i = 0; i < factories.Length; i++)
                if (factories[i].Name == factoryName)
                    return factories[i];

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
}
