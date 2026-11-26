using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters
{
	public class DamageInflicter : MonoBehaviour
	{
		[FormerlySerializedAs("_OnInflictedDamage")] public UnityEvent<IDamageable> OnInflictedDamage;
		[FormerlySerializedAs("_OnCollidedWithoutInflictingDamage")] public UnityEvent OnCollidedWithoutInflictingDamage;
		
		[FormerlySerializedAs("_Ignored")] public GameObject[] Ignored;
		[FormerlySerializedAs("_Damage")] [SerializeField] private int _damage;

		private void OnTriggerEnter2D(Collider2D other)
		{
			GameObject otherGo = other.gameObject;
			
			foreach (GameObject ignored in Ignored)
			{
				if (otherGo == ignored)
					return;
			}

			IDamageable damageable = otherGo.GetComponent<IDamageable>();
			if (damageable is null)
			{
				OnCollidedWithoutInflictingDamage.Invoke();
				return;
			}
			
			damageable.TakeDamage(_damage);
			OnInflictedDamage.Invoke(damageable);
		}
	}
}
