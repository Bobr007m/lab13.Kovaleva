using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometryclass;
using MyCollectionH;

namespace lab13
{
    // делегат
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args); //source — объект, вызвавший событие, args — дополнительная информация о событии
    public class MyObservableCollection<T> : MyHashTable<T> where T : Geometryfigure1
    {
        // События
        public event CollectionHandler CollectionCountChanged;//вызывается при добавлении или удалении элемента.
        public event CollectionHandler CollectionReferenceChanged;//вызывается при замене ссылки на элемент по ключу.

        // Поле журнала
        public readonly Journal journal = new Journal();
        public readonly string collectionName;
        //  методы для вызова событий
        public virtual void OnCollectionCountChanged(CollectionHandlerEventArgs e)//Если есть подписчики на событие CollectionCountChanged, то оно вызывается с аргументами e.
        {
            CollectionCountChanged?.Invoke(this, e);//используется для вызова всех подписанных методов (обработчиков) на это событие или делегат
        }

        public virtual void OnCollectionReferenceChanged(CollectionHandlerEventArgs e)
        {
            CollectionReferenceChanged?.Invoke(this, e);
        }
        // Переопределяем метод Add(T obj), чтобы бросать событие
        public override void Add(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // Генерируем случайный ключ для объекта
            string key = GenerateRandomKey();
            base.Add(key, obj);

            // Вызываем событие
            OnCollectionCountChanged(new CollectionHandlerEventArgs("ItemAdded", obj));//Вызывает событие CollectionCountChanged с информацией о добавлении.
        }
        // Переопределяем метод Remove(T obj), чтобы бросать событие
        public override bool Remove(T obj)
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


