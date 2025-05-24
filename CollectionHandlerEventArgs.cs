using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab;

namespace lab13
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        // Свойство: тип изменения 
        public string ChangeType { get; }

        // Свойство: ссылка на объект, связанный с событием
        public object Item { get; }

        // Конструктор
        public CollectionHandlerEventArgs(string changeType, object item)
        {
            ChangeType = changeType ?? throw new ArgumentNullException(nameof(changeType));
            Item = item;
        }
    }
}
