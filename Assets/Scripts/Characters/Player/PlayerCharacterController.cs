using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Characters.Player
{
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerCharacterController : MonoBehaviour
	{
		private PlayerInput _input;
		
		[FormerlySerializedAs("_Camera")] [SerializeField] private Camera _camera;
		[FormerlySerializedAs("_Character")] [SerializeField] private PlayerCharacter _character;
		[FormerlySerializedAs("_Aim")] [SerializeField] private PlayerAim _aim;
		
		private void Awake()
		{
			_input = GetComponent<PlayerInput>();
			_input.actions["Move"].performed += Move;
			_input.actions["Move"].canceled += Move;
			_input.actions["Primary Ability"].performed += PrimaryAbilityPerformed;
			_input.actions["Primary Ability"].canceled += PrimaryAbilityCanceled;
			_input.actions["Secondary Ability"].performed += SecondaryAbilityPerformed;
			_input.actions["Secondary Ability"].canceled += SecondaryAbilityCanceled;
			_input.actions["Pointer Aim"].performed += PointerAimPerformed;
		}

		private void PointerAimPerformed(InputAction.CallbackContext obj)
		{
			Vector2 relativeAimPoint = obj.ReadValue<Vector2>();
			Vector2 worldPositionOfPointer = _camera.ScreenToWorldPoint(relativeAimPoint);
			Vector2 pointerRelativeToCamera = worldPositionOfPointer - (Vector2)_camera.transform.position;
			_aim.SetRelativeAimPoint(pointerRelativeToCamera);
		}

		private void SecondaryAbilityCanceled(InputAction.CallbackContext obj)
		{
			_character.DeactivateSecondaryAbility();
		}

		private void SecondaryAbilityPerformed(InputAction.CallbackContext obj)
		{
			_character.ActivateSecondaryAbility();
		}

		private void PrimaryAbilityCanceled(InputAction.CallbackContext obj)
		{
			_character.DeactivatePrimaryAbility();
		}

		private void PrimaryAbilityPerformed(InputAction.CallbackContext obj)
		{
			_character.ActivatePrimaryAbility();
		}

		private void Move(InputAction.CallbackContext context)
		{
			Vector2 value = context.ReadValue<Vector2>();
			_character.SetMoveDirection(value);
		}
	}
}
