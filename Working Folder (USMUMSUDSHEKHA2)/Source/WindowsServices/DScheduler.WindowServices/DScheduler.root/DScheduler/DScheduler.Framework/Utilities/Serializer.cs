using System.IO;
using System.Xml.Serialization;

namespace DScheduler.Framework
{
    /// <summary>
    ///
    /// </summary>
    public class Serializer
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Deserialize<T>(string filePath)
        {
            T result;

            if (File.Exists(filePath))
            {
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                var serializer = new XmlSerializer(typeof(T));

                // To read the file, create a FileStream.
                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    // Call the Deserialize method and cast to the object type.

                    result = (T)serializer.Deserialize(fileStream);
                }
            }
            else
            {
                result = default(T);
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void Serialize<T>(T entity, string filePath)
        {
            // Construct an instance of the XmlSerializer with the type
            // of object that is being deserialized.
            var serializer = new XmlSerializer(typeof(T));

            // To read the file, create a FileStream.
            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                // Call the Deserialize method and cast to the object type.
                serializer.Serialize(fileStream, entity);
            }
        }
    }
}