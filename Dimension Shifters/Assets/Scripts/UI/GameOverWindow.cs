using DimensionShifters.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;

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
        [SerializeField]
        private Button _addToLeaderboardButton = null;

        private const string _leaderboardID = "Dimension_Shifters";
        private int _score = 0;

        private void Awake()
        {
            _restartButton.onClick.AddListener(Restart);
            _quitButton.onClick.AddListener(Exit);
            _addToLeaderboardButton.onClick.AddListener(AddScoreToLeaderboard);
            _score = ScoreSaver.Score;
            _scoreText.text = $"Score: {_score}";
            Destroy(FindObjectOfType<ScoreSaver>().gameObject);
        }

        private void AddScoreToLeaderboard()
        {
            AddScoreToLeaderboardAsync();
        }

        private async void AddScoreToLeaderboardAsync()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(_leaderboardID);
            await LeaderboardsService.Instance.AddPlayerScoreAsync(_leaderboardID, _score);
            AuthenticationService.Instance.SignOut(true);
            _addToLeaderboardButton.gameObject.SetActive(false);
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