using UnityEngine;

namespace CodeBase.Enemy
{
	public class BaseTroll : MonoBehaviour
	{
		[SerializeField] private EnemyHealth _health;
		[SerializeField] private EnemyDeath _death;
		[SerializeField] private float _healthRegeneration;
		[SerializeField] private float _healthRegenerationInterval = 7;
		[SerializeField] private ParticleSystem _healVfx;
	
		private float _healthRegenerationTimer;
		private bool _isActive;

		private void Start()
		{
			_isActive = true;
			_death.Happened += DeathOnHappened;
		}

		private void DeathOnHappened()
		{
			_isActive = false;
		}

		private void Update()
		{
			if (_isActive)
			{
				_healthRegenerationTimer += Time.deltaTime;
				if (_healthRegenerationTimer > _healthRegenerationInterval && _health.Current + _healthRegeneration < _health.Max)
				{
					_health.TakeDamage(-_healthRegeneration);
					_healthRegenerationTimer = 0;
					_healVfx.Play();
				}
			}
			
		}
	}
}