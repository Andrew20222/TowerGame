using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class GoldCounter : MonoBehaviour
	{
		public TextMeshProUGUI ScoreText;
		private IPersistentProgressService _progressService;

		private void Awake()
		{
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
			
			_progressService.Progress.WorldData.LootData.ChangedGold += ChangedGold;
			ChangedGold();
		}

		private void OnDestroy() =>
			_progressService.Progress.WorldData.LootData.ChangedGold -= ChangedGold;


		private void ChangedGold() =>
			UpdateGoldCounter();

		private void UpdateGoldCounter() =>
			ScoreText.text = _progressService.Progress.WorldData.LootData.CollectedGold.ToString();
	}
}