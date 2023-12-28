using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class HeroHpIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseHp(0.1f);
					break;
				case 2:
					IncreaseHp(0.1f);
					break;
				case 3:
					IncreaseHp(0.2f);
					break;
				case 4:
					IncreaseHp(0.2f);
					break;
				case 5:
					IncreaseHp(0.4f);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroState.MaxHPMultiplier);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.CriticalChance * 100 % 100)}%");
		}

		
		private void IncreaseHp(float percent) =>
			ProgressService.Progress.HeroState.MaxHPMultiplier *= percent;
	}
}