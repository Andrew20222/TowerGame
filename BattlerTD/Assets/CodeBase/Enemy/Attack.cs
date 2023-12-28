using UnityEngine;

namespace CodeBase.Enemy
{
	public abstract class Attack : MonoBehaviour
	{
		[SerializeField] protected EnemyHealth _enemyHealth;
		[SerializeField] protected EnemyAnimator Animator;
		[SerializeField] protected AgentMoveToPlayer MoveToPlayer;
		[SerializeField] protected float AttackCooldown = 3.0f;

		protected int _layerMask;
		protected bool _attackIsActive;
		protected float _attackCooldown;
		protected bool _isAttacking;
		
		private Transform _heroTransform;

		public void Construct(Transform heroTransform) =>
			_heroTransform = heroTransform;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer("MainBuilding") | 1 << LayerMask.NameToLayer("Player");

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		protected virtual void OnUpdate() =>
			Update();

		public virtual void OnAttack(){}

		public virtual void OnAttackEnded()
		{
			_attackCooldown = AttackCooldown;
			_isAttacking = false;
		}
		public virtual void DisableAttack()
		{
			_attackIsActive = false;
		}

		public virtual void EnableAttack() =>
			_attackIsActive = true;

		private bool CanAttack() =>
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() =>
			_attackCooldown <= 0f;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}

		private void StartAttack()
		{
			Animator.PlayAttack();
			_isAttacking = true;
		}
	}
}