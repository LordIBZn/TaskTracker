using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskTracker.Utility
{
    public class JsonFileHandler<T>
    {
        private readonly string _fileName;
        public JsonFileHandler(string fileName)
        {
            _fileName = fileName;

            if (!File.Exists(_fileName))
            {
                File.WriteAllText(_fileName, "[]");
            }
        }

        public List<T> Read()
        {
            var json = File.ReadAllText(_fileName);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void Write(List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions {WriteIndented = true});
            File.WriteAllText(_fileName, json);
        }
    }
}
