using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
	public class DamageInflicter : MonoBehaviour
	{
		public UnityEvent<IDamageable> _OnInflictedDamage;
		public UnityEvent _OnCollidedWithoutInflictingDamage;
		
		public GameObject[] _Ignored;
		[SerializeField] private int _Damage;

		private void OnTriggerEnter2D(Collider2D other)
		{
			GameObject otherGO = other.gameObject;
			
			foreach (GameObject ignored in _Ignored)
			{
				if (otherGO == ignored)
					return;
			}

			IDamageable damageable = otherGO.GetComponent<IDamageable>();
			if (damageable is null)
			{
				_OnCollidedWithoutInflictingDamage.Invoke();
				return;
			}
			
			damageable.TakeDamage(_Damage);
			_OnInflictedDamage.Invoke(damageable);
		}
	}
}
