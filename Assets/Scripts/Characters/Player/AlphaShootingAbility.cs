using UnityEngine;

namespace Characters.Player
{
	public class AlphaShootingAbility : AbilityWithCooldown
	{
		[SerializeField] GameObject _ProjectilePrefab;
		[SerializeField] private float _ProjectileSpeed;
		
		protected override void ActivateAfterCooldown()
		{
			float projectileZRotation = 0f;
			Quaternion rotation = Quaternion.Euler(0, 0, projectileZRotation);
			GameObject projectile = Instantiate(_ProjectilePrefab, transform.position, rotation);
			Rigidbody2D rigidBody = projectile.GetComponent<Rigidbody2D>();
		}
	}
}
