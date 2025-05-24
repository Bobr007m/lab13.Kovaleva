using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using lab;

namespace lab13
{
    public class Journal
    {
        private readonly List<JournalEntry> entries = new List<JournalEntry>();

        public void AddEntry(string collectionName, string changeType, object item)
        {
            entries.Add(new JournalEntry(collectionName, changeType, item));
        }

        public void PrintEntries()
        {
            Console.WriteLine("Journal entries:");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
        public string CollectionName { get; }
        public string ChangeType { get; }
        public string ItemInfo { get; }

        public JournalEntry(string collectionName, string changeType, object item)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ItemInfo = item?.ToString() ?? "null";
        }

        public override string ToString()
        {
            return $"[{CollectionName}] {ChangeType}: {ItemInfo}";
        }
    }
}
