using Characters.Player;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Beta
{
	public class BetaShootingAbility : AbilityWithCooldown
	{
		private PlayerProjectileFactory _projectileFactory;
		private PlayerAim _aim;

		[FormerlySerializedAs("_HalfSpread")] [SerializeField] private float _halfSpread;
		[FormerlySerializedAs("_ProjectileCount")] [SerializeField] private int _projectileCount;
		[FormerlySerializedAs("_ShotAudio")] [SerializeField] private AudioSourcePool _shotAudio;
		
		private void Awake()
		{
			_projectileFactory = GetComponent<PlayerProjectileFactory>();
			_aim = GetComponent<PlayerAim>();
		}

		protected override void ActivateAfterCooldown()
		{
			float lookingRotationDeg = _aim.GetRotationDegTowardsTarget();
			for (int i = 0; i < _projectileCount; i++)
			{
				float projectileZRotation = lookingRotationDeg + Random.Range(-_halfSpread, _halfSpread);
				_projectileFactory.SpawnProjectile(projectileZRotation);
			}
			_shotAudio.PlayEmptySource();
		}
	}
}
