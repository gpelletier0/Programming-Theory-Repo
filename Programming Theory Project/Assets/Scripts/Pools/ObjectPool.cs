using System;
using UnityEngine;

namespace Pools
{
    [Serializable]
    public class ObjectPool
    {
        public PrefabPool Pool;
        public GameObject prefab;
        public int poolSize;
    }
}