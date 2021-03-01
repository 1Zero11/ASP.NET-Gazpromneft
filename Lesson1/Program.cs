using DataLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializationManager serializationManager = new SerializationManager();
            DBManager dBManager = new DBManager();

            var tanks = dBManager.GetTanks();
            var units = dBManager.GetUnits();
            var factories = dBManager.GetFactories();

            TextManager textManager = new TextManager(dBManager);
            textManager.Event += (str) => textManager.Show(new string[] { str });

            while (true) //Основной цикл
            {

                textManager.Show("Доступные команды:");

                textManager.Show("1) Вывести информацию");
                textManager.Show("2) Создать json");
                textManager.Show("3) Поиск");
                textManager.Show("4) Выход");
                textManager.Show("");

                textManager.Show("Введите номер команды");

                

                string readLine = textManager.ReadLine();

                textManager.Show("");




                dBManager.Populate(); //Заполняем пустые объекты - создаём связи в обе стороны

                

                if (readLine == "1")
                {
                    textManager.Show(textManager.Information());
                }
                else if (readLine == "2")
                {
                    serializationManager.FileName = @".\text.json";
                    try
                    {
                        textManager.Show(new string[] { serializationManager.Serialize(dBManager.factories.ToArray()) });
                    }catch (NullReferenceException e)
                    {
                        textManager.Show(e.Message);
                    }
                }
                else if (readLine == "3")
                {
                    try
                    {
                        textManager.SearchDialog(units, tanks, factories);
                    }
                    catch (InvalidOperationException e)
                    {
                        textManager.Show(e.Message);
                    }
                }
                if (readLine == "4")
                    break;

                textManager.Show("");
            }
            
        }
    }

    

    

    

}
