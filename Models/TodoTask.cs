using System.ComponentModel;

namespace TodoListApp.Models
{
    public class TodoTask : INotifyPropertyChanged
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                EditedAt = DateTime.Now;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(EditedAt));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                EditedAt = DateTime.Now;
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(EditedAt));
            }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                EditedAt = DateTime.Now;
                OnPropertyChanged(nameof(IsCompleted));
                OnPropertyChanged(nameof(EditedAt));
            }
        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        private DateTime? _editedAt;
        public DateTime? EditedAt
        {
            get => _editedAt;
            set
            {
                _editedAt = value;
                OnPropertyChanged(nameof(EditedAt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}