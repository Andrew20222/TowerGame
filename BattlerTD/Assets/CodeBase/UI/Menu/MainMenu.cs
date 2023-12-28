using System;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{

	public class MainMenu : MonoBehaviour
	{
		private IGameStateMachine _gameStateMachine;
		private IPersistentProgressService _progressService;
		private IGameFactory _gameFactory;

		[SerializeField] private Button _playButton;
		[SerializeField] private Button _quitButton;
		[SerializeField] private Button _settingsButton;
		[SerializeField] private Button _upgradesButton;
		[SerializeField] private GameObject _settingsMenu;
		[SerializeField] private UpgradeMenu _upgradesMenu;


		public void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService progressService, IGameFactory gameFactory)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_gameFactory = gameFactory;
		}

		private void Start()
		{
			_playButton.onClick.AddListener(LoadLevelState);
			_quitButton.onClick.AddListener(Application.Quit);
			_settingsButton.onClick.AddListener(() =>
			{
				transform.GetChild(0).gameObject.SetActive(false);
				_settingsMenu.SetActive(true);
			});

			_upgradesButton.onClick.AddListener(() =>
			{
				
				transform.GetChild(0).gameObject.SetActive(false);
				_upgradesMenu.gameObject.SetActive(true);
				_upgradesMenu.Construct(_gameFactory.HeroesPreview);
			});
		}

		private void LoadLevelState()
		{
			_gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
		}

	}
}