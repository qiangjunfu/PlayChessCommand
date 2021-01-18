using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace UnityUtility
{
    public class ReadTxt
    {
        public static void WriteInTxtByStream(string path, string str)
        {
            if (!File.Exists(path))
            {
                Debug.LogError("路径不存在 创建新路径");
                try
                {
                    //File.Create(path).Dispose();
                    File.Create(path);
                }
                catch (System.Exception  e)
                {
                    Debug.Log($"创建文件 {path} 异常: {e}");
                }
            }
            StreamWriter sw;
            FileInfo fileInfo = new FileInfo(path);
            //sw = fileInfo.AppendText();  //追加
            sw = fileInfo.CreateText();    //覆盖
            sw.WriteLine(str);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        public static void WriteInTxtByAllLines(string path, string[] s)
        {
            File.WriteAllLines(path, s);
        }


        public static List<string> ReadTxtByStream(string path)
        {
            List<string> info = new List<string>();
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                info.Add(s);
            }
            sr.Dispose();
            sr.Close();

            return info;
        }
        public static string ReadTxtByAllText(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogErrorFormat ("路径不存在: {0}",path );
                return "";
            }
            //string str = Resources.Load<TextAsset>(path).text.ToString();
            string str = File.ReadAllText(path, System.Text.Encoding.UTF8);
            return str;
        }
        public static string[] SplitColumn(string path)
        {
            string[] strs = File.ReadAllLines(path);
            return strs;
        }
        public static string[] SplitRow(string s)
        {
            string[] strs = System.Text.RegularExpressions.Regex.Split(s, "\\s+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //for (int i = 0; i < strs.Length; i++)
            //{
            //    Debug.Log(i + " " + strs[i]);
            //}
            return strs;
        }
    }
}