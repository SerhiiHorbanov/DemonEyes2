using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private PlayerMovement _movement;
        private IDamageable _damageTakingBehaviour;
    
        [FormerlySerializedAs("_PrimaryAbility")] [SerializeField] private PlayerAbility _primaryAbility;
        [FormerlySerializedAs("_SecondaryAbility")] [SerializeField] private PlayerAbility _secondaryAbility;
    
        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _damageTakingBehaviour = GetComponent<IDamageable>();
        }

        public void SetMoveDirection(Vector2 value)
        {
            _movement.SetMoveDirection(value);
        }

        public void ActivatePrimaryAbility()
        {
            _primaryAbility?.Activate();
        }
        
        public void DeactivatePrimaryAbility()
        {
            _primaryAbility?.Deactivate();
        }

        public void ActivateSecondaryAbility()
        {
            _secondaryAbility?.Activate();
        }

        public void DeactivateSecondaryAbility()
        {
            _secondaryAbility?.Deactivate();
        }
    }
}
