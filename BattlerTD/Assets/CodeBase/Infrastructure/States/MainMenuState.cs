using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Menu;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.States
{
	public class MainMenuState : IState
	{
		private const string MainMenu = "MainMenu";
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IUIFactory _uiFactory;
		private readonly IGameFactory _gameFactory;
		private readonly ISaveLoadService _saveLoadService;
		private readonly IStaticDataService _dataService;
		private readonly IPersistentProgressService _progressService;
		private readonly IGameStateMachine _stateMachine;
		private HeroesPreviewMainMenu _heroVisual;

		public MainMenuState(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IUIFactory uiFactory, IGameFactory gameFactory, ISaveLoadService saveLoadService,
			IStaticDataService dataService,
			IPersistentProgressService progressService, IGameStateMachine stateMachine)
		{
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_uiFactory = uiFactory;
			_gameFactory = gameFactory;
			_saveLoadService = saveLoadService;
			_dataService = dataService;
			_progressService = progressService;
			_stateMachine = stateMachine;
		}

		public void Exit() =>
			_saveLoadService.SaveProgress();

		public void Update() { }

		public void Enter()
		{
			_loadingCurtain.Show();

			_sceneLoader.Load(MainMenu, OnLoaded);
		}

		private void OnLoaded()
		{
			LevelStaticData levelStaticData = LevelStaticData();
			InitHeroVisual(levelStaticData);
			InitUIRoot();
			InitMainMenu();
			_loadingCurtain.Hide();
		}

		private void InitMainMenu()
		{
			MainMenu mainMenu = _uiFactory.CreateMainMenu();
			mainMenu.Construct(_stateMachine, _progressService, _gameFactory);
		}

		private void InitHeroVisual(LevelStaticData levelStaticData) =>
			_heroVisual = _gameFactory.CreateHeroVisual(levelStaticData.InitialHeroPosition);

		private void InitUIRoot() =>
			_uiFactory.CreateUIRoot();

		private LevelStaticData LevelStaticData()
		{
			string sceneKey = SceneManager.GetActiveScene().name;
			LevelStaticData levelData = _dataService.ForLevel(sceneKey);
			return levelData;
		}
	}
}