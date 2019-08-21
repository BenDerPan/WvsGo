using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace WvsGo.Parser
{
    /// <summary>
    /// xml序列化/反序列化帮助类
    /// </summary>
    public class XmlParser
    {
        /// <summary>
        /// 将对象序列化成xml字符串
        /// </summary>
        /// <typeparam name="T">需要序列化的对象类型</typeparam>
        /// <param name="obj">需要序列化的对象实例</param>
        /// <returns>xml字符串</returns>
        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                serializer.Serialize(writer, obj);
                string xml = writer.ToString();
                return xml;
            }
        }

        /// <summary>
        /// 将xml格式字符串反序列化成对象实例
        /// </summary>
        /// <typeparam name="T">需要反序列化的对象类型</typeparam>
        /// <param name="xml">需要反序列化的xml格式字符串</param>
        /// <returns>反序列化后得到的对象实例</returns>
        public static T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xml))
            {
                T result = (T)(serializer.Deserialize(reader));
                return result;
            }
        }
    }
}
