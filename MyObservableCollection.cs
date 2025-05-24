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
        // События
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        // Поле журнала
        private readonly Journal _journal = new Journal();
        private readonly string _collectionName;
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
        // Переопределяем индексатор
        public T this[string key]
        {
            get => base[key];
            set
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                if (value == null) throw new ArgumentNullException(nameof(value));

                // Проверяем, существует ли такой ключ
                if (ContainsKey(key))
                {
                    T oldValue = base[key];

                    // Проверяем, действительно ли изменилась ссылка
                    if (!object.ReferenceEquals(oldValue, value))
                    {
                        // Вызываем базовый сеттер
                        base[key] = value;

                        // Генерируем событие об изменении ссылки
                        OnCollectionReferenceChanged(new CollectionHandlerEventArgs("ItemModified", value));
                    }
                }
                else
                    // Если ключа нет — добавляем новый элемент
                    base[key] = value;
            }
        }
    }
}


