using DimensionShifters.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionShifters.UI
{
    public class PlayerScoreDisplay : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreDisplay = null;

        private static int _score = 0;
        public static int Score { get => _score; }

        private void Awake()
        {
            EnemyHealth.OnEnemyDeath.AddListener(UpdateScore);
            _scoreDisplay.text = _score.ToString();
        }

        private void UpdateScore(int arg0)
        {
            _score += arg0;
            _scoreDisplay.text = _score.ToString();
        }
    }
}