using UnityEngine;

namespace Characters.Squids
{
	[RequireComponent(typeof(Squid))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class SquidController : MonoBehaviour
	{
		[SerializeField] private Transform _Target;

		[SerializeField] private ArenaArea _Arena;
		private Squid _squid;
		private Rigidbody2D _rigidBody;

		private SquidControllerState _currentState;
		
		private abstract class SquidControllerState
		{
			public abstract void UpdateState(SquidController controller, Squid squid, Transform target);
		}

		private class DecidingState : SquidControllerState
		{
			public override void UpdateState(SquidController controller, Squid squid, Transform target)
			{
				Vector2 targetOfDirection = controller._Arena.GetRandomEdgePosition();
				Vector2 relativePosition = targetOfDirection - (Vector2)squid.transform.position;
				
				float direction = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;

				RotateTowardsDirectionState rotateState = new(new BoostingState(), direction);
				controller.SetState(rotateState);
			}
		}

		private  class BoostingState : SquidControllerState
		{
			private bool _hasBoosted;

			public override void UpdateState(SquidController controller, Squid squid, Transform target)
			{
				if (!_hasBoosted)
				{
					squid.Boost();
					_hasBoosted = true;
					return;
				}

				Vector2 velocity = controller._rigidBody.linearVelocity;
				if (velocity.magnitude < 0.5f)
					controller.SetState<DecidingState>();
			}
		}

		private class RotateTowardsDirectionState : SquidControllerState
		{
			private readonly SquidControllerState _nextState;
			private readonly float _specifiedDirectionDeg;
			
			public RotateTowardsDirectionState(SquidControllerState nextState, float directionDeg)
			{
				_nextState = nextState;
				_specifiedDirectionDeg = directionDeg;
			}

			public override void UpdateState(SquidController controller, Squid squid, Transform target)
			{
				squid.TargetRotationDeg = _specifiedDirectionDeg;

				if (squid.IsRotatedTowardsTarget)
					controller.SetState(_nextState);
			}
		}
		
		private void Awake()
		{
			_squid = GetComponent<Squid>();
			_rigidBody = GetComponent<Rigidbody2D>();
			
			_currentState = new DecidingState();
		}

		private void FixedUpdate()
		{
			_currentState.UpdateState(this, _squid, _Target);
		}

		private void SetDirectionToObjectAsTargetRotation()
		{
			Vector2 relativePosition = (Vector2)_Target.position - (Vector2)transform.position;	
			float targetRotation = Mathf.Atan2(relativePosition.y, relativePosition.x) * Mathf.Rad2Deg;
			
			_squid.TargetRotationDeg = targetRotation;
		}

		private void SetState<T>() where T : SquidControllerState, new()
		{
			_currentState = new T();
		}
		
		private void SetState(SquidControllerState state)
			=> _currentState = state;
	}
}
