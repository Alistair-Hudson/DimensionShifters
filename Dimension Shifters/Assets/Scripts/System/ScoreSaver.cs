using DimensionShifters.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DimensionShifters.Core
{
    public class ScoreSaver : MonoBehaviour
    {
        public static int Score { get; private set; }  = 0;

        private void Awake()
        {
            Score = 0;
            DontDestroyOnLoad(gameObject);
            EnemyHealth.OnEnemyDeath.AddListener(UpdateScore);
        }

        private void UpdateScore(int arg0)
        {
            Score += arg0;
        }
    }
}