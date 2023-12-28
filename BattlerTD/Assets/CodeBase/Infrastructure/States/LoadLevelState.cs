using System.Collections.Generic;
using CodeBase.CameraLogic;
using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.Tower;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;
		private readonly IStaticDataService _staticData;
		private readonly IUIFactory _uiFactory;
		private readonly ITimerService _timerService;
		private readonly IBuildingService _buildingService;
		private GameObject _hero;
		private bool _firstEnter = true;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory,
			IPersistentProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory, ITimerService timerService,
			IBuildingService buildingService)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_staticData = staticDataService;
			_uiFactory = uiFactory;
			_timerService = timerService;
			_buildingService = buildingService;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_gameFactory.Cleanup();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit()
		{
			_progressService.Progress.KillData.ResetKillData();
			_progressService.Progress.KillData.ResetWaveData();
			_loadingCurtain.Hide();
		}

		public void Update() { }

		private void OnLoaded()
		{
			InitUIRoot();
			InitGameWorld();
			InformProgressReaders();

			_stateMachine.Enter<GameLoopAttackState>();
		}

		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			LevelStaticData levelData = LevelStaticData();
			_hero = _gameFactory.CreateHero(levelData.InitialHeroPosition);
			CameraFollow(_hero);
			GameObject king = InitKing(levelData);
			InitGrid(levelData);
			InitSpawners(levelData);
			//InitBossSpawners();
			InitLootPieces();
			InitHud(_hero, king);
		}

		private void InitGrid(LevelStaticData levelStaticData)
		{
			_buildingService.Init(levelStaticData.Grid);
		}

		private GameObject InitKing(LevelStaticData levelData) =>
			_gameFactory.CreateKing(levelData.InitialMainBuildingPosition);

		private void InitSpawners(LevelStaticData levelData)
		{
			foreach (EnemySpawnerStaticData spawnerData in levelData.EnemySpawners)
				_gameFactory.CreateSpawner(spawnerData.Id, spawnerData.Position, spawnerData.Rotation, spawnerData.MeleeMonsterTypeId, spawnerData.RangeMonsterTypeId);
		}

		private LevelStaticData LevelStaticData()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			return levelData;
		}

		private void InitBossSpawners()
		{
			/*string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _staticData.ForLevel(sceneKey);
			_gameFactory.CreateBossSpawner(levelData.BossSpawnerPosition, MonsterTypeId.Golem, _hero.transform);*/
		}

		private void InitLootPieces()
		{
			foreach (KeyValuePair<string, LootPieceData> item in _progressService.Progress.WorldData.LootData.LootPiecesOnScene.Dictionary)
			{
				LootPiece lootPiece = _gameFactory.CreateLoot();
				lootPiece.GetComponent<UniqueId>().Id = item.Key;
				lootPiece.Initialize(item.Value.Loot);
				lootPiece.transform.position = item.Value.Position.AsUnityVector();
			}
		}

		private void InitHud(GameObject hero, GameObject king)
		{
			Hud hud = _gameFactory.CreateHud();
			hud.Construct(_buildingService);
			hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<IHealth>(), king.GetComponent<IHealth>(), _progressService);
		}

		private void CameraFollow(GameObject hero) =>
			Camera.main.GetComponent<CameraFollow>().Follow(hero);
	}
}