using UnityEngine;

namespace CodeBase.Enemy
{
	public class SlimeDividerCounter : MonoBehaviour
	{
		private int _divideCounter;
		public int DivideCounter
		{
			get
			{
				return _divideCounter;
			}
			set
			{
				_divideCounter = value;
			}
		}
	}
}