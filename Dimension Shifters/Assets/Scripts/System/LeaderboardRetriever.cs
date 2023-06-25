using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionShifters.Core
{
    public class LeaderboardRetriever : MonoBehaviour
    {
        [SerializeField]
        private Text _names = null;
        [SerializeField]
        private Text _ranks = null;
        [SerializeField]
        private Text _score = null;

        private const string _leaderboardID = "Dimension_Shifters";

        private void OnEnable()
        {
            _ranks.text = "Rank:\n";
            _names.text = "Name:\n";
            _score.text = "Score:\n";
            RetrieveLeaderboardAsync();
        }

        private async void RetrieveLeaderboardAsync()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(_leaderboardID);

            var results = scoresResponse.Results;
            int resultsToDisplay = Math.Min(results.Count, 10);
            for (int i = 0; i < resultsToDisplay; i++)
            {
                _ranks.text += (results[i].Rank+1) + "\n";
                _names.text += results[i].PlayerName + "\n";
                _score.text += results[i].Score + "\n";
            }
            AuthenticationService.Instance.SignOut(true);
        }
    }
}