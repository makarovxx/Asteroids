using System;
using UnityEngine;

namespace Project.Scripts.Plugins
{
    public interface ICreator<T> where T : MonoBehaviour
    {
        public T Create();
    }
}