namespace CodeBase.UI.Menu
{
	public class MirrorHitIconButton : ProgressIconButton
	{
		/*public override void UpdateTalent()
		{
			base.UpdateTalent();
			switch (Level)
			{
				case 1:
					//IncreaseRegeneration(1.001f);
					break;
				case 2:
					//IncreaseRegeneration(0.001f);
					break;
				case 3:
					//IncreaseRegeneration(0.002f);
					break;
				case 4:
					//IncreaseRegeneration(0.002f);
					break;
				case 5:
					//IncreaseRegeneration(0.004f);
					break;
				default:
					break;
			}
			//Debug.Log(ProgressService.Progress.HeroStats.MirrorHitChance);

		}*/

		private void IncreaseMirrorHit() =>
			ProgressService.Progress.HeroStats.MirrorHitChance += 2;
	}
}