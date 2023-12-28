using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class MoveSpeedIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					IncreaseMoveSpeed(0.03f);
					break;
				case 2:
					IncreaseMoveSpeed(0.02f);
					break;
				case 3:
					IncreaseMoveSpeed(0.05f);
					break;
				case 4:
					IncreaseMoveSpeed(0.05f);
					break;
				case 5:
					IncreaseMoveSpeed(0.1f);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.MoveSpeedMultiplier);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.MoveSpeedMultiplier * 100 % 100)}%");
		}

		
		private void IncreaseMoveSpeed(float percent) =>
			ProgressService.Progress.HeroStats.MoveSpeedMultiplier += percent;
	}
}