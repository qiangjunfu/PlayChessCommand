using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace UnityUtility
{
    public class SingleTon<T> where T : SingleTon<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var ctor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new System.Type[0], null);

                    if (ctor == null)
                    {
                        throw new NullReferenceException("这个类必须有一个私有的无参的构造函数!!!");
                    }

                    instance = (T)ctor.Invoke(null);
                }

                return instance;
            }
        }

    }
}