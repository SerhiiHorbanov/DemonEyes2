using UnityEngine;
using UnityEngine.Events;

namespace Characters
{
	public class Health : MonoBehaviour, IDamageable
	{
		public UnityEvent _OnDeath;

		[SerializeField] private bool _GodMode;
		[SerializeField] private int _Hp;
		private bool _isDead;
		
		public void DestroyThis()
			=> Destroy(gameObject);
		
		public void TakeDamage(int damage)
		{
			if (_GodMode)
				return;
			
			_Hp -= damage;

			if (_Hp > 0 || _isDead)
				return;
			
			_OnDeath.Invoke();
			_isDead = true;
		}
	}
}
