using DimensionShifters.Enemies;
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
            while (true)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
                EnemyAI enemyPrefab = _enemyDict[1][0];
                EnemyAI newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                newEnemy.transform.parent = transform;
                yield return new WaitForSeconds(5f);
            }
        }
    }
}