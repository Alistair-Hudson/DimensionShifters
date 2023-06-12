using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Enemies
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WorldEnemyList")]
    public class WorldEnemyList : ScriptableObject
    {
        [System.Serializable]
        public struct EnemyData
        {
            public int Level;
            public EnemyAI Enemy;
        }

        [SerializeField]
        private List<EnemyData> _enemyList = new List<EnemyData>();
        public List<EnemyData> EnemyList { get => _enemyList; }
    }
}