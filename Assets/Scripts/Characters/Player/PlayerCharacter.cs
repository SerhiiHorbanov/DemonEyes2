using UnityEngine;

namespace Characters.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        private PlayerMovement _movement;
        private IDamageable _damageTakingBehaviour;
    
        [SerializeField] private PlayerAbility _PrimaryAbility;
        [SerializeField] private PlayerAbility _SecondaryAbility;
    
        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _damageTakingBehaviour = GetComponent<IDamageable>();
        }

        public void UsePrimaryAbility()
        {
            _PrimaryAbility.Activate();
        }

        public void SetMoveDirection(Vector2 value)
        {
            _movement.SetMoveDirection(value);
        }
    }
}
