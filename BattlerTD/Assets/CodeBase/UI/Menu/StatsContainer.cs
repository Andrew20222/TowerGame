using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class StatsContainer : MonoBehaviour
	{
		[Header("Hero")]
		[Header("Attack")]
		[SerializeField] private TextMeshProUGUI SwordDamage;
		[SerializeField] private TextMeshProUGUI SwordRadius;
		[SerializeField] private TextMeshProUGUI ArrowDamage; 
		[SerializeField] private TextMeshProUGUI ArrowSpeed;
		[SerializeField] private TextMeshProUGUI CriticalChance;
		[SerializeField] private TextMeshProUGUI DamageMultiplier;
		[SerializeField] private TextMeshProUGUI CriticalMultiplier;
		[SerializeField] private TextMeshProUGUI MirrorHitChance;
		[SerializeField] private TextMeshProUGUI MoveSpeed;
		[SerializeField] private TextMeshProUGUI MagicShieldChance;
		[SerializeField] private TextMeshProUGUI Vampiric;
		[SerializeField] private TextMeshProUGUI MaxPowerShot;	
		
		[Header("Defense")]
		[SerializeField] private TextMeshProUGUI MaxHP;
		[SerializeField] private TextMeshProUGUI Regeneration; 
		
		[Header("King")]
		
		private IPersistentProgressService _progressService;

		private void Awake() =>
			_progressService = AllServices.Container.Single<IPersistentProgressService>();

		private void OnEnable()
		{
			UpdateStatsContainer();
		}

		private void UpdateStatsContainer()
		{
			MaxHP.text = $"{MaxHP.text}: {_progressService.Progress.HeroState.MaxHP + _progressService.Progress.HeroState.MaxHPMultiplier}";
			Regeneration.text = $"{Regeneration.text}: {_progressService.Progress.HeroState.Regeneration}";
			SwordDamage.text = $"{SwordDamage.text}: {_progressService.Progress.HeroStats.SwordDamage + _progressService.Progress.HeroStats.SwordDamageMultiplier}";
			ArrowDamage.text = $"{ArrowDamage.text}: {_progressService.Progress.HeroStats.ArrowDamage + _progressService.Progress.HeroStats.ArrowDamageMultiplier}";
			Vampiric.text = $"{Vampiric.text}: {_progressService.Progress.HeroStats.Vampiric}";
			SwordRadius.text= $"{SwordRadius.text}: {_progressService.Progress.HeroStats.SwordRadius}";
			ArrowSpeed.text = $"{ArrowSpeed.text}: {_progressService.Progress.HeroStats.ArrowSpeed}";
			CriticalChance.text = $"{CriticalChance.text}: {_progressService.Progress.HeroStats.CriticalChance}";
			DamageMultiplier.text = $"{DamageMultiplier.text}: {_progressService.Progress.HeroStats.DamageMultiplier}";
			CriticalMultiplier.text = $"{CriticalMultiplier.text}: {_progressService.Progress.HeroStats.CriticalMultiplier}";
			MaxPowerShot.text = $"{MaxPowerShot.text}: {_progressService.Progress.HeroStats.MaxPowerShot}";
			MirrorHitChance.text = $"{MirrorHitChance.text}: {_progressService.Progress.HeroStats.MirrorHitChance}";
			MoveSpeed.text = $"{MoveSpeed.text}: {_progressService.Progress.HeroStats.MoveSpeed + _progressService.Progress.HeroStats.MoveSpeedMultiplier}";
			MagicShieldChance.text = $"{MagicShieldChance.text}: {_progressService.Progress.HeroStats.MagicShieldChance}";
		}
	}
}