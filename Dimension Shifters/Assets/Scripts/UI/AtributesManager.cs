using DimensionShifters.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionShifters.UI
{
    public class AtributesManager : MonoBehaviour
    {
        [SerializeField]
        private Text _creditsText = null;

        [SerializeField]
        private Text _healthValueText = null;
        [SerializeField]
        private Text _firingRateValueText = null;

        [SerializeField]
        private Button _healthIncreaseButton = null;
        [SerializeField]
        private Button _firingRateIncreaseButton = null;

        private int _credits = 0;
        private float _health = 100;
        private float _firingRate = 1;

        private void Awake()
        {
            _healthIncreaseButton.onClick.AddListener(IncreaseHealth);
            _firingRateIncreaseButton.onClick.AddListener(IncreaseFiringRate);
        }

        private void OnEnable()
        {
            _credits = PlayerAtributes.PlayerCredits;
            _creditsText.text = $"Credits; {_credits}";
            _health = PlayerAtributes.PlayerHealth;
            _healthValueText.text = $"Health: {_health}";
            _firingRate = PlayerAtributes.PlayerFiringRate;
            _firingRateValueText.text = $"Firing Rate per sec: {_firingRate}";
        }

        private void OnDisable()
        {
            PlayerAtributes.PlayerCredits = _credits;
            PlayerAtributes.PlayerHealth = _health;
            PlayerAtributes.PlayerFiringRate = _firingRate;
            PlayerAtributes.SaveAtributes();
        }


        private void IncreaseFiringRate()
        {
            if (_credits <= 0)
            {
                return;
            }
            _credits--;
            _creditsText.text = $"Credits; {_credits}";
            _firingRate += 0.1f;
            _firingRateValueText.text = $"Firing Rate per sec: {_firingRate}";
        }

        private void IncreaseHealth()
        {
            if (_credits <= 0)
            {
                return;
            }
            _credits--;
            _creditsText.text = $"Credits; {_credits}";
            _health += 10;
            _healthValueText.text = $"Health: {_health}";
        }
    }
}