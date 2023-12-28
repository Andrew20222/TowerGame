using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class CriticalHitIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseCriticalHitChance();
					IncreaseCriticalDamage(124);
					break;
				case 2:
					IncreaseCriticalHitChance();
					IncreaseCriticalDamage(25);
					break;
				case 3:
					IncreaseCriticalHitChance();
					IncreaseCriticalHitChance();
					IncreaseCriticalDamage(50);
					break;
				case 4:
					IncreaseCriticalHitChance();
					IncreaseCriticalHitChance();
					IncreaseCriticalDamage(50);
					break;
				case 5:
					IncreaseCriticalHitChance();
					IncreaseCriticalHitChance();
					IncreaseCriticalHitChance();
					IncreaseCriticalHitChance();
					IncreaseCriticalDamage(75);
					break;
				default:
					break;
			}
			
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.CriticalChance);
			Debug.Log(ProgressService.Progress.HeroStats.CriticalMultiplier);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.CriticalChance * 100 % 100)}%");
		}

		private void IncreaseCriticalHitChance() =>
			ProgressService.Progress.HeroStats.CriticalChance += 0.01f;

		private void IncreaseCriticalDamage(float percent) =>
			ProgressService.Progress.HeroStats.CriticalMultiplier += percent;
	}
}