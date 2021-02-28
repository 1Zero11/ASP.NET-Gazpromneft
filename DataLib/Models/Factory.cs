using System;
using System.Collections.Generic;
using System.Text;
using DataLib.Models;

namespace DataLib.Models
{
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
    public class Factory : Item
    {
        public string Description { get; set; }
        public List<Unit> Units { get; set; }

        public Factory(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Factory() { }
    }
}
