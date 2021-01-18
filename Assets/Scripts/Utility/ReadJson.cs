using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtility
{
    public class ReadJson
    {
        public static void WriteJson<T>(T t, string path) where T : new()
        {
            string str = Newtonsoft.Json.JsonConvert.SerializeObject(t);
            Debug.Log (str);
            ReadTxt.WriteInTxtByStream(path, str);
        }

        public static T ReadJsonData<T>(string path) where T : new()
        {
            string s = ReadTxt.ReadTxtByAllText(path);
            T t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
            return t;
        }


        public static List<T> ReadJsonArray<T>(string path) where T : new()
        {
            string s = ReadTxt.ReadTxtByAllText(path);
            Debug.LogFormat("{1} 读取信息: {0}", s , path );
            List<T> t = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(s);
            return t;
        }
        
    }
}