using UnityEngine;

namespace CodeBase.UI.Menu
{
	public class VampirismIconButton : ProgressIconButton
	{
		public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					ImproveVampirism(0.1f);
					base.UpdateTalent();
					break;
				case 2:
					ImproveVampirism(0.1f);
					base.UpdateTalent();
					break;
				case 3:
					ImproveVampirism(0.1f);
					base.UpdateTalent();
					break;
				default:
					break;
			}
			SetDescription(Description);
			Debug.Log(ProgressService.Progress.HeroStats.Vampiric);
		}

		protected override void SetDescription(string description)
		{
			base.SetDescription(description);
			_skillDescription.SetDescription($"{description} + {Mathf.RoundToInt(ProgressService.Progress.HeroStats.Vampiric * 100 % 100)}%");
		}

		private void ImproveVampirism(float percent) =>
			ProgressService.Progress.HeroStats.Vampiric += percent;
	}

}