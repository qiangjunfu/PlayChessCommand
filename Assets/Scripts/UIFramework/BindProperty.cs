using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFramework.MVVM
{
    [System.Serializable]
    public class BindProperty<T>
    {
        public delegate void ValueChangedDelegate(T oldValue, T newValue);
        private ValueChangedDelegate OnValueChanged;

        public BindProperty() { }


        [SerializeField] private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                //if (!Equals(_value, value)){}
                T old = _value;
                _value = value;
                ValueChanged(old, _value);
            }
        }
        private void ValueChanged(T oldValue, T newValue)
        {
            if (OnValueChanged != null)
            {
                OnValueChanged(oldValue, newValue);
            }
        }

        public void AddListener(ValueChangedDelegate valueChangedDelegate)
        {
            if (OnValueChanged == null)
            {
                OnValueChanged += valueChangedDelegate;
            }
            else
            {
                Debug.LogErrorFormat("已经存在该类型:{0} 绑定的回调函数", valueChangedDelegate.GetType());
            }
        }

        public void RemoveListener(ValueChangedDelegate valueChangedDelegate)
        {
            if (OnValueChanged != null)
            {
                if (OnValueChanged.GetType() != valueChangedDelegate.GetType())
                {
                    Debug.LogErrorFormat("RemoveListener(): {0} != {1} ", OnValueChanged.GetType(), valueChangedDelegate.GetType());
                    return;
                }
                OnValueChanged -= valueChangedDelegate;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.AppendFormat("Value == ", Value);
            return str.ToString();
        }
    }

}