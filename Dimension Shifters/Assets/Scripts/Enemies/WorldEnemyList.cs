using DimensionShifters.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DimensionShifters.Enemies
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WorldEnemyList")]
    public class WorldEnemyList : ScriptableObject
    {
        [System.Serializable]
        public struct EnemyData
        {
            public int Level;
            public EnemyAI EnemyBasePrefab;
            public GenericWeapon Weapon;
            public float AtackRange;
            public float MaxRunningSpeed;
            public int EnemyHealth;
            public int PointsValue;
        }

        [SerializeField]
        private List<EnemyData> _enemyList = new List<EnemyData>();
        public List<EnemyData> EnemyList { get => _enemyList; }

        private Dictionary<int, List<EnemyData>> _enemyDict;

        public EnemyData SpawnEnemy(int level)
        {
            if (_enemyDict == null)
            {
                SetupDictionary();
            }

            int enemySeletionList = Random.Range(1, level);
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
            EnemyData enemy = _enemyDict[enemySeletionList][Random.Range(0, _enemyDict[enemySeletionList].Count)];

            return enemy;
        }

        private void SetupDictionary()
        {
            _enemyDict = new Dictionary<int, List<EnemyData>>();
            foreach (var enemy in _enemyList)
            {
                if (!_enemyDict.ContainsKey(enemy.Level))
                {
                    _enemyDict.Add(enemy.Level, new List<EnemyData>());
                }
                _enemyDict[enemy.Level].Add(enemy);
            }
        }
    }
}