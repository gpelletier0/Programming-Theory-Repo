using UnityEngine;

namespace Factories
{
    public class PrefabFactory
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly string _name;
        private int _index;
        
        /// <summary>
        /// Factory pattern for prefabs
        /// </summary>
        /// <param name="prefab">prefab</param>
        /// <param name="parent">parent transform</param>
        public PrefabFactory(GameObject prefab, Transform parent = null)
        {
            _prefab = prefab;
            _name = prefab.name;

            _parent = new GameObject().transform;
            _parent.name = prefab.name;
            
            if(parent)
                _parent.parent = parent;
        }

        /// <summary>
        /// Instantiates a new game object
        /// </summary>
        /// <returns>new game object</returns>
        public GameObject Create()
        {
            GameObject gameObject = GameObject.Instantiate(_prefab, _parent);
            gameObject.name = _name + _index;
            _index++;
            
            return gameObject;
        }
    }
}