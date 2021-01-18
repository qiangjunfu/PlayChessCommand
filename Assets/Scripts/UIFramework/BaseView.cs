using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UIFramework.MVVM
{
    public abstract class BaseView<VM, M> : MonoBehaviour where VM : IViewModel<M> where M : IModel
    {
       public  virtual  VM ViewModel { get; } 
        public abstract void BindViewModel(VM viewModel);

    }

}