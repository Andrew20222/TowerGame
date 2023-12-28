using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Tower
{

	public class FloorTrapMechanism : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			Debug.Log(other.name);
			other.GetComponent<AgentMoveToPlayer>().DecreaseSpeed();
		}

		private void OnTriggerStay(Collider other)
		{
			other.GetComponent<AgentMoveToPlayer>().DecreaseSpeed();
		}

		private void OnTriggerExit(Collider other)
		{
			Debug.Log(other.name);
			other.GetComponent<AgentMoveToPlayer>().RestoreSpeed();
		}
	}
}