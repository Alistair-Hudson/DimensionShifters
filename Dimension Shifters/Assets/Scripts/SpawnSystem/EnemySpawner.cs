using DimensionShifters.Enemies;
using DimensionShifters.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.SpawnSystem
{
    public class EnemySpawner : MonoBehaviour
    {
        [System.Serializable]
        public struct EnemyData
        {
            public int Level;
            public EnemyAI Enemy;
        }

        [SerializeField]
        private GameObject _warpInPrefab = null;
        [SerializeField]
        private List<EnemyData> _enemyList = new List<EnemyData>();

        private Dictionary<int, List<EnemyAI>> _enemyDict = new Dictionary<int, List<EnemyAI>>();

        private void Awake()
        {
            foreach (var enemy in _enemyList)
            {
                if (!_enemyDict.ContainsKey(enemy.Level))
                {
                    _enemyDict.Add(enemy.Level, new List<EnemyAI>());
                }
                _enemyDict[enemy.Level].Add(enemy.Enemy);
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            while (true)
            {
                int difficultyLevel = PlayerScoreDisplay.Score / 1000 + 2;
                Vector3 spawnPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                int enemySeletionList = Random.Range(1, difficultyLevel);
                if (!_enemyDict.ContainsKey(enemySeletionList))
                {
                    int max = 1;
                    foreach (var key in _enemyDict.Keys)
                    {
                        if (key >= max && key <= enemySeletionList)
                        {
                            max = key;
                        }
                    }
                    enemySeletionList = max;
                }
                EnemyAI enemyPrefab = _enemyDict[enemySeletionList][Random.Range(0, _enemyDict[enemySeletionList].Count)];
                yield return Spawn(enemyPrefab, spawnPoint);
                yield return new WaitForSeconds(5f);
            }
        }

        private IEnumerator Spawn(EnemyAI enemyPrefab, Vector3 spawnPoint)
        {
            var newWarp = Instantiate(_warpInPrefab, spawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(1);
            //var animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 2);
            //float time = 0;
            //while(time < 1)
            //{
            //    var t = animationCurve.Evaluate(time);
            //    newWarp.transform.position = new Vector3(newWarp.transform.position.x, t, newWarp.transform.position.z);
            //    yield return null;
            //    time += Time.deltaTime;
            //}
            EnemyAI newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = transform;
            yield return null;
            Destroy(newWarp.gameObject);
        }
    }
}