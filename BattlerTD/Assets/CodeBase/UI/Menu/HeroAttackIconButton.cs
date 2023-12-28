using CodeBase.Infrastructure.Services.Inputs;
using CodeBase.Infrastructure.Services.StaticData;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.UI.Menu
{
	public class HeroAttackIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseDamage();
					break;
				case 2:
					IncreaseDamage();
					break;
				case 3:
					IncreaseDamage();
					IncreaseDamage();
					break;
				case 4:
					IncreaseDamage();
					IncreaseDamage();
					break;
				case 5:
					IncreaseDamage();
					IncreaseDamage();
					IncreaseDamage();
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.DamageMultiplier);
		}
		
		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.DamageMultiplier * 100 % 100)}%");
		}

		
		private void IncreaseDamage() =>
			ProgressService.Progress.HeroStats.DamageMultiplier += 0.05f;
	}


}