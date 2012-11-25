using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Perks.Data.Xml
{
    /// <summary>
    /// The service to work with XML.
    /// </summary>
    public class XmlService
    {
        protected readonly IFileStorage _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlService" /> class.
        /// </summary>
        public XmlService(IFileStorage storage)
        {
            Ensure.ArgumentNotNull(storage, "storage");

            _storage = storage;
        }

        /// <summary>
        /// Creates an empty XML file if it not exists.
        /// </summary>
        /// <param name="uri">The URI to resource.</param>
        public virtual void CreateIfNotExists(string uri)
        {
            if (!_storage.FileExists(uri))
            {
                _storage.WriteFile(uri, string.Format(@"<?xml version=""1.0""?>{0}<root>{0}</root>", Environment.NewLine));
            }
        }

        /// <summary>
        /// Loads XML from the resource by specified URI.
        /// </summary>
        /// <param name="uri">The URI to resource.</param>
        /// <returns><see cref="XDocument"/> with XML DOM.</returns>
        public virtual XDocument Load(string uri)
        {
            using (var file = _storage.OpenRead(uri))
            {
                return XDocument.Load(file);
            }
        }

        /// <summary>
        /// Opens the file to read XML by specified URI.
        /// </summary>
        /// <param name="uri">The URI to resource.</param>
        /// <returns><see cref="XDocument"/> with XML DOM.</returns>
        public virtual XmlReader Open(string uri)
        {
            var settings = new XmlReaderSettings
            {
                CloseInput = true
            };

            return XmlReader.Create(_storage.OpenRead(uri), settings);
        }

        /// <summary>
        /// Saves the specified <see cref="XDocument"/> by specified URI.
        /// </summary>
        /// <param name="doc">The document to save.</param>
        /// <param name="uri">The URI to save.</param>
        public virtual void Save(XDocument doc, string uri)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                CloseOutput = true
            };

            using (var writer = XmlWriter.Create(_storage.OpenWrite(uri), settings))
            {
                doc.WriteTo(writer);
            }
        }

        /// <summary>
        /// Converts specified object to <see cref="XElement"/>.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <returns><see cref="XElement"/> containing XML of provided object.</returns>
        public virtual XElement ToXml(object o)
        {
            var output = new StringBuilder();

            using (var writer = XmlWriter.Create(output))
            {
                var serializer = new XmlSerializer(o.GetType());

                var dummyNamespace = new XmlSerializerNamespaces();
                dummyNamespace.Add(string.Empty, string.Empty);

                serializer.Serialize(writer, o, dummyNamespace);
            }

            return XElement.Parse(output.ToString());
        }

        /// <summary>
        /// Converts XML to the typed object.
        /// </summary>
        /// <typeparam name="T">The type of result object.</typeparam>
        /// <param name="from"><see cref="XElement"/> with object XML.</param>
        /// <returns>Typed object got from provided XML.</returns>
        public virtual T To<T>(XElement from)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(from.ToString());

            return (T) serializer.Deserialize(reader);
        }

        /// <summary>
        /// Reads XML resource as a collection of typed objects.
        /// </summary>
        /// <typeparam name="T">The type of objects in result collection.</typeparam>
        /// <param name="uri">The URI to resource.</param>
        /// <returns>Collection of typed objects got from XML.</returns>
        public virtual IList<T> ToCollectionOf<T>(string uri)
        {
            using (var reader = Open(uri))
            {
                return ToCollectionOf<T>(reader);
            }
        }

        /// <summary>
        /// Reads XML source as a collection of typed objects.
        /// </summary>
        /// <typeparam name="T">The type of objects in result collection.</typeparam>
        /// <param name="reader">Reader for XML.</param>
        /// <returns>Collection of typed objects got from XML.</returns>
        public virtual IList<T> ToCollectionOf<T>(XmlReader reader)
        {
            var collectionType = typeof(List<>).MakeGenericType(typeof(T));
            var serializer = new XmlSerializer(collectionType, new XmlRootAttribute("root") {Namespace = ""});

            return (IList<T>) serializer.Deserialize(reader);
        }
    }
}
