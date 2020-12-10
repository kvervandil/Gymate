using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gymate.App.Abstract;
using Gymate.Domain.Common;

namespace Gymate.App.Common
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        public List<T> Items { get; set; }

        public BaseService()
        {
            Items = new List<T>();
        }

        public int GetLastId()
        {
            return Items.Any() ? Items.OrderBy(p => p.Id).Last().Id : 0;
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public T GetItem(int id)
        {
            return Items.Find(i => i.Id == id);
        } 

        public int AddItem(T item)
        {
            Items.Add(item);

            return item.Id;
        }

        public int UpdateItem(T item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);

            if (entity != null)
            {
                entity = item;
            }

            return entity.Id;
        }

        public void RemoveItem(T item)
        {
            Items.Remove(item);
        }
    }
}
