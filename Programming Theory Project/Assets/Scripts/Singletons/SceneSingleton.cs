using UnityEngine;

namespace Singletons
{
    public abstract class SceneSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this as T;
            else
                Destroy(gameObject);

            Initialize();
        }

        protected virtual void Initialize() { }
    }
}