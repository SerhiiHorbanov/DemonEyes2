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
			_input.actions["Primary Ability"].performed += PrimaryAbilityPerformed;
			_input.actions["Primary Ability"].canceled += PrimaryAbilityCanceled;
			_input.actions["Secondary Ability"].performed += SecondaryAbilityPerformed;
			_input.actions["Secondary Ability"].canceled += SecondaryAbilityCanceled;
		}

		private void SecondaryAbilityCanceled(InputAction.CallbackContext obj)
		{
			_Character.DeactivateSecondaryAbility();
		}

		private void SecondaryAbilityPerformed(InputAction.CallbackContext obj)
		{
			_Character.ActivateSecondaryAbility();
		}

		private void PrimaryAbilityCanceled(InputAction.CallbackContext obj)
		{
			_Character.DeactivatePrimaryAbility();
		}

		private void PrimaryAbilityPerformed(InputAction.CallbackContext obj)
		{
			_Character.ActivatePrimaryAbility();
		}

		private void Move(InputAction.CallbackContext context)
		{
			Vector2 value = context.ReadValue<Vector2>();
			_Character.SetMoveDirection(value);
		}
	}
}
