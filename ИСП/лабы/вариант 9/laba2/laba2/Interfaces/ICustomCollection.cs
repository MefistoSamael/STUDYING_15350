using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba2.Interfaces
{
    public interface ICustomCollection<A>
    {
        A this[int index] { get; set; } // индексатор коллекции
        void Reset(); // устанавливает курсор в начало
        void Next(); // перемещает курсор на следующий элемент
        A Current(); // возвращает элемент текущего положения курсора
        int Count { get; } // возвращает количество элементов коллекции
        void Add(A item); // добавляет объект item в конец коллекции
        void Remove(A item); // удаляет объект item из коллекции
        void RemoveCurrent(); // удаляет элемент текущего положения курсора
    }
}
