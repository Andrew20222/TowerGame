using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Infrastructure.Services.Timers;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Menu;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UIFactory : IUIFactory
	{
		private const string UIRootPath = "UI/UIRoot";
		private readonly IAssetProvider _assets;
		private readonly IStaticDataService _staticData;

		private Transform _uiRoot;
		private readonly IPersistentProgressService _progressService;
		private readonly ITimerService _timerService;
		private readonly IGameStateMachine _stateMachine;
		private TowerPanel _towerPanel;

		public Transform UiRoot
		{
			get
			{
				return _uiRoot;
			}
		}

		public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService,
			ITimerService timerService, IGameStateMachine stateMachine)
		{
			_assets = assets;
			_staticData = staticData;
			_progressService = progressService;
			_timerService = timerService;
			_stateMachine = stateMachine;
		}

		public MainMenu CreateMainMenu()
		{
			WindowConfig config = _staticData.ForMenu(WindowId.MainMenu);
			MainMenu window = Object.Instantiate(config.MenuPrefab, _uiRoot);
			return window;
		}
		public void CreateWinPanel()
		{
			WindowConfig config = _staticData.ForWinPanel(WindowId.WinPanel);
			ScorePanel scorePanel = Object.Instantiate(config.ScorePrefab, _uiRoot); 
		//	scorePanel.ScoreText.text = $"Score: {_progressService.Progress.LootData.Points}";
		}
		
		public void CreateDeathPanel()
		{
			WindowConfig config = _staticData.ForDeathPanel(WindowId.DeathPanel);
			DeathPanel deathPanel = Object.Instantiate(config.DeathPrefab, _uiRoot); 
			//deathPanel.DeathText.text = $"You lost all your money: {_progressService.Progress.LootData.Points}";
		}

		public void CreateUIRoot() =>
			_uiRoot = _assets.Instantiate(UIRootPath).transform;

		public void CreateShop()
		{
			
		}

	}

}