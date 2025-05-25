using Geometryclass;
using lab13;
using System;

public class Program
{
    public static void Main()
    {
            var collection1 = new MyObservableCollection<Geometryfigure1>();
            Console.WriteLine("Коллекция 1");
            var collection2 = new MyObservableCollection<Geometryfigure1>();
            Console.WriteLine("Коллекция 2");

            var journal1 = new Journal();
            var journal2 = new Journal();

            // Подписываем журналы на события
            SubscribeJournal1(collection1, journal1);
            SubscribeJournal2(collection1, collection2, journal2);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню ");
                Console.WriteLine("1. Добавить элемент в коллекцию 1");
                Console.WriteLine("2. Добавить элемент в коллекцию 2");
                Console.WriteLine("3. Удалить элемент из коллекции 1");
                Console.WriteLine("4. Удалить элемент из коллекции 2");
                Console.WriteLine("5. Изменить элемент в коллекции 1");
                Console.WriteLine("6. Изменить элемент в коллекции 2");
                Console.WriteLine("7. Вывести содержимое коллекций");
                Console.WriteLine("8. Вывести данные журнала 1");
                Console.WriteLine("9. Вывести данные журнала 2");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddElement(collection1, "Коллекция 1");
                        break;
                    case "2":
                        AddElement(collection2, "Коллекция 2");
                        break;
                    case "3":
                        RemoveElement(collection1, "Коллекция 1");
                        break;
                    case "4":
                        RemoveElement(collection2, "Коллекция 2");
                        break;
                    case "5":
                        ModifyElement(collection1, "Коллекция 1");
                        break;
                    case "6":
                        ModifyElement(collection2, "Коллекция 2");
                        break;
                    case "7":
                        DisplayCollections(collection1, collection2);
                        break;
                    case "8":
                        Console.WriteLine("\n=== Журнал 1 ===");
                        journal1.ShowJournal();
                        break;
                    case "9":
                        Console.WriteLine("\n=== Журнал 2 ===");
                        journal2.ShowJournal();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("Выход из программы.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        // Подписка журнала 1 на оба события первой коллекции
        static void SubscribeJournal1(MyObservableCollection<Geometryfigure1> collection, Journal journal)
        {
            collection.CollectionCountChanged += (sender, args) =>
            {
                journal.AddRecord(collection.collectionName, args.ChangeType, args.Item?.ToString());
            };

            collection.CollectionReferenceChanged += (sender, args) =>
            {
                journal.AddRecord(collection.collectionName, args.ChangeType, args.Item?.ToString());
            };
        }

        // Подписка журнала 2 только на CollectionReferenceChanged обеих коллекций
        static void SubscribeJournal2(
            MyObservableCollection<Geometryfigure1> coll1,
            MyObservableCollection<Geometryfigure1> coll2,
            Journal journal)
        {
            Action<object, CollectionHandlerEventArgs> handler = (sender, args) =>
            {
                var coll = sender as MyObservableCollection<Geometryfigure1>;
                journal.AddRecord(coll?.collectionName ?? "Неизвестно", args.ChangeType, args.Item?.ToString());
            };

            coll1.CollectionReferenceChanged += handler;
            coll2.CollectionReferenceChanged += handler;
        }

        // Добавление случайного элемента
        static void AddElement(MyObservableCollection<Geometryfigure1> collection, string name)
        {
            T GenerateRandomFigure<T>() where T : Geometryfigure1
            {
                Random rand = new Random();
                int type = rand.Next(1, 4);
                return type switch
                {
                    1 => (T)(object)new Circle1(rand.Next(1, 11)),
                    2 => (T)(object)new Rectangle1(rand.Next(1, 11), rand.Next(1, 11)),
                    3 => (T)(object)new Parallelepiped1(rand.Next(1, 11), rand.Next(1, 11), rand.Next(1, 11)),
                    _ => throw new InvalidOperationException(),
                };
            }

            var figure = GenerateRandomFigure<Geometryfigure1>();
            collection.Add(figure);
            Console.WriteLine($"Добавлен элемент в {name}: {figure}");
        }

        // Удаление элемента по ключу
        static void RemoveElement(MyObservableCollection<Geometryfigure1> collection, string name)
        {
            Console.Write("Введите ключ для удаления: ");
            string key = Console.ReadLine();

            if (collection.ContainsKey(key))
            {
                collection.Remove(key);
                Console.WriteLine($"Элемент с ключом '{key}' удален из {name}.");
            }
            else
            {
                Console.WriteLine($"Элемент с ключом '{key}' не найден в {name}.");
            }
        }

        // Изменение элемента по ключу
        static void ModifyElement(MyObservableCollection<Geometryfigure1> collection, string name)
        {
            Console.Write("Введите ключ для изменения: ");
            string key = Console.ReadLine();

            if (collection.ContainsKey(key))
            {
                var newValue = new Circle1(new Random().Next(1, 11));
                collection[key] = newValue;
                Console.WriteLine($"Элемент с ключом '{key}' изменён в {name} на {newValue}.");
            }
            else
            {
                Console.WriteLine($"Элемент с ключом '{key}' не найден в {name}.");
            }
        }

        // Вывод содержимого коллекций
        static void DisplayCollections(
            MyObservableCollection<Geometryfigure1> coll1,
            MyObservableCollection<Geometryfigure1> coll2)
        {
            Console.WriteLine("\n--- Содержимое коллекции 1 ---");
            foreach (var item in coll1)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n--- Содержимое коллекции 2 ---");
            foreach (var item in coll2)
            {
                Console.WriteLine(item);
            }
        }
    }



