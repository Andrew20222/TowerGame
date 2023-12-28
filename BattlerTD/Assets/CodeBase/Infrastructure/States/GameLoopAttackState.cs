using System.Collections;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Audio;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
	public class GameLoopAttackState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IWindowService _windowService;
		private readonly IPersistentProgressService _progressService;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly ITimerService _timerService;
		private readonly IGameFactory _gameFactory;
		private readonly IAudioService _audioService;
		private readonly SceneLoader _sceneLoader;
		private IHealth _heroHealth;
		private float _monstersForWave;

		public GameLoopAttackState(GameStateMachine stateMachine, IWindowService windowService, IPersistentProgressService progressService, LoadingCurtain loadingCurtain,
			ITimerService timerService, IGameFactory gameFactory, IAudioService audioService)
		{
			_stateMachine = stateMachine;
			_windowService = windowService;
			_progressService = progressService;
			_loadingCurtain = loadingCurtain;
			_timerService = timerService;
			_gameFactory = gameFactory;
			_audioService = audioService;
		}

		public void Exit()
		{
			DescribeHeroDeath();
			StopWave();
			_gameFactory.Monsters.Clear();
			_progressService.Progress.KillData.ResetKillData();
			_progressService.Progress.KillData.NextWave();
			_gameFactory.MonsterCreated -= MonsterCreated;
			_progressService.Progress.KillData.KilledMobsChanged -= KilledMobsChanged;
		}

		public void Enter()
		{
			_gameFactory.Monsters.Clear();
			_audioService.PlayFightStageMusic();
			_gameFactory.HUD.AppearAttackButton();
			_gameFactory.MonsterCreated += MonsterCreated;
			_progressService.Progress.KillData.KilledMobsChanged += KilledMobsChanged;
			_gameFactory.HeroGameObject.TryGetComponent(out _heroHealth);
			SubscribeHeroDeath();

			StartWave();

			_progressService.Progress.WorldData.LootData.LevelUp += LevelUp;
			Hid();
		}

		public void Update()
		{
			
		}

		private void DescribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart -= Restart;
			_gameFactory.KingGameObject.TryGetComponent(out KingHealth health);
			health.Restart -= Restart;
		}


		private void MonsterCreated(GameObject monster)
		{
			if (_gameFactory.Monsters.Count >= _monstersForWave)
				StopWave();
		}

		private void KilledMobsChanged()
		{
			if (_gameFactory.Monsters.Count > 0)
				_gameFactory.Monsters.RemoveAt(_gameFactory.Monsters.Count - 1);

			if (_gameFactory.Monsters.Count < 1)
				_stateMachine.Enter<GameLoopBuildingState>();
		}


		private void StartWave()
		{
			_monstersForWave += 2;
			
			float result = _monstersForWave / _gameFactory.Spawners.Count;

			foreach (SpawnPoint spawner in _gameFactory.Spawners)
			{
				spawner.StartSpawnMeleeMob(result);
			}
		}

		private void StopWave()
		{
			foreach (SpawnPoint spawner in _gameFactory.Spawners)
			{
				spawner.DelayBetweenSpawn *= 0.7f;
				spawner.StopSpawn(_monstersForWave);
			}
		}

		private void OnCutsceneEnded() { }

		private void SubscribeHeroDeath()
		{
			_gameFactory.HeroGameObject.TryGetComponent(out HeroDeath heroDeath);
			heroDeath.Restart += Restart;
			_gameFactory.KingGameObject.TryGetComponent(out KingHealth health);
			health.Restart += Restart;
		}


		private void Restart()
		{
			_loadingCurtain.Show();

			_stateMachine.Enter<RestartLevelState>();
		}

		private void Hid()
		{
			_timerService.StopTimer();
		}

		private void LevelUp()
		{
			_heroHealth.LevelUp();
			Hid();
			_timerService.LevelStage *= 1.15f;
		}
	}

}