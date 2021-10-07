using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        void Spawn(Vector3 position);
        IEnumerator DeSpawnCoroutine(float time);
    }
}