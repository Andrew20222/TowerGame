using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(CheckClosestEnemy))]
	public class CheckDistanceRangeAttack : MonoBehaviour
	{
		public CheckClosestEnemy TargetLocator;
		public AgentMoveToPlayer MoveToPlayer;

	}
}