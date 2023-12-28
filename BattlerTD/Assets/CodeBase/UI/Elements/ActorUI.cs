using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{

	public class ActorUI : MonoBehaviour
	{
		public HpBar HeroHpBar;
		public HpBar KingHpBar;


		private IPersistentProgressService _progressService;
		private IHealth _heroHealth;
		private IHealth _kingHealth;
		private LootData _lootData;

		public void Construct(IHealth heroHealth, IHealth kingHealth, IPersistentProgressService progressService)
		{
			_heroHealth = heroHealth;
			_kingHealth = kingHealth;
			_progressService = progressService;
			_lootData = _progressService.Progress.WorldData.LootData;
			_heroHealth.HealthChanged += UpdateHeroHpBar;
			_kingHealth.HealthChanged += UpdateKingHpBar;
		}

		private void Construct(IHealth heroHealth)
		{
			_heroHealth = heroHealth;
			_heroHealth.HealthChanged += UpdateHeroHpBar;
			_kingHealth.HealthChanged += UpdateKingHpBar;
		}

		private void Start()
		{
			IHealth health = GetComponent<IHealth>();

			if (health != null)
				Construct(health);
		}

		private void OnDestroy()
		{
			if (_heroHealth != null)
				_heroHealth.HealthChanged -= UpdateHeroHpBar;

			if (_kingHealth != null)
				_kingHealth.HealthChanged -= UpdateKingHpBar;
		}

		private void UpdateHeroHpBar()
		{
			if (HeroHpBar != null)
				HeroHpBar.SetValue(_heroHealth.Current, _heroHealth.Max);
		}

		private void UpdateKingHpBar()
		{
			if (KingHpBar != null)
				KingHpBar.SetValue(_kingHealth.Current, _kingHealth.Max);
		}
	}
}