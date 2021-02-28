using DataLib;
using System;
using System.Collections.Generic;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tanks = DBManager.GetTanks();
            var units = DBManager.GetUnits();
            var factories = DBManager.GetFactories();

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

               


                DBManager.Populate(); //Заполняем пустые объекты - создаём связи в обе стороны


                if (readLine == "1")
                {
                    TextManager.Show(TextManager.Information());
                }
                else if (readLine == "2")
                {
                    TextManager.Show(new string[] { SerializationManager.Serialize() });
                }
                else if (readLine == "3")
                {
                    TextManager.SearchDialog(units, tanks, factories);
                }
                if (readLine == "4")
                    break;
                
                Console.WriteLine();
            }
            
        }
    }

    

    

    

}
