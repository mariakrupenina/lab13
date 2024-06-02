using library_for_lab10;
using library_for_lab12;
using System;
using System.Collections;
using System.Collections.Generic;

namespace lab13
{
    //Делегат для обработки событий изменения коллекции
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);


    public class MyObservableCollection<T> : MyCollection<T> where T : Tool, IInit, ICloneable, new()
    {
        public string CollectionName { get; }

        //конструкторы
        #region

        public MyObservableCollection(string collectionName, int size) : base(size)
        {
            CollectionName = collectionName;
        }

        public MyObservableCollection() : base() { }

        public MyObservableCollection(int size) : base(size) { }

        public MyObservableCollection(T[] collection) : base(collection) { }
        #endregion

        //События
        public event CollectionHandler CollectionCountChanged; //при добавлении нового элемента в коллекцию или при удалении элемента из коллекции
        public event CollectionHandler CollectionReferenceChanged; //когда одной из ссылок присваивается новое значение

        //в соответствующих методах или свойствах класса генерируются события
        #region

        //Метод для вызова события изменения количества элементов в коллекции
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            //if (creatCollectionCountChangedeNumber != null)
            //    CollectionCountChanged(this, args);

            CollectionCountChanged?.Invoke(source, args);
        }
        //Метод для вызова события изменения ссылки на элемент
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(source, args);
        }

        #endregion

        //переопределённые методы
        #region
        public override void AddToBegin(T item)
        {
            base.AddToBegin(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "элемент добавлен в начало", item));
        }
        public override void AddToEnd(T item)
        {
            base.AddToEnd(item);
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "элемент добавлен в конец", item));
        }

        public override bool Remove(T item)
        {
            bool removed = base.Remove(item);
            if (removed)
            {
                OnCollectionCountChanged(this, new CollectionHandlerEventArgs(CollectionName, "элемент удалён", item));
            }
            return removed;
        }

        public new T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return base[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                T oldItem = base[index];
                base[index] = value;
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(CollectionName, "ссылка на элемент изменена", value));
            }
        }
        #endregion
    }
}
