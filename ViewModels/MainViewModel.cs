using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TodoListApp.Commands;
using TodoListApp.Models;
using TodoListApp.Services;

namespace TodoListApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TodoTask> Tasks { get; set; }

        private TodoTask _selectedTask;
        public TodoTask SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand RemoveCommand { get; }
        public RelayCommand RemoveAllCommand { get; }
        public RelayCommand SaveCommand { get; }

        public MainViewModel()
        {
            Tasks = FileService.Load();

            AddCommand = new RelayCommand(_ => AddTask());
            RemoveCommand = new RelayCommand(_ => RemoveTask(), _ => SelectedTask != null);
            RemoveAllCommand = new RelayCommand(_ => RemoveAllTasks(), _ => Tasks.Any());
            SaveCommand = new RelayCommand(_ => FileService.Save(Tasks));
        }

        private void AddTask()
        {
            int newNumber = 1;

            // Получаем список номеров существующих задач
            var existingNumbers = Tasks
                .Where(t => t.Title.StartsWith("Задача"))
                .Select(t =>
                {
                    var part = t.Title.Substring(7);
                    return int.TryParse(part, out int n) ? n : 0;
                })
                .Where(n => n > 0)
                .ToHashSet();

            // Находим минимально свободный номер
            while (existingNumbers.Contains(newNumber))
                newNumber++;

            var task = new TodoTask { Title = $"Задача {newNumber}" };
            Tasks.Add(task);
            SelectedTask = task;
        }

        private void RemoveTask()
        {
            Tasks.Remove(SelectedTask);
        }

        private void RemoveAllTasks()
        {
            Tasks.Clear();
            SelectedTask = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}