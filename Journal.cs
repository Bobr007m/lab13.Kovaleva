using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;


namespace lab13
{
    public class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();//список всех событий
        // Конструктор по умолчанию
        public Journal()
        {
            // Инициализация журнала без параметров
        }
        // Конструктор с параметрами
        public Journal(string collectionName, string changeType, string changedItem)
        {
            AddRecord(collectionName, changeType, changedItem);
        }

        // Метод добавления записи в журнал
        public void AddRecord(string collectionName, string changeType, string changedItem)
        {
            var entry = new JournalEntry(collectionName, changeType, changedItem);
            entries.Add(entry);
        }

        // Вывод всех записей
        public void ShowJournal()
        {
            Console.WriteLine("Журнал изменений:");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
        public class JournalEntry
        {
            // Название коллекции, в которой произошло событие
            public string CollectionName { get; }

            // Тип изменения (например: "ItemAdded", "ItemRemoved", "ItemModified")
            public string ChangeType { get; }

            // Данные объекта, с которым связаны изменения
            public string ChangedItem { get; }

            // Конструктор для инициализации полей
            public JournalEntry(string collectionName, string changeType, string changedItem)
            {
                CollectionName = collectionName ?? throw new ArgumentNullException(nameof(collectionName));
                ChangeType = changeType ?? throw new ArgumentNullException(nameof(changeType));
                ChangedItem = changedItem;
            }

            // Перегрузка метода ToString()
            public override string ToString()
            {
                return $"Коллекция: {CollectionName}, Изменение: {ChangeType}, Объект: {ChangedItem}";
            }
        }
    }
}
