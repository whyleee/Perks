using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Perks.Data.Xml
{
    /// <summary>
    /// Simple XML-file based repository with permanent data persistence.
    /// </summary>
    /// <typeparam name="T">The type of items in repository.</typeparam>
    public class XmlStorageRepository<T> : IRepository<T> where T : class
    {
        protected readonly string _xmlPath;
        protected readonly XmlService _xmlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlStorageRepository{T}" /> class.
        /// </summary>
        /// <param name="xmlPath">The path to the XML file for storage use.</param>
        public XmlStorageRepository(string xmlPath, XmlService xmlService)
        {
            Ensure.ArgumentNotNullOrEmpty(xmlPath, "xmlPath");
            Ensure.ArgumentNotNull(xmlService, "xmlService");

            _xmlPath = xmlPath;
            _xmlService = xmlService;

            _xmlService.CreateIfNotExists(_xmlPath);
        }

        /// <summary>
        /// Gets all items of the specified type from the storage.
        /// </summary>
        /// <returns>All items of the specified type from the storage.</returns>
        public virtual IQueryable<T> GetAll()
        {
            return _xmlService.ToCollectionOf<T>(_xmlPath).AsQueryable();
        }

        /// <summary>
        /// Gets the item from the storage by ID.
        /// </summary>
        /// <param name="id">The ID of the item.</param>
        /// <returns>The item from the storage by ID.</returns>
        public virtual T Get(object id)
        {
            return GetElement(id).IfNotNull(e => _xmlService.To<T>(e));
        }

        /// <summary>
        /// Adds the specified item to the storage or updates existing item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Save(T item)
        {
            var element = _xmlService.ToXml(item);
            var doc = _xmlService.Load(_xmlPath);

            // TODO: update existing element if exists.

            doc.Root.Add(element);
            _xmlService.Save(doc, _xmlPath);
        }

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        public virtual void Delete(T item)
        {
            var doc = _xmlService.Load(_xmlPath);
            var id = ((dynamic) item).Id;
            var element = GetElement((object) id, doc);

            if (element == null)
            {
                throw new ArgumentException(string.Format("No item with ID '{0}' found in repository", id));
            }

            element.Remove();
            _xmlService.Save(doc, _xmlPath);
        }

        /// <summary>
        /// Gets the element from XML file by its ID.
        /// </summary>
        /// <param name="id">Element ID.</param>
        /// <param name="doc">XML document.</param>
        /// <returns><see cref="XElement"/> got by ID from XML file.</returns>
        /// <remarks>
        /// XML element should have child "Id" element to be found.
        /// </remarks>
        protected virtual XElement GetElement(object id, XDocument doc = null)
        {
            if (doc == null)
            {
                doc = _xmlService.Load(_xmlPath);
            }

            return doc.Root.Elements().FirstOrDefault(x => x.Element("Id").Value == id.ToString());
        }
    }
}
