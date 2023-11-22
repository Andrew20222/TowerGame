using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
	public class EnemyDeath : MonoBehaviour
	{
		[SerializeField] private Collider _collider;
		[SerializeField] private Collider _aggroCollider;
		[SerializeField] private EnemyHealth _health;
		[SerializeField] private Aggro _aggro;
		[SerializeField] private AgentMoveToPlayer _move;
		[SerializeField] private RotateToHero _rotateToHero;
		[SerializeField] private CheckAttackRange _range;
		[SerializeField] private Attack _attack;
		[SerializeField] private EnemyAnimator _animator;
		[SerializeField] private GameObject _deathFx;
		
		private IPersistentProgressService _progressService;
		
		public event Action Happened;

		
		private void Start()
		{
			_health.HealthChanged += OnHealthChanged;
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
		}

		private void OnDestroy()
		{
			_health.HealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged()
		{
			if (_health.Current <= 0)
				Die();
		}

		private void Die()
		{
			_range.enabled = false;
			_aggroCollider.enabled = false;
			_collider.enabled = false;
			_health.HealthChanged -= OnHealthChanged;
			_aggro.enabled = false;
			_move.Agent.speed = 0;
			_attack.enabled = false;
			_rotateToHero.enabled = false;

			_animator.PlayDeath();
			RegisterKilledMobs();
			
			// SpawnDeathFx();

			Destroy(gameObject, 3);

			Happened?.Invoke();
		}

		private void SpawnDeathFx()
		{
			// Instantiate(DeathFx, transform.position, Quaternion.identity);
		}
		
		private void RegisterKilledMobs()
		{
			_progressService.Progress.KillData.Add(1);           
		}
	}
}