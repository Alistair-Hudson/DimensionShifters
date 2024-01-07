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
        private Button _startCyberAttackButton = null;
        [SerializeField]
        private Button _startAttackOfTheMouseButton = null;
        [SerializeField]
        private Button _quitButton = null;

        [Header("Scene Names")]
        [SerializeField]
        private string _sceneNameCyberAttack = "CyberAttack";
        [SerializeField]
        private string _sceneNameAttackOfTheMouse = "AttackOfTheMouse";

        private void Awake()
        {
            _startCyberAttackButton.onClick.AddListener(delegate { StartGame(_sceneNameCyberAttack); });
            _startAttackOfTheMouseButton.onClick.AddListener(delegate { StartGame(_sceneNameAttackOfTheMouse); });
            _quitButton.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void StartGame(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}