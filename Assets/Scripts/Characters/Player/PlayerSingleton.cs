using UnityEngine;

namespace Characters.Player
{
    public class PlayerSingleton : MonoBehaviour
    {
        public static PlayerSingleton Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("PlayerSingleton already exists!");
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}