using DimensionShifters.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DimensionShifters.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText = null;
        [SerializeField]
        private Button _restartButton = null;
        [SerializeField]
        private Button _quitButton = null;

        private void Awake()
        {
            _restartButton.onClick.AddListener(Restart);
            _quitButton.onClick.AddListener(Exit);
            _scoreText.text = $"Score: {ScoreSaver.Score}";
            Destroy(FindObjectOfType<ScoreSaver>().gameObject);
        }

        private void Restart()
        {
            SceneManager.LoadScene(1);
        }

        private void Exit()
        {
            SceneManager.LoadScene(0);
        }
    }
}