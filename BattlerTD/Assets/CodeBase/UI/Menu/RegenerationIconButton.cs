using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class RegenerationIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseRegeneration(1.001f);
					break;
				case 2:
					IncreaseRegeneration(0.001f);
					break;
				case 3:
					IncreaseRegeneration(0.002f);
					break;
				case 4:
					IncreaseRegeneration(0.002f);
					break;
				case 5:
					IncreaseRegeneration(0.004f);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroState.Regeneration);
		}
		
		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroState.Regeneration * 100 % 100)}%");
		}

		private void IncreaseRegeneration(float percent) =>
			ProgressService.Progress.HeroState.Regeneration += percent;
	}
}