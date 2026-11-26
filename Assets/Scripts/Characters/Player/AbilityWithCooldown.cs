using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Player
{
	public abstract class AbilityWithCooldown : PlayerAbility
	{
		[FormerlySerializedAs("_IsAutomatic")] [SerializeField] private bool _isAutomatic;
		[FormerlySerializedAs("_CanBeBuffered")] [SerializeField] private bool _canBeBuffered;
		[FormerlySerializedAs("_Cooldown")] [SerializeField] private float _cooldown;
		
		private float _timeOfLastUse = -1;

		private bool _isActivated;
		private bool _isBuffered;
		
		private bool IsPastCooldown()
		{
			float now = Time.fixedTime;
			float timeSinceLastUse = now - _timeOfLastUse;

			return _cooldown < timeSinceLastUse;
		}

		public override void Activate()
		{
			_isActivated = true;
			if (!_isAutomatic)
			{
				if (_isActivated && IsPastCooldown())
				{
					ActivateAndUpdateLastTimeUsed();
				}
				else if (_canBeBuffered)
				{
					_isBuffered = true;
				}
			}
		}

		private void FixedUpdate()
		{
			if (_isAutomatic && _isActivated && IsPastCooldown())
			{
				ActivateAndUpdateLastTimeUsed();
				return;
			}
			if (_isBuffered && IsPastCooldown())
			{
				ActivateAndUpdateLastTimeUsed();
				_isBuffered = false;
			}
		}

		public override void Deactivate()
		{
			_isActivated = false;
			_isBuffered = false;
		}

		private void ActivateAndUpdateLastTimeUsed()
		{
			_timeOfLastUse = Time.fixedTime;
			ActivateAfterCooldown();
		}
		
		protected abstract void ActivateAfterCooldown();
	}
}
