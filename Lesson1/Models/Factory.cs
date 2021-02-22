using System;
using System.Collections.Generic;
using System.Text;
using Lesson1.Models;

namespace Lesson1.Models
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
}
