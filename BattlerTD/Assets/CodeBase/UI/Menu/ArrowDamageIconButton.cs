using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class ArrowDamageIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					ImproveBow(0.15f);
					break;
				case 2:
					ImproveBow(0.05f);
					break;
				case 3:
					ImproveBow(0.1f);
					break;
				case 4:
					ImproveBow(0.1f);
					break;
				case 5:
					ImproveBow(0.2f);
					break;
				default:
					break;
			}

			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.ArrowDamageMultiplier);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.ArrowDamageMultiplier * 100 % 100)}%");
		}

		
		private void ImproveBow(float percent) =>
			ProgressService.Progress.HeroStats.ArrowDamageMultiplier += percent;

	}
}