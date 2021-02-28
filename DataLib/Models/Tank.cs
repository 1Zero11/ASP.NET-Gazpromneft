using System;
using System.Collections.Generic;
using System.Text;
using DataLib.Models;
using static DataLib.DBManager;

namespace DataLib.Models
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
    public class Tank : Item
    {
        public int Volume { get; set; }
        public int MaxVolume { get; set; }
        public int UnitId { get; set; }

        public Unit Unit;

        public Tank(int id, string name, int volume, int maxVolume, int unitId)
        {
            Id = id;
            Name = name;
            Volume = volume;
            MaxVolume = maxVolume;
            UnitId = unitId;
        }

        public Tank() { }
    }

}
