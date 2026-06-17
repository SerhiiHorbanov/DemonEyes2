using UnityEngine;

namespace Characters.Player
{
	public abstract class AbilityWithCooldown : PlayerAbility
	{
		[SerializeField] private bool _IsAutomatic;
		[SerializeField] private bool _CanBeBuffered;
		[SerializeField] private float _Cooldown;
		
		private float _TimeOfLastUse = -1;

		private bool _isActivated;
		private bool _isBuffered;
		
		private bool IsPastCooldown()
		{
			float now = Time.fixedTime;
			float timeSinceLastUse = now - _TimeOfLastUse;

			return _Cooldown < timeSinceLastUse;
		}

		public override void Activate()
		{
			_isActivated = true;
			if (!_IsAutomatic)
			{
				if (_isActivated && IsPastCooldown())
				{
					ActivateAndUpdateLastTimeUsed();
				}
				else if (_CanBeBuffered)
				{
					_isBuffered = true;
				}
			}
		}

		private void FixedUpdate()
		{
			if (_IsAutomatic && _isActivated && IsPastCooldown())
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
			_TimeOfLastUse = Time.fixedTime;
			ActivateAfterCooldown();
		}
		
		protected abstract void ActivateAfterCooldown();
	}
}
