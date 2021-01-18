using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityUtility
{
    public static class Utility
    {
        /// <summary>
        /// 获取物体的根节点
        /// </summary>
        public static T GetRoot<T>(T children) where T : Transform
        {
            T t = (T)children.parent;

            if (t.parent != null)
            {
                return GetRoot((T)t.parent);
            }
            else
            {
                return t;
            }
        }

        ///<summary>
        ///删除数组元素
        ///</summary> 
        public static T[] DeleteArray<T>(T[] array, int index, int deleteLenght = 1)
        {
            if (deleteLenght < 0) return array;

            if (index == 0 && deleteLenght >= array.Length)
            {
                deleteLenght = array.Length;
            }
            else if ((index + deleteLenght) >= array.Length)
            {
                deleteLenght = array.Length - index - 1;
            }

            T[] tempArray = new T[array.Length - deleteLenght];

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (i >= index)
                {
                    tempArray[i] = array[i + deleteLenght];
                }
                else
                {
                    tempArray[i] = array[i];
                }
            }
            return tempArray;
        }

        /// <summary>
        /// 插入数组元素
        /// </summary>
        public static T[] InsertArray<T>(T[] array, T value, int index)
        {
            if (index > array.Length)
            {
                Debug.LogError($"index{index} > array.Length{array.Length}");
                return array;
            }
            List<T> list = new List<T>(array);
            list.Insert(index, value);
            return list.ToArray();
        }
    }
}