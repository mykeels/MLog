using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace MLog.Core
{
    public static class XmlExtensions
    {
        public static XElement ToXElement<T>(this T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                xs.Serialize(ms, obj);
                ms.Position = 0;
                using (XmlReader reader = XmlReader.Create(ms))
                {
                    XElement element = XElement.Load(reader);
                    return element;
                }
            }
        }

        public static T ToObject<T>(this XElement xml)
        {
            T obj = System.Activator.CreateInstance<T>();
            var serializer = new XmlSerializer(typeof(T));
            obj = (T)serializer.Deserialize(xml.CreateReader());
            return obj;
        }
    }
}
