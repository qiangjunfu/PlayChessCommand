using UnityEngine;


namespace UIFramework.MVVM
{
    public interface IViewModel<M> where M : IModel
    {
        M Model { get; }

    }
}