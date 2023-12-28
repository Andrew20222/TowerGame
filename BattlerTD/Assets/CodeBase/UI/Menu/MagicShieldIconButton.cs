using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class MagicShieldIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			switch (Level)
			{
				case 1:
					IncreaseMagicShieldChance(0.3f);
					break;
				case 2:
					IncreaseMagicShieldChance(0.2f);
					break;
				case 3:
					IncreaseMagicShieldChance(0.3f);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.MagicShieldChance);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.MagicShieldChance * 100 % 100)}%");
		}

		private void IncreaseMagicShieldChance(float percent) =>
			ProgressService.Progress.HeroStats.MagicShieldChance += percent;
	}
}