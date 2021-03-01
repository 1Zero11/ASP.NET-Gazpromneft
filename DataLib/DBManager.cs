using System;
using System.Collections.Generic;
using System.Text;
using DataLib.Models;

namespace DataLib
{
    public class DBManager
    {

        public  List<Factory> factories = new List<Factory>();
        public  List<Unit> units = new List<Unit>();
        public  List<Tank> tanks = new List<Tank>();

        public Tank[] GetTanks()
        {
            // ваш код здесь

            Tank[] Tanks = new Tank[6];

            Tanks[0] = new Tank(1, "Резервуар 1", 1500, 2000, 1);
            Tanks[1] = new Tank(2, "Резервуар 2", 2500, 3000, 1);
            Tanks[2] = new Tank(3, "Дополнительный резервуар 24", 3000, 3000, 2);
            Tanks[3] = new Tank(4, "Резервуар 35", 3000, 3000, 2);
            Tanks[4] = new Tank(5, "Резервуар 47", 4000, 5000, 2);
            Tanks[5] = new Tank(6, "Резервуар 256", 500, 500, 3);

            tanks = new List<Tank>(Tanks);

            return Tanks;
        }

        public  Unit[] GetUnits()
        {
            // ваш код здесь
            Unit[] Units = new Unit[3];

            Units[0] = new Unit(1, "ГФУ-1", 1);
            Units[1] = new Unit(2, "ГФУ-2", 1);
            Units[2] = new Unit(3, "АВТ-6", 2);

            units = new List<Unit>(Units);

            return Units;
        }

        public  Factory[] GetFactories()
        {
            // ваш код здесь
            Factory[] Factories = new Factory[2];

            Factories[0] = new Factory(1, "МНПЗ", "Московский нефтеперерабатывающий завод");
            Factories[1] = new Factory(2, "ОНПЗ", "Омский нефтеперерабатывающий завод");

            factories = new List<Factory>(Factories);

            return Factories;
        }

        // реализуйте этот метод, чтобы он возвращал объект завода, которому принадлежит установка
        public  Factory FindFactory(Unit unit)
        {
            // ваш код здесь

            return unit.Factory;
        }

        // реализуйте этот метод, чтобы он возвращал суммарный объем резервуаров в массиве
        public  int GetTotalVolume()
        {
            // ваш код здесь

            int volume = 0;

            foreach (Tank tank in tanks)
                volume += tank.Volume;

            return volume;
        }

        public  void Populate()
        {
            //Для каждой фабрики создаём List и ищем установки, FactoryId которых совпадает с id этого завода. 

            for (int i = 0; i < factories.Count; i++)
            {
                Factory factory = factories[i];
                factory.Units = new List<Unit>();

                for (int j = 0; j < units.Count; j++)
                {
                    Unit unit = units[j];
                    unit.Tanks = new List<Tank>();

                    if (unit.FactoryId == factory.Id)
                    {
                        //Добавляем их в List, а им самим даём объект завода
                        factory.Units.Add(unit);
                        unit.Factory = factory;
                    }

                    foreach (Tank tank in tanks)
                    {
                        if (tank.UnitId == unit.Id)
                        {
                            unit.Tanks.Add(tank);
                            tank.Unit = unit;
                        }

                    }

                }
            }
        }

        public  void UnloadToDB()
        {
            units.Clear();
            tanks.Clear();

            

            for (int i = 0; i < factories.Count; i++)
            {
                Factory factory = factories[i];

                if (factory.Units != null)
                {
                    units.AddRange(factory.Units);
                    
                    for (int j = 0; j < factory.Units.Count; j++)
                    {
                        if (factory.Units[j].Tanks != null)
                            tanks.AddRange(factory.Units[j].Tanks);
                    }
                }
            }


            Populate();
        }

        public  void ClearConnections()
        {
            foreach (Factory factory in factories)
            {
                factory.Units = null;
            }

            foreach (Unit unit in units)
            {
                unit.Tanks = null;
                unit.Factory = null;
            }

            foreach (Tank tank in tanks)
            {
                tank.Unit = null;
            }
        }

        public T FindById<T>(IEnumerable<T> collection, int id) where T : Item
        {
            if(collection == null)
                throw new NullReferenceException("No collection");

            foreach (T item in collection)
                if (item.Id == id)
                    return item;
            return default(T);
        }

        public  T FindByName<T>(IEnumerable<T> collection, string name) where T : Item
        {
            if (collection == null)
                throw new NullReferenceException("No collection");

            foreach (T item in collection)
                if (item.Name == name)
                    return item;

            throw new InvalidOperationException($"Не найден объект с именем {name}");
            //return default(T);
        }



        public  void AddFactory(Factory factory)
        {
            factories.Add(factory);
        }

        public Factory ChangeFactory(int id, Factory factory)
        {
            Factory foundFactory = FindById(factories, id);

            foundFactory.Id = factory.Id;
            foundFactory.Description = factory.Description;
            foundFactory.Name = factory.Name;

            ClearConnections();
            Populate();

            return foundFactory;
        }

        public  void DeleteFactory(string name)
        {
            Factory factory = FindByName(factories ,name);
            if(factory.Units!=null)
                foreach (Unit unit in factory.Units)
                {
                    if(unit.Tanks!=null)
                        foreach (Tank tank in unit.Tanks)
                            tanks.Remove(tank);
                    units.Remove(unit);
                }
            factories.Remove(factory);

            ClearConnections();
            Populate();
        }



        public  void AddTank(Tank tank)
        {
            tanks.Add(tank);
            tank.Unit = FindById(units, tank.UnitId);
            tank.Unit.Tanks.Add(tank);
        }

        public Tank ChangeTank(int id, Tank tank)
        {
            Tank foundTank = FindById(tanks, id);

            foundTank.Id = tank.Id;
            foundTank.Volume = tank.Volume;
            foundTank.Name = tank.Name;
            foundTank.MaxVolume = tank.MaxVolume;
            foundTank.UnitId = tank.UnitId;


            ClearConnections();
            Populate();

            return foundTank;
        }

        public  void DeleteTank(string name)
        {
            Tank tank = FindByName(tanks, name);
            tank.Unit.Tanks.Remove(tank);
            tank.Unit = null;
            tanks.Remove(tank);

            ClearConnections();
            Populate();
        }

        public  void AddUnit(Unit unit)
        {
            units.Add(unit);
            Factory factory = FindById(factories, unit.FactoryId);
            unit.Factory = factory;
            factory.Units.Add(unit);
        }

        public Unit ChangeUnit(int id, Unit unit)
        {
            Unit foundUnit = FindById(units, id);

            foundUnit.Id = unit.Id;
            foundUnit.FactoryId = unit.FactoryId;
            foundUnit.Name = unit.Name;

            ClearConnections();
            Populate();

            return foundUnit;
        }

        public  void DeleteUnit(string name)
        {
            Unit unit = FindByName(units, name);
            if(unit.Tanks!=null)
                foreach (Tank tank in unit.Tanks)
                {
                    tanks.Remove(tank);
                }
            unit.Factory.Units.Remove(unit);
            unit.Factory = null;
            units.Remove(unit);

            ClearConnections();
            Populate();
        }


    }
}
