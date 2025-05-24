using Geometryclass;
using lab13;
using System;

public class Program
{
    public static void Main()
    {
        // 1. Создаем две наблюдаемые коллекции с именами
        var figures1 = new MyObservableCollection<Geometryfigure1>("Figures1");
        var figures2 = new MyObservableCollection<Geometryfigure1>("Figures2");

        // 2. Создаем журналы
        var fullJournal = new Journal(); // Записывает все события
        var refJournal = new Journal();  // Записывает только изменения ссылок

        // 3. Подписываем журналы на события
        // Первый журнал подписан на все события из первой коллекции
        figures1.CollectionCountChanged += (s, e) =>
            fullJournal.AddEntry(figures1.CollectionName, e.ChangeType, e.ChangedItem);
        figures1.CollectionReferenceChanged += (s, e) =>
            fullJournal.AddEntry(figures1.CollectionName, e.ChangeType, e.ChangedItem);

        // Второй журнал подписан на изменения ссылок в обеих коллекциях
        figures1.CollectionReferenceChanged += (s, e) =>
            refJournal.AddEntry(figures1.CollectionName, e.ChangeType, e.ChangedItem);
        figures2.CollectionReferenceChanged += (s, e) =>
            refJournal.AddEntry(figures2.CollectionName, e.ChangeType, e.ChangedItem);

        // 4. Работа с коллекциями

        // Добавляем элементы
        figures1.Add("circle1", new Circle1(5));
        figures1.Add("rect1", new Rectangle1(3, 4));
        figures2.Add("paral1", new Parallelepiped1(2, 3, 4));

        // Удаляем элемент
        figures1.Remove("circle1");

        // Изменяем элементы
        figures2["paral1"] = new Parallelepiped1(5, 5, 5);
        figures1["rect1"] = new Rectangle1(10, 10);

        // 5. Выводим результаты

        Console.WriteLine("=== Full Journal (all changes in Figures1) ===");
        fullJournal.PrintEntries();

        Console.WriteLine("\n=== Reference Journal (reference changes in both collections) ===");
        refJournal.PrintEntries();
    }
}
