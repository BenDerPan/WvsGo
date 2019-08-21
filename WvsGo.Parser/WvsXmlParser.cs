using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WvsGo.Parser.Structures;

namespace WvsGo.Parser
{
    /// <summary>
    /// WVS扫描结果xml文件解析器实现类
    /// </summary>
    public class WvsXmlParser
    {
        /// <summary>
        /// 将WVS导出的xml扫描结果字符串反序列化成WVS扫描结果对象
        /// </summary>
        /// <param name="wvsXmlResult">WVS xml格式扫描结果</param>
        /// <returns>WVS扫描结果对象</returns>
        public static ScanGroup Parse(string wvsXmlResult)
        {
            return XmlParser.Deserialize<ScanGroup>(wvsXmlResult);
        }

        /// <summary>
        /// 尝试将WVS导出的xml扫描结果字符串反序列化成WVS扫描结果对象
        /// </summary>
        /// <param name="wvsXmlResult">WVS xml格式扫描结果</param>
        /// <param name="wvsResultObj">WVS扫描结果对象</param>
        /// <returns>成功返回true，否则返回false</returns>
        public static bool TryParse(string wvsXmlResult,out ScanGroup wvsResultObj)
        {
            wvsResultObj = null;
            try
            {
                wvsResultObj = Parse(wvsXmlResult);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 从WVS扫描结果xml文件中加载扫描结果对象
        /// </summary>
        /// <param name="filePath">结果xml文件路径</param>
        /// <returns>WVS扫描结果对象</returns>
        public static ScanGroup LoadFromFile(string filePath)
        {
            using (FileStream stream=new FileStream(filePath,FileMode.Open,FileAccess.Read))
            {
                using (StreamReader reader=new StreamReader(stream))
                {
                    var xmlResult = reader.ReadToEnd();
                    return Parse(xmlResult);
                }
            }
        }
    }
}
