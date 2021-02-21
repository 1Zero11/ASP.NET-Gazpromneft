using System;
using System.Collections.Generic;
using Svetliakov.Models;

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

                var tanks = DBManager.GetTanks();
                var units = DBManager.GetUnits();
                var factories = DBManager.GetFactories();


                DBManager.Populate(factories, units, tanks); //Заполняем пустые объекты - создаём связи в обе стороны


                if (readLine == "1")
                {
                    Routing.ShowInformation(tanks, units, factories);
                }
                else if (readLine == "2")
                {
                    Routing.GenerateJson(factories);
                }
                else if (readLine == "3")
                {
                    Routing.SearchDialog(units, tanks, factories);
                }
                else if (readLine == "4")
                    break;
                
                Console.WriteLine();
            }
            
        }
    }

    

    

    

}
