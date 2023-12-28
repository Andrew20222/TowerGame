using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Logic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class BaseSlime : MonoBehaviour
	{
		[SerializeField] protected EnemyDeath _enemyDeath;
		[SerializeField] protected SlimeDividerCounter _divideCounter;

		protected IGameFactory _gameFactory;

		private void Start()
		{
			_enemyDeath.Happened += DeathHappened;
			_gameFactory = AllServices.Container.Single<IGameFactory>();
		}

		private void OnDestroy() =>
			_enemyDeath.Happened -= DeathHappened;

		private void Update()
		{
			if (_divideCounter.DivideCounter > 2)
			{
				Destroy(gameObject, 1);
			}
		}

		protected virtual void DeathHappened()
		{
			if (_divideCounter.DivideCounter <= 2)
			{
				DivideSlime(transform);
				
			}
		}

		private void DivideSlime(Transform spot)
		{
			DividedSlime monster1 = _gameFactory.CreateMonster(MonsterTypeId.SlimeDivided, spot, 0.5f).GetComponent<DividedSlime>();
			DividedSlime monster2 = _gameFactory.CreateMonster(MonsterTypeId.SlimeDivided, spot, 0.5f).GetComponent<DividedSlime>();

			monster1.GetComponent<AgentMoveToPlayer>().Agent.speed *= 1.33f;
			monster2.GetComponent<AgentMoveToPlayer>().Agent.speed *= 1.33f;

			monster1.GetComponent<IHealth>().Current *= 0.7f;
			monster2.GetComponent<IHealth>().Current *= 0.7f;
		
			monster1.transform.localScale *= 0.7f;
			monster2.transform.localScale *= 0.7f;
			
			monster1.Construct(_divideCounter);
			monster2.Construct(_divideCounter);
			
			if (_divideCounter.DivideCounter > 0)
			{
				monster1.transform.localScale *= 0.7f;
				monster2.transform.localScale *= 0.7f;
			}
			
			_divideCounter.DivideCounter++;
		}
	}

}