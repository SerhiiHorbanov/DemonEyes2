using UnityEngine;

namespace Characters.Player
{
	[RequireComponent(typeof(PlayerAim))]
	public class AlphaShootingAbility : AbilityWithCooldown
	{
		[SerializeField] private GameObject _ProjectilePrefab;
		
		private PlayerAim _aim;

		private void Awake()
		{
			_aim = GetComponent<PlayerAim>();
		}
		
		protected override void ActivateAfterCooldown()
		{
			float projectileZRotation = _aim.GetRotationDegTowardsTarget();
			Quaternion rotation = Quaternion.Euler(0, 0, projectileZRotation);
			GameObject projectile = Instantiate(_ProjectilePrefab, transform.position, rotation);
		}
	}
}
