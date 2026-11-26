using Tools;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Characters.Player
{
	[RequireComponent(typeof(PlayerProjectileFactory))]
	[RequireComponent(typeof(PlayerAim))]
	public class AlphaShootingAbility : AbilityWithCooldown
	{
		private PlayerProjectileFactory _projectileFactory;
		private PlayerAim _aim;

		[SerializeField] private IAudioGenerator _shotAudioGenerator;
		[FormerlySerializedAs("_ShotAudio")] [SerializeField] private AudioSourcePool _shotAudio;

		private void Awake()
		{
			_projectileFactory = GetComponent<PlayerProjectileFactory>();
			_aim = GetComponent<PlayerAim>();
		}
		
		protected override void ActivateAfterCooldown()
		{
			float projectileZRotation = _aim.GetRotationDegTowardsTarget();
			_projectileFactory.SpawnProjectile(projectileZRotation);

			
			_shotAudio.PlayEmptySource();
		}
	}
}
