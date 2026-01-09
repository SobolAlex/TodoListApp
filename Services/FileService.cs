using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using TodoListApp.Models;

namespace TodoListApp.Services
{
    public static class FileService
    {
        private static string filePath = "tasks.json";

        public static ObservableCollection<TodoTask> Load()
        {
            if (!File.Exists(filePath))
                return new ObservableCollection<TodoTask>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<TodoTask>>(json);
        }

        public static void Save(ObservableCollection<TodoTask> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
        }
    }
}