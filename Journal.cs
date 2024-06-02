using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab13
{
   
    internal class Journal : IEnumerable<JournalEntry>
    {
        private List<JournalEntry> journal = new List<JournalEntry>();

        //метод для добавления новой записи в журнал
        public void WriteRecord(object source, CollectionHandlerEventArgs args)
        {
            string collectionName = args.CollectionName;
            JournalEntry entry = new JournalEntry(collectionName, args.ChangeType, args.ChangedItem.ToString());
            journal.Add(entry);
        }

        public IEnumerator<JournalEntry> GetEnumerator()
        {
            return ((IEnumerable<JournalEntry>)journal).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)journal).GetEnumerator();
        }
    }

    public class JournalEntry //класс, представляющий запись в журнал
    {
        public string CollectionName { get; set; } //название коллекции
        public string ChangeType { get; set; } //тип изменения
        public string ChangedItem { get; set; } //изменяемый элемент

        public JournalEntry(string collectionName, string changeType, string changedItem) //конструктор для записи
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedItem = changedItem;
        }

        public override string ToString()
        {
            return $"В коллекции {CollectionName} {ChangeType}, изменяемый элемент: {ChangedItem}";
        }
    }
}
