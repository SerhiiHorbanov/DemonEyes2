using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Characters
{
	public class Health : MonoBehaviour, IDamageable
	{
		[FormerlySerializedAs("_OnDeath")] public UnityEvent OnDeath;

		[FormerlySerializedAs("_GodMode")] [SerializeField] private bool _godMode;
		[FormerlySerializedAs("_Hp")] [SerializeField] private int _hp;
		private bool _isDead;
		
		public void DestroyThis()
			=> Destroy(gameObject);
		
		public void TakeDamage(int damage)
		{
			if (_godMode)
				return;
			
			_hp -= damage;

			if (_hp > 0 || _isDead)
				return;
			
			OnDeath.Invoke();
			_isDead = true;
		}
	}
}
