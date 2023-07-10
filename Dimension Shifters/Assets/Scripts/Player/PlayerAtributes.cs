using DimensionShifters.Saving;
using UnityEngine;

namespace DimensionShifters.Player
{
    [RequireComponent(typeof(SavingWrapper))]
    public class PlayerAtributes : MonoBehaviour, ISaveable
    {
        [System.Serializable]
        public struct PlayerSaveData
        {
            public float PlayerHealth;
            public float PlayerFiringRate;
        }

        public static float PlayerHealth = 100;
        public static float PlayerFiringRate = 1;

        private void Awake()
        {
            if (FindObjectsOfType<PlayerAtributes>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            GetComponent<SavingWrapper>().Load();
        }

        public object CaptureState()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData();
            playerSaveData.PlayerHealth = PlayerHealth;
            playerSaveData.PlayerFiringRate = PlayerFiringRate;
            return playerSaveData;
        }

        public void RestoreState(object state)
        {
            if (!(state is PlayerSaveData))
            {
                return;
            }
            var playerSaveData = (PlayerSaveData)state;
            PlayerHealth = playerSaveData.PlayerHealth;
            PlayerFiringRate = playerSaveData.PlayerFiringRate;
        }
    }
}