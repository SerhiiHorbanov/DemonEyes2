using Characters.Player;
using Tools;
using UnityEngine;

namespace Characters.Beta
{
	public class BetaShootingAbility : AbilityWithCooldown
	{
		private PlayerProjectileFactory _projectileFactory;
		private PlayerAim _aim;

		[SerializeField] private float _HalfSpread;
		[SerializeField] private int _ProjectileCount;
		[SerializeField] private AudioSourcePool _ShotAudio;
		
		private void Awake()
		{
			_projectileFactory = GetComponent<PlayerProjectileFactory>();
			_aim = GetComponent<PlayerAim>();
		}

		protected override void ActivateAfterCooldown()
		{
			float lookingRotationDeg = _aim.GetRotationDegTowardsTarget();
			for (int i = 0; i < _ProjectileCount; i++)
			{
				float projectileZRotation = lookingRotationDeg + Random.Range(-_HalfSpread, _HalfSpread);
				_projectileFactory.SpawnProjectile(projectileZRotation);
			}
			_ShotAudio.PlayEmptySource();
		}
	}
}
