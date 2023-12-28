using System;
using System.Globalization;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
	public class UpgradeMenu : MonoBehaviour
	{
		[SerializeField] private GameObject _heroTalents;
		[SerializeField] private GameObject _kingTalents;
		[SerializeField] private Button _heroUpgradesButton;
		[SerializeField] private HeroUpgradesPage _heroUpgradesPage;
		[SerializeField] private KingUpgradesPage _kingUpgradesPage;
		[SerializeField] private Button _kingUpgradesButton;
		[SerializeField] private GameObject _mainMenu;
		[SerializeField] private RectTransform _heroUpgrades;
		[SerializeField] private RectTransform _kingUpgrades;
		[SerializeField] private RectTransform _baseBar;
		[SerializeField] private Page _statsPage;

		private Page _currentPage;
		private RectTransform _currentSelectionBar;
		private IPersistentProgressService _progressService;
		private HeroesPreviewMainMenu _heroesPreview;

		public void Construct(HeroesPreviewMainMenu heroesPreview)
		{
			_heroesPreview = heroesPreview;
			_heroesPreview.ChangeModelToHero();
			ChangeCurrentSelectionBar(_baseBar);
			SubscribeHeroButton();
			SubscribeKingButton();
		}

		private void Awake()
		{
			ChangeCurrentPage(_statsPage);
			_progressService = AllServices.Container.Single<IPersistentProgressService>();
		}

		private void OnEnable()
		{
			ResetCounters();
		}

		private void OnDisable()
		{
			ChangeUpgradesPageToHero();
			_heroesPreview.ChangeModelToHero();
			ResetCounters();
		}

		public void GoMainMenu()
		{
			gameObject.SetActive(false);
			_mainMenu.SetActive(true);
		}

		public void HideTalents()
		{
			_kingTalents.SetActive(false);
			_heroTalents.SetActive(false);
		}

		public void ChangeSelectionBarToBase() =>
			ChangeCurrentSelectionBar(_baseBar);

		private void SubscribeKingButton() =>
			_kingUpgradesButton.onClick.AddListener(ChangeUpgradesPageToKing);

		private void SubscribeHeroButton() =>
			_heroUpgradesButton.onClick.AddListener(ChangeUpgradesPageToHero);

		private void ChangeUpgradesPageToHero()
		{
			_kingTalents.SetActive(false);
			_heroTalents.SetActive(true);
			_heroUpgradesPage.ChangeToAttackContent();
			ChangeCurrentPage(_heroUpgradesPage);
			ChangeCurrentSelectionBar(_heroUpgrades);
			_heroesPreview.ChangeModelToHero();
		}

		private void ChangeUpgradesPageToKing()
		{
			_kingTalents.SetActive(true);
			_heroTalents.SetActive(false);
			_kingUpgradesPage.ChangeToDefenseContent();
			ChangeCurrentPage(_kingUpgradesPage);
			ChangeCurrentSelectionBar(_kingUpgrades);
			_heroesPreview.ChangeModelToKing();
		}

		private void ChangeCurrentPage(Page page)
		{
			
			if (_currentPage)
				_currentPage.gameObject.SetActive(false);

			_currentPage = page;
			_currentPage.gameObject.SetActive(true);
		}

		private void ChangeCurrentSelectionBar(RectTransform bar)
		{
			if (_currentSelectionBar)
				_currentSelectionBar.gameObject.SetActive(false);

			_currentSelectionBar = bar;
			_currentSelectionBar.gameObject.SetActive(true);
		}

		private void ResetCounters() =>
			ResetGoldCounter();

		private void ResetGoldCounter() =>
			_progressService.Progress.WorldData.LootData.AddGold(0);
	}
}