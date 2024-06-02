using library_for_lab10;
using System;
using System.Collections.Generic;

namespace lab13
{
    class Program
    {
        static void Main(string[] args)
        {

            //Создание коллекций
            MyObservableCollection<Tool> collection1 = new MyObservableCollection<Tool>("1", 1);
            Console.WriteLine("Коллекция 1: ");
            foreach (var i in collection1)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            MyObservableCollection<Tool> collection2 = new MyObservableCollection<Tool>("2", 2);
            Console.WriteLine("Коллекция 2: ");
            foreach (var i in collection2)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //Создание журналов
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            //Подписка на события
            collection1.CollectionCountChanged += journal1.WriteRecord;
            collection1.CollectionReferenceChanged += journal1.WriteRecord;

            collection1.CollectionReferenceChanged += journal2.WriteRecord;
            collection2.CollectionReferenceChanged += journal2.WriteRecord;

            //Главное меню программы
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Главное меню:");
                Console.WriteLine("1. Добавить элемент в коллекцию 1");
                Console.WriteLine("2. Добавить элемент в коллекцию 2");
                Console.WriteLine("3. Удалить элемент из коллекции 1");
                Console.WriteLine("4. Удалить элемент из коллекции 2");
                Console.WriteLine("5. Изменить элемент в коллекции 1");
                Console.WriteLine("6. Изменить элемент в коллекции 2");
                Console.WriteLine("7. Вывести данные журнала 1");
                Console.WriteLine("8. Вывести данные журнала 2");
                Console.Write("Выберите действие: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {

                    switch (choice)
                    {
                        case 1:
                            //Добавить элемент в коллекцию 1
                            Tool tool1 = new Tool();
                            tool1.RandomInit();
                            collection1.AddToBegin(tool1);
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection1)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 2:
                            //Добавить элемент в коллекцию 1
                            Tool tool2 = new Tool();
                            tool2.RandomInit();
                            collection1.AddToEnd(tool2);
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection2)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 3:
                            //Удалить элемент из коллекции 1
                            Tool tool3 = new Tool();
                            tool3.Init();
                            collection1.Remove(tool3);
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection1)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 4:
                            //Удалить элемент из коллекции 2
                            Tool tool4 = new Tool();
                            tool4.Init();
                            collection1.Remove(tool4);
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection2)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 5:
                            //Изменить элемент в коллекции 1
                            if (collection1.Count > 0)
                            {
                                collection1[0] = new Tool("Новый инструмент1", 22);
                            }
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection1)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 6:
                            //Изменить элемент в коллекции 2
                            if (collection2.Count > 0)
                            {
                                collection2[0] = new Tool("Новый инструмент2", 22);
                            }
                            Console.WriteLine("Обновлённая коллекция:");
                            foreach (var i in collection2)  
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 7:
                            Console.WriteLine("Журнал 1:");
                            foreach (var i in journal1)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 8:
                            Console.WriteLine("Журнал 2:");
                            foreach (var i in journal2)
                            {
                                Console.WriteLine(i);
                            }
                            Console.WriteLine();
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                }

                Console.WriteLine();
            }
        }
    }
}
