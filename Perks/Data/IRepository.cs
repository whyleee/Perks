using System.Linq;

namespace Perks.Data
{
    /// <summary>
    /// Represents any kind of storage and provides simple collection-like API for it.
    /// </summary>
    /// <typeparam name="T">The type of items in repository.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all items of the specified type from the storage.
        /// </summary>
        /// <returns>All items of the specified type from the storage.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets the item from the storage by ID.
        /// </summary>
        /// <returns>The item from the storage by ID.</returns>
        T Get(object id);

        /// <summary>
        /// Adds the specified item to the storage or updates existing item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Save(T item);

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        void Delete(T item);
    }
}