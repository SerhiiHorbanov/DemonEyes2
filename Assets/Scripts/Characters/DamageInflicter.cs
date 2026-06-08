using System;
using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
	public class DamageInflicter : MonoBehaviour
	{
		public UnityEvent<Health> _OnInflictedDamage;
		
		public GameObject[] _Ignored;
		[SerializeField] private int _Damage;

		/*private void OnCollisionEnter2D(Collision other)
		{
			Debug.Log("Collision");
		}*/

		private void OnTriggerEnter2D(Collider2D other)
		{
			GameObject otherGO = other.gameObject;
			
			foreach (GameObject ignored in _Ignored)
			{
				if (otherGO == ignored)
					return;
			}

			IDamageable damageable = otherGO.GetComponent<IDamageable>();
			damageable?.TakeDamage(_Damage);
		}
	}
}
