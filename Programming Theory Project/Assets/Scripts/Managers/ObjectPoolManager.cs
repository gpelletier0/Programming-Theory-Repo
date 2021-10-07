using System.Collections.Generic;
using Factories;
using Pools;
using Singletons;
using UnityEngine;

namespace Managers
{
    public class ObjectPoolManager : SceneSingleton<ObjectPoolManager>
    {
        public ObjectPool[] objectPools;

        private Dictionary<string, ObjectPool> _goDictionary;
        
        private void Start()
        {
            _goDictionary = new Dictionary<string, ObjectPool>();
            
            foreach (var objectPool in objectPools)
            {
                var factory = new PrefabFactory(objectPool.prefab, transform);
                objectPool.Pool = new PrefabPool(factory, objectPool.poolSize);
                _goDictionary.Add(objectPool.prefab.name.ToLower(), objectPool);
            }
        }

        /// <summary>
        /// Returns first game object available in pool
        /// </summary>
        /// <param name="prefabName">name of the prefab object pool</param>
        /// <returns>game object or null</returns>
        public GameObject GetFirst(string prefabName) =>
            _goDictionary.ContainsKey(prefabName.ToLower()) ? _goDictionary[prefabName].Pool.GetFirst() : null;
    }
}