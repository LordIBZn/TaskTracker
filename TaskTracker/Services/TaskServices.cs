using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using TaskTracker.Utility;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class TaskServices
    {
        private readonly JsonFileHandler<Models.Task> _fileHandler;
        private readonly List<Models.Task> _tasks;

        public TaskServices(string fileName)
        {
            _fileHandler = new JsonFileHandler<Models.Task>(fileName);
            _tasks = _fileHandler.Read();
        }

        public void AddTask(string description)
        {
            var newTask = new Models.Task
            {
                Id = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1,
                Description = description,
                Status = "todo",
                Created = DateTime.Now,
                Updated = DateTime.Now
            };

            _tasks.Add(newTask);
            _fileHandler.Write(_tasks);

            Console.WriteLine($"Tarea agregada exitosamente (ID: {newTask.Id})");
        }
        public void UpdateTask(int id, string newDescription)
        {
            var task = FindTask(id);
            if (task == null) return;

            task.Description = newDescription;
            task.Updated = DateTime.Now;

            _fileHandler.Write(_tasks);
            Console.WriteLine("Tarea actualizada correctamente.");
        }
        public void DeleteTask(int id)
        {
            var task = FindTask(id);
            if (task == null) return;

            _tasks.Remove(task);
            _fileHandler.Write(_tasks);
            Console.WriteLine("Tarea eliminada correctamente.");
        }
        public void ChangeStatus(int id, string status)
        {
            var task = FindTask(id);
            if (task == null) return;

            task.Status = status;
            task.Updated = DateTime.Now;

            _fileHandler.Write(_tasks);
            Console.WriteLine($"Tarea marcada como {status}.");
        }
        public void ListTasks(string status = null)
        {
            var filteredTasks = string.IsNullOrEmpty(status)
                ? _tasks
                : _tasks.Where(t => t.Status == status).ToList();

            if (!filteredTasks.Any())
            {
                Console.WriteLine("No hay tareas para mostrar.");
                return;
            }

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status} (Creado: {task.Created})");
            }
        }
        private Models.Task FindTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Tarea no encontrada.");
            }
            return task;
        }


    }
}
