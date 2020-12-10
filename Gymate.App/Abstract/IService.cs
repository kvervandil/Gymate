using System;
using System.Collections.Generic;

namespace Gymate.App.Abstract
{
    public interface IService<T>
    {
        List<T> Items { get; set; }

        List<T> GetAllItems();

        T GetItem(int id);

        int AddItem(T item);

        int UpdateItem(T item);

        void RemoveItem(T item);

        int GetLastId();
    }
}