using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using lab13;
using library_for_lab10;
using library_for_lab12;
using System.Collections.Generic;

namespace TestForLab13
{
    [TestClass]
    public class TestForLab13
    {
        [TestMethod]
        public void AddToBegin()
        {
            //Arrange
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Коллекция из 5 элементов для тестиования", 5);
            bool eventRaised = false;
            collection.CollectionCountChanged += (sender, args) => eventRaised = true;

            //Act
            collection.AddToBegin(new Tool());

            //Assert
            Assert.IsTrue(eventRaised, "Коллекция из 6 элементов");
        }

        [TestMethod]
        public void AddToEnd()
        {
            //Arrange
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Коллекция из 5 элементов для тестиования", 5);
            bool eventRaised = false;
            collection.CollectionCountChanged += (sender, args) => eventRaised = true;

            //Act
            collection.AddToEnd(new Tool("пассатижи", 66));

            //Assert
            Assert.IsTrue(eventRaised, "Коллекция из 6 элементов");
        }

        [TestMethod]
        public void Remove()
        {
            //Arrange
            Tool tool = new Tool();
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Коллекция из 2 элементов для тестиования", 2);
            collection.AddToEnd(tool);
            bool eventRaised = false;
            collection.CollectionCountChanged += (sender, args) => eventRaised = true;

            //Act
            collection.Remove(tool);

            //Assert
            Assert.IsTrue(eventRaised, "Коллекция из 1 элемента");
        }

        [TestMethod]
        public void IndexerSet()
        {
            //Arrange
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Коллекция из 3 элементов для тестиования", 3);
            bool eventRaised = false;
            collection.CollectionReferenceChanged += (sender, args) => eventRaised = true;
            collection.AddToEnd(new Tool());

            //Act
            collection[0] = new Tool();

            //Assert
            Assert.IsTrue(eventRaised, "Новая коллекция с новым элемнетом");
        }

        [TestMethod]
        public void IndexerGet()
        {
            // Arrange
            Tool tool = new Tool("инструмент", 1);
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Тестируемая коллекция", 5);
            collection.AddToBegin(tool);

            // Act
            var result = collection[0];

            // Assert
            Assert.AreEqual(tool, result, "Возвращенный элемент должен быть тем же самым объектом, что был добавлен в коллекцию");
        }

        [TestMethod]
        public void IndexerGetInvalidIndex()
        {
            //Arrange
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Тестируемая коллекция", 5);

            //Act and Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { var result = collection[0]; });
        }


        [TestMethod]
        public void CollectionName()
        {
            //Arrange
            string expectedCollectionName = "Тестируемая коллекция";
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>(expectedCollectionName, 5);

            //Act
            string actualCollectionName = collection.CollectionName;

            //Assert
            Assert.AreEqual(expectedCollectionName, actualCollectionName, "Свойство CollectionName должно возвращать ожидаемое значение");
        }

        [TestMethod]
        public void WriteRecord()
        {
            //Arrange
            Journal journal = new Journal();
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Тестируемая коллекция", 5);
            JournalEntry expectedEntry = new JournalEntry("Тестируемая коллекция", "элемент добавлен в начало", "инструмент 1");
            JournalEntry actualEntry = null;

            //Act
            journal.WriteRecord(this, new CollectionHandlerEventArgs("Тестируемая коллекция", "элемент добавлен в начало", "инструмент 1"));

            //Assert
            foreach (JournalEntry entry in journal)
            {
                actualEntry = entry;
                break;
            }

            Assert.IsNotNull(actualEntry, "Журнал должен содержать запись");
            Assert.AreEqual(expectedEntry.CollectionName, actualEntry.CollectionName, "Название коллекции не совпадает");
            Assert.AreEqual(expectedEntry.ChangeType, actualEntry.ChangeType, "Тип изменения не совпадает");
            Assert.AreEqual(expectedEntry.ChangedItem, actualEntry.ChangedItem, "Изменяемый элемент не совпадает");
        }

        [TestMethod]
        public void WriteRecordAddMultipleJournalEntries()
        {
            //Arrange
            Journal journal = new Journal();
            MyObservableCollection<Tool> collection = new MyObservableCollection<Tool>("Тестируемая коллекция", 5);
            List<JournalEntry> expectedEntries = new List<JournalEntry>
            {
                new JournalEntry("Тестируемая коллекция", "элемент добавлен в начало", "инструмент 1"),
                new JournalEntry("Тестируемая коллекция", "элемент добавлен в конец", "пассатижи 66"),
                new JournalEntry("Тестируемая коллекция", "элемент удалён", "инструмент 1")
            };
            List<JournalEntry> actualEntries = new List<JournalEntry>();

            //Act
            journal.WriteRecord(this, new CollectionHandlerEventArgs("Тестируемая коллекция", "элемент добавлен в начало", "инструмент 1"));
            journal.WriteRecord(this, new CollectionHandlerEventArgs("Тестируемая коллекция", "элемент добавлен в конец", "пассатижи 66"));
            journal.WriteRecord(this, new CollectionHandlerEventArgs("Тестируемая коллекция", "элемент удалён", "инструмент 1"));

            //Assert
            foreach (JournalEntry entry in journal)
            {
                actualEntries.Add(entry);
            }

            Assert.AreEqual(expectedEntries.Count, actualEntries.Count, "Количество записей в журнале не совпадает");

            for (int i = 0; i < expectedEntries.Count; i++)
            {
                Assert.AreEqual(expectedEntries[i].CollectionName, actualEntries[i].CollectionName, $"Запись {i + 1}: Название коллекции не совпадает");
                Assert.AreEqual(expectedEntries[i].ChangeType, actualEntries[i].ChangeType, $"Запись {i + 1}: Тип изменения не совпадает");
                Assert.AreEqual(expectedEntries[i].ChangedItem, actualEntries[i].ChangedItem, $"Запись {i + 1}: Изменяемый элемент не совпадает");
            }
        }


        [TestMethod]
        public void Constructor()
        {
            //Arrange
            string collectionName = "Тестовая коллекция";
            string changeType = "Добавление";
            object changedItem = new object();

            //Act
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(collectionName, changeType, changedItem);

            //Assert
            Assert.AreEqual(collectionName, args.CollectionName, "");
            Assert.AreEqual(changeType, args.ChangeType, "");
            Assert.AreEqual(changedItem, args.ChangedItem, "");
        }

        [TestMethod]
        public void ToString_ReturnsExpectedString()
        {
            //Arrange
            string collectionName = "Тестовая коллекция";
            string changeType = "Удаление";
            string changedItemString = "Элемент для удаления";
            object changedItem = changedItemString;

            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(collectionName, changeType, changedItem);

            //Act
            string result = args.ToString();

            //Assert
            string expected = $"В коллекции {collectionName} {changeType}, изменяемый элемент: {changedItemString}";
            Assert.AreEqual(expected, result, "Метод ToString() должен возвращать ожидаемую строку");
        }
    }
}
