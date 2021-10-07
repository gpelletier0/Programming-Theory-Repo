using System.Collections;
using System.Linq;
using Factories;
using Pools;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyControllerPool[] enemyPools;
        public Transform[] spawnPoints;

        private void Start()
        {
            if (enemyPools is null || enemyPools.Length == 0)
                Debug.LogError($"{nameof(enemyPools)} {Constants.IsNotSet}");
            else if (spawnPoints == null || spawnPoints.Length == 0 || spawnPoints.Count(x => x == null) > 0)
                Debug.LogError($"{nameof(spawnPoints)} {Constants.IsNotSet}");
            else
            {
                foreach (var enemyPool in enemyPools)
                {
                    var factory = new PrefabFactory(enemyPool.prefab, transform);
                    enemyPool.Pool = new PrefabPool(factory, enemyPool.poolSize);

                    StartCoroutine(SpawnEnemies(enemyPool));
                }
            }
        }

        /// <summary>
        /// Spawn Zombies Coroutine
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpawnEnemies(EnemyControllerPool enemyControllerPool)
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(enemyControllerPool.spawnTime);
                
                /*
                var enemy = enemyControllerPool.Pool.GetFirst().GetComponent<EnemyController>();
                if (enemy)
                {
                    var position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    enemy.Spawn(position);
                    enemy.OnAddPoints += OnAddPoints;
                }*/
            }
        }

        /// <summary>
        /// Adds points to player score
        /// </summary>
        /// <param name="points">enemy points</param>
        private void OnAddPoints(int points) => GameManager.Instance.AddPoints(points);
    }
}