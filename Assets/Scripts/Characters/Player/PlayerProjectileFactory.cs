using UnityEngine;

namespace Characters.Player
{
	public class PlayerProjectileFactory : MonoBehaviour
	{
		[SerializeField] private GameObject _ProjectilePrefab;
		
		public void SpawnProjectile(float directionDeg)
		{
			Quaternion rotation = Quaternion.Euler(0, 0, directionDeg);
			GameObject projectile = Instantiate(_ProjectilePrefab, transform.position, rotation);
		}
	}
}
