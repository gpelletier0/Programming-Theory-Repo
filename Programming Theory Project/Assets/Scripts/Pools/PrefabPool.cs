using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Factories;
using UnityEngine;

namespace Pools
{
    public class PrefabPool : IEnumerable
    {
        private readonly HashSet<GameObject> _goPool;
        private readonly PrefabFactory _factory;
        private readonly int _poolSize;
        
        public PrefabPool(PrefabFactory factory, int poolSize)
        {
            _poolSize = poolSize;
            _factory = factory;
            _goPool = new HashSet<GameObject>();
            
            CreatePoolSize();
        }

        /// <summary>
        /// Creates a new factory pattern for game object and add to pool
        /// </summary>
        private void Create()
        {
            GameObject go = _factory.Create();
            _goPool.Add(go);
        }

        /// <summary>
        /// Creates _poolSize number of factory patterns
        /// </summary>
        private void CreatePoolSize()
        {
            for (var i = 0; i < _poolSize; i++)
                Create();
        }

        /// <summary>
        /// Returns first game object that is not active in hierarchy
        /// </summary>
        /// <returns>game object or null</returns>
        public GameObject GetFirst() => _goPool.FirstOrDefault(o => !o.activeInHierarchy);
        /*
        {
            if (_goPool.Count(o => !o.activeInHierarchy) <= 0)
                CreatePoolSize();
            
            return _goPool.FirstOrDefault(o => !o.activeInHierarchy);
        }*/
        
        public IEnumerator GetEnumerator() => _goPool.GetEnumerator();
    }
}