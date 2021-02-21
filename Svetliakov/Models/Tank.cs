using System;
using System.Collections.Generic;
using System.Text;
using Svetliakov.Models;

namespace Svetliakov.Models
{
    /// <summary>
    /// Резервуар
    /// 
    /// Создаем конструктор класса
    /// 
    /// Unit означает установку, к которой относится резервуар
    /// Unit заполняются в методе Populate()
    /// 
    /// Чтобы получилась строгая иерархия в json, сериализируем связи только в одну сторону (get set)
    /// </summary>
    public class Tank
    {
        //..

        public string Name { get; set; }
        public int Volume { get; set; }
        public int MaxVolume { get; set; }
        public int UnitId { get; set; }

        public Unit Unit;

        public Tank(string name, int volume, int maxVolume, int unitId)
        {
            Name = name;
            Volume = volume;
            MaxVolume = maxVolume;
            UnitId = unitId;
        }
    }

}
