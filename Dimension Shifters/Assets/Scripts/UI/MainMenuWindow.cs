using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DimensionShifters.UI
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton = null;
        [SerializeField]
        private Button _quitButton = null;

        private void Awake()
        {
            _startButton.onClick.AddListener(StartGame);
            _quitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void StartGame()
        {
            SceneManager.LoadScene(1);
        }
    }
}