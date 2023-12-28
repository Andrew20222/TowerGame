namespace CodeBase.Enemy
{
	public class DividedSlime : BaseSlime
	{
		public void Construct(SlimeDividerCounter divideCounter) =>
			_divideCounter = divideCounter;

		protected override void DeathHappened()
		{
			//_gameFactory.Monsters.RemoveAt(_gameFactory.Monsters.Count - 1);
		}
	}
}