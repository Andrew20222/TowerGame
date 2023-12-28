using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class PowerShotIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					ImprovePowerShot(2);
					break;
				case 2:
					ImprovePowerShot(2);
					break;
				case 3:
					ImprovePowerShot(2);
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.MaxPowerShot);
		}
		
		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {ProgressService.Progress.HeroStats.MaxPowerShot}");
		}


		private void ImprovePowerShot(int percent) =>
			ProgressService.Progress.HeroStats.MaxPowerShot += percent;
	}
}