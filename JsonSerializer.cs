using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SedentaryCountdown
{
    public static class JsonSerializer
    {
        /// <summary>
        /// Json序列化，将Class转换成JsonString
        /// <para>encoding为字符串编码，默认(为null时)是Encoding.UTF8</para>
        /// </summary>
        public static string JsonSerialize<T>(object obj, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, (T)obj);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms, encoding);
            string ret = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            return ret;
        }

        /// <summary>
        /// Json反序列化，将JsonString转换成Class
        /// <para>encoding为字符串编码，默认(为null时)是Encoding.UTF8</para>
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(encoding.GetBytes(jsonString));
            T jsonObject = (T)serializer.ReadObject(ms);
            ms.Close();
            return jsonObject;
        }

        /// <summary>
        /// Json序列化，将Class转换成JsonString
        /// <para>encoding为字符串编码，默认(为null时)是Encoding.UTF8</para>
        /// </summary>
        public static string TryJsonSerialize<T>(object obj, Encoding encoding = null)
        {
            try
            {
                return JsonSerialize<T>(obj, encoding);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Json反序列化，将JsonString转换成Class
        /// <para>encoding为字符串编码，默认(为null时)是Encoding.UTF8</para>
        /// </summary>
        public static T TryJsonDeserialize<T>(string jsonString, Encoding encoding = null)
        {
            try
            {
                return JsonDeserialize<T>(jsonString, encoding);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
