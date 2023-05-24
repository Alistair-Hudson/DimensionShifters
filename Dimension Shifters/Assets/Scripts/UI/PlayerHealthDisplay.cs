using DimensionShifters.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DimensionShifters.UI
{
    public class PlayerHealthDisplay : MonoBehaviour
    {
        [SerializeField]
        private Image _healthBar = null;

        private void Awake()
        {
            PlayerHealth.OnPlayerHealthChange.AddListener(UpdateDisplay);
        }

        private void UpdateDisplay(float arg0)
        {

            _healthBar.fillAmount = arg0;
        }
    }
}