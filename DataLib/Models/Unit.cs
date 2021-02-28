using System;
using System.Collections.Generic;
using System.Text;
using DataLib.Models;

namespace DataLib.Models
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
    public class Unit : Item
    {
        public int FactoryId { get; set; }
        public List<Tank> Tanks { get; set; }

        public Factory Factory;

        public Unit(int id, string name, int factoryId)
        {
            Id = id;
            Name = name;
            FactoryId = factoryId;
        }

        public Unit() { }
    }
}
