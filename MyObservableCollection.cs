using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometryclass;
using lab;

namespace lab13
{
    public class MyObservableCollection<T> : MyHashTable<T> where T : Geometryfigure1
    {
        // Событие для изменения количества элементов (добавление/удаление)
        public event CollectionHandler CollectionCountChanged;

        // Событие для изменения ссылки на элемент в коллекции
        public event CollectionHandler CollectionReferenceChanged;
        // делегат
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
        // Защищённые методы для вызова событий
        protected virtual void OnCollectionCountChanged(CollectionHandlerEventArgs e)
        {
            CollectionCountChanged?.Invoke(this, e);
        }

        protected virtual void OnCollectionReferenceChanged(CollectionHandlerEventArgs e)
        {
            CollectionReferenceChanged?.Invoke(this, e);
        }
        // Переопределяем метод Add(T obj), чтобы бросать событие
        public void Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // Генерируем случайный ключ для объекта
            string key = GenerateRandomKey();
            base.Add(key, obj);

            // Вызываем событие
            OnCollectionCountChanged(new CollectionHandlerEventArgs("ItemAdded", obj));
        }
        // Переопределяем метод Remove(T obj), чтобы бросать событие
        public bool Remove(T obj)
        {
            foreach (var entry in table)
            {
                if (entry != null && !entry.IsDeleted && entry.Value.Equals(obj))
                {
                    bool result = base.Remove(entry.Key);
                    if (result)
                    {
                        OnCollectionCountChanged(new CollectionHandlerEventArgs("ItemRemoved", obj));
                        return true;
                    }
                }
            }
            return false;
        }
    }
}


