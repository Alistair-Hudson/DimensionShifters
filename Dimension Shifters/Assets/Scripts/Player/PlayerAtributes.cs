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
            public int PlayerCredits;
            public float PlayerHealth;
            public float PlayerFiringRate;
        }

        public static int PlayerCredits = 0;
        public static float PlayerHealth = 100;
        public static float PlayerFiringRate = 1;

        private static SavingWrapper _savingWrapper = null;

        private void Awake()
        {
            if (FindObjectsOfType<PlayerAtributes>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            _savingWrapper = GetComponent<SavingWrapper>();
            _savingWrapper.Load();
        }

        public static void SaveAtributes()
        {
            _savingWrapper.Save();
        }

        public object CaptureState()
        {
            PlayerSaveData playerSaveData = new PlayerSaveData();
            playerSaveData.PlayerCredits = PlayerCredits;
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
            PlayerCredits = playerSaveData.PlayerCredits;
            PlayerHealth = playerSaveData.PlayerHealth;
            PlayerFiringRate = playerSaveData.PlayerFiringRate;
        }

        private void OnApplicationQuit()
        {
            _savingWrapper.Save();
        }
    }
}