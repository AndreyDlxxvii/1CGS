using UnityEngine;
using Zenject;

namespace Code
{
    public interface IAbstractFactory
    {
        T Create<T>(T prefab) where T : MonoBehaviour;
    }

    public class AbstractFactory : IAbstractFactory
    {
        readonly DiContainer _container;

        public AbstractFactory(DiContainer container)
        {
            _container = container;
        }

        public T Create<T>(T prefab)
            where T : MonoBehaviour
        {
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }
    }
}