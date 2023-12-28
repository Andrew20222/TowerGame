using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.Timers;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
	public class AgentMoveToPlayer : Follow
	{
		private const float MinimalDistance = 1.1f;
		
		public NavMeshAgent Agent;
		public Attack _enemyAttack;

		private Transform _targetTransform;
		private Transform _cachedKingTransform;
		private float _cachedSpeed;
		private bool _isSpeedDecreased;

		public Transform TargetTransform
		{
			get
			{
				return _targetTransform;
			}
			set
			{
				_targetTransform = value;
				_enemyAttack.Construct(_targetTransform);
			}
		}

		public void Construct(Transform pumpkinTransform) =>
			TargetTransform = pumpkinTransform;

		private void Start()
		{
			_cachedSpeed = Agent.speed;
			_cachedKingTransform = _targetTransform;
		}

		private void Update()
		{
			if (TargetTransform && IsHeroNotReached())
				Agent.destination = TargetTransform.position;
		}

		public void StopMove() =>
			Agent.speed = 0;

		public void StartMove() =>
			Agent.speed = _cachedSpeed;

		public void ChangeToNewTarget(Transform newTarget)
		{
			TargetTransform = newTarget;
		}

		public void ChangeToOldTarget()
		{
			TargetTransform = _cachedKingTransform;
		}

		public void RestoreSpeed()
		{
			if (_isSpeedDecreased)
			{
				_isSpeedDecreased = false;
				Agent.speed = _cachedSpeed;
			}
		}

		public void DecreaseSpeed()
		{
			if (!_isSpeedDecreased)
			{
				_isSpeedDecreased = true;
				Agent.speed *= 0.5f;
			}
		}

		private bool IsHeroNotReached() =>
			Agent.transform.position.SqrMagnitudeTo(TargetTransform.position) >= MinimalDistance;

	}
}