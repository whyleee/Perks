using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Perks.Data
{
    /// <summary>
    /// Simple in-memory repository, which using <see cref="List{T}"/> internally and doesn't permanent.
    /// </summary>
    /// <typeparam name="T">The type of items in repository.</typeparam>
    public class InMemoryRepository<T> : IRepository<T> where T : class
    {
        protected readonly List<T> _list = new List<T>();

        /// <summary>
        /// Gets all items of the specified type from the storage.
        /// </summary>
        /// <returns>
        /// All items of the specified type from the storage.
        /// </returns>
        public virtual IQueryable<T> GetAll()
        {
            return _list.AsReadOnly().AsQueryable();
        }

        /// <summary>
        /// Gets the item from the storage by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>
        /// The item from the storage by ID.
        /// </returns>
        public virtual T Get(object id)
        {
            return _list.FirstOrDefault(x => GetId(x).Equals(id));
        }

        /// <summary>
        /// Adds the specified item to the storage or updates existing item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Save(T item)
        {
            if (_list.Any(x => EqualsById(x, item)))
            {
                Delete(item);
            }

            _list.Add(item);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public virtual void Delete(T item)
        {
            var itemInRepo = _list.FirstOrDefault(x => EqualsById(x, item));

            if (itemInRepo != null)
            {
                _list.Remove(itemInRepo);
            }
        }

        /// <summary>
        /// Determines whether provided items are equal by ID.
        /// </summary>
        /// <param name="item1">First item.</param>
        /// <param name="item2">Second item.</param>
        /// <returns><c>true</c> if item IDs are equal; otherwise - <c>false</c>.</returns>
        private bool EqualsById(object item1, object item2)
        {
            return GetId(item1).Equals(GetId(item2));
        }

        /// <summary>
        /// Gets item ID, trying to identificate it.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Item ID if found, or its hash code otherwise.</returns>
        protected virtual object GetId(object item)
        {
            var possibleIdPropertyNames = new[] {"Id", "Name", "Phone"};

            foreach (var idPropertyName in possibleIdPropertyNames)
            {
                var idProperty = item.GetType().GetProperty(idPropertyName, BindingFlags.Public | BindingFlags.Instance);

                if (idProperty != null)
                {
                    return idProperty.GetValue(item, null);
                }
            }

            return item.GetHashCode();
        }
    }
}
