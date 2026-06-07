using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
	public class PlayerCharacterController : MonoBehaviour
	{
		private PlayerInput _input;
		
		[SerializeField] private PlayerCharacter _Character; 
		
		private void Awake()
		{
			_input = GetComponent<PlayerInput>();
			_input.actions["Move"].performed += Move;
			_input.actions["Move"].canceled += Move;
		}

		private void Move(InputAction.CallbackContext context)
		{
			Vector2 value = context.ReadValue<Vector2>();
			_Character.SetMoveDirection(value);
		}
	}
}
