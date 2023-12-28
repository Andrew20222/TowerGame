using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class SwordDamageIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					ImproveSword(0.15f);
					break;
				case 2:
					ImproveSword(0.05f);
					break;
				case 3:
					ImproveSword(0.1f);
					break;
				case 4:
					ImproveSword(0.1f);
					break;
				case 5:
					ImproveSword(0.2f);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.SwordDamageMultiplier);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.SwordDamageMultiplier * 100 % 100)}%");
		}

		private void ImproveSword(float percent) =>
			ProgressService.Progress.HeroStats.SwordDamageMultiplier += percent;
	}
}