using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace DataLib
{
    public static class SerializationManager
    {
        public static string fileName;
        public static string Serialize()
        {
            //Пишем json в консоль и в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(DBManager.factories, options);

            File.WriteAllText(fileName, jsonString);

            return jsonString;
        }

        public static T[] Deserialise<T>(string jsonString)
        {
            //string fileName = @".\text.json";
            //string jsonString = File.ReadAllText(fileName);

            T[] facs = JsonSerializer.Deserialize<T[]>(jsonString);
            return facs;
        }

        public static string GetJsonFromFile()
        {
            //string fileName = @".\bin\Debug\net5.0\text.json";
            return File.ReadAllText(fileName);
        }
    }
}
