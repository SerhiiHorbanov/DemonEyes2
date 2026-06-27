using Tools;
using UnityEngine;
using UnityEngine.Audio;

namespace Characters.Player
{
	[RequireComponent(typeof(PlayerProjectileFactory))]
	[RequireComponent(typeof(PlayerAim))]
	public class AlphaShootingAbility : AbilityWithCooldown
	{
		private PlayerProjectileFactory _projectileFactory;
		private PlayerAim _aim;

		[SerializeField] private IAudioGenerator _ShotAudioGenerator;
		[SerializeField] private AudioSourcePool _ShotAudio;

		private void Awake()
		{
			_projectileFactory = GetComponent<PlayerProjectileFactory>();
			_aim = GetComponent<PlayerAim>();
		}
		
		protected override void ActivateAfterCooldown()
		{
			float projectileZRotation = _aim.GetRotationDegTowardsTarget();
			_projectileFactory.SpawnProjectile(projectileZRotation);

			
			_ShotAudio.PlayEmptySource();
		}
	}
}
