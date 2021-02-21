using System;
using System.Collections.Generic;
using System.Text;
using Svetliakov.Models;

namespace Svetliakov
{
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
}
