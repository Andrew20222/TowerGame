using UnityEngine;

namespace CodeBase.Tower
{
	public class ArrowHandler : MonoBehaviour
	{
		[SerializeField] private Arrow _arrow;
		private readonly int _arrowMoveSpeed = 1500;
		private readonly int _arrowDamage = 25;

		public void PushArrow(Transform enemyTransform)
		{
			Arrow arrow = Instantiate(_arrow, transform.position, Quaternion.identity);
			arrow.MoveSpeed = _arrowMoveSpeed;
			arrow.Damage = _arrowDamage;
			if (enemyTransform.position.x > transform.position.x)
			{
				arrow.transform.Rotate(0,-90,0);
			}
			else
			{
				arrow.transform.Rotate(0,90,0);
			}
		}
	}
}