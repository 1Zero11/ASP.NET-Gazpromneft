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
    public class SerializationManager
    {
        public string FileName;

        public string Serialize(IReadOnlyCollection<Factory> factories)
        {
            //Пишем json в консоль и в файл
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            if (factories == null)
            {
                throw new NullReferenceException("Empty collection");
            }

            string jsonString = JsonSerializer.Serialize(factories, options);

            File.WriteAllText(FileName, jsonString);

            return jsonString;
        }

        public IReadOnlyCollection<T> Deserialise<T>(string jsonString)
        {
            //string fileName = @".\text.json";
            //string jsonString = File.ReadAllText(fileName);

            T[] facs = JsonSerializer.Deserialize<T[]>(jsonString);
            return facs;
        }

        public string GetJsonFromFile()
        {
            //string fileName = @".\bin\Debug\net5.0\text.json";
            return File.ReadAllText(FileName);
        }
    }
}
