using System;
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{

	public abstract class ProgressIconButton : MonoBehaviour, ISavedProgress
	{
		[SerializeField] protected SkillDescriptionWindow _skillDescription;

		[SerializeField] protected string Description;
		[SerializeField] protected TextMeshProUGUI LevelText;
		[SerializeField] protected TextMeshProUGUI PriceText;
		[SerializeField] private int _maxLevel;
		public int Level { get; set; }

		public Button TalentButton;
		public int Price;

		private float _elapsedTime;
		private bool _isAppeared;
		protected IPersistentProgressService ProgressService;

		public int MaxLevel
		{
			get
			{
				return _maxLevel;
			}
			set
			{
				_maxLevel = value;
			}
		}

		public void LoadProgress(PlayerProgress progress)
		{
			Level = progress.HeroStats.Level;
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroStats.Level = Level;
		}

		private void Awake()
		{
			ProgressService = AllServices.Container.Single<IPersistentProgressService>();

			TalentButton.onClick.AddListener(OpenDescriptionWindow);
			PriceText.text = Price.ToString();
		}

		public virtual void UpdateTalent()
		{
			ProgressService.Progress.WorldData.LootData.AddGold(-Price);
			LevelText.text = $"{Level}/{MaxLevel}";
		}

		private void OpenDescriptionWindow()
		{
			SetDescription(Description);
			_skillDescription.Appear(this);
		}

		protected virtual void SetDescription(string description) { }
	}
}