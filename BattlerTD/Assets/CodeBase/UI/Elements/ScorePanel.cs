using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class ScorePanel : MonoBehaviour
	{
		[SerializeField] private int _goldAfterLevel = 15;
		[SerializeField] private Button _backButton;

		public TextMeshProUGUI ScoreText;
		
		private IPersistentProgressService _progressService;
		private IRandomService _randomService;
		private IGameStateMachine _stateMachine;
		private ISaveLoadService _saveLoadService;

		private void Awake()
		{
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			_randomService = AllServices.Container.Single<IRandomService>();
			_stateMachine = AllServices.Container.Single<IGameStateMachine>();
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
			_progressService.Progress.WorldData.LootData.AddGold(_goldAfterLevel);
			ScoreText.text = _progressService.Progress.WorldData.LootData.CollectedGold.ToString();
			_backButton.onClick.AddListener(GoToMainMenu);
		}

		private void GoToMainMenu()
		{
			_saveLoadService.SaveProgress();
			_stateMachine.Enter<MainMenuState>();
		}
	}

}