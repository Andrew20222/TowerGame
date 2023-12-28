using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{

	public class SpawnPoint : MonoBehaviour, ISavedProgress
	{
		[SerializeField] private float _delayBetweenSpawn = 7;
		public MonsterTypeId MeleeMonsterTypeId;
		public MonsterTypeId RangeMonsterTypeId;

		public Action<SpawnPoint> DestroySpawner;
		public string Id { get; set; }

		private IGameFactory _factory;

		private EnemyDeath _enemyDeath;
		
		public bool _isActive;
		private float _levelStage = 1;
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		public float DelayBetweenSpawn
		{
			get
			{
				return _delayBetweenSpawn;
			}
			set
			{
				_delayBetweenSpawn = value;
			}
		}

		public void Construct(IGameFactory gameFactory)
		{
			_factory = gameFactory;
		}

		private void Start()
		{
			IsActive = true;
		}

		private void OnDestroy()
		{
			IsActive = false;
			DestroySpawner?.Invoke(this);	
		}

		public void StartSpawnMeleeMob(float times) =>
			StartCoroutine(SpawnMeleeMob(times));		
		
		public void StopSpawn(float times) =>
			StopCoroutine(SpawnMeleeMob(times));

		public void LoadProgress(PlayerProgress progress)
		{
			
		}

		public void UpdateProgress(PlayerProgress progress)
		{

		}

		private IEnumerator SpawnMeleeMob(float times)
		{
			for (int i = 0; i < times; i++)
			{
				CreateMob(MeleeMonsterTypeId);

				yield return new WaitForSeconds(DelayBetweenSpawn);

				CreateMob(RangeMonsterTypeId);
				
				yield return new WaitForSeconds(DelayBetweenSpawn);
			}
		}

		private GameObject CreateMob(MonsterTypeId monsterTypeId)
		{
			GameObject monster = _factory.CreateMonster(monsterTypeId, transform);
			return monster;
		}
	}
}