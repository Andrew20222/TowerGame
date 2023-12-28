using System.Collections;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class Aggro : MonoBehaviour
	{
		public TriggerObserver TriggerObserver;
		public AgentMoveToPlayer Follow;
		public RotateToHero Rotate;

		public float Cooldown;
		private bool _hasAggroTarget;

		private WaitForSeconds _switchFollowOffAfterCooldown;

		private Coroutine _aggroCoroutine;
		private Transform _heroTransform;
		public bool HasAggroTarget
		{
			get
			{
				return _hasAggroTarget;
			}
		}

		public void Construct(Transform heroTransform)
		{
			_heroTransform = heroTransform;
		}

		private void Start()
		{
			_switchFollowOffAfterCooldown = new WaitForSeconds(Cooldown);

			TriggerObserver.TriggerEnter += TriggerEnter;
			TriggerObserver.TriggerStay += TriggerStay;
			TriggerObserver.TriggerExit += TriggerExit;
		}

		private void OnDestroy()
		{
			TriggerObserver.TriggerEnter -= TriggerEnter;
			TriggerObserver.TriggerStay -= TriggerStay;
			TriggerObserver.TriggerExit -= TriggerExit;
		}

		private void TriggerStay(Collider obj)
		{
			if (HasAggroTarget && _aggroCoroutine != null)
			{
				StopAggroCoroutine();
			}
		}

		private void TriggerEnter(Collider obj)
		{
			if (HasAggroTarget)
				return;

			StopAggroCoroutine();

			SwitchFollowOn();
		}

		private void TriggerExit(Collider obj)
		{
			if (!HasAggroTarget) return;

			_aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
		}

		private void StopAggroCoroutine()
		{
			if (_aggroCoroutine == null) return;
			
			StopCoroutine(_aggroCoroutine);
			_aggroCoroutine = null;
		}

		private IEnumerator SwitchFollowOffAfterCooldown()
		{
			yield return _switchFollowOffAfterCooldown;

			SwitchFollowOff();
		}

		private void SwitchFollowOn()
		{
			_hasAggroTarget = true;
			Follow.ChangeToNewTarget(_heroTransform);
			Rotate.ChangeToNewTarget(_heroTransform);
		}

		private void SwitchFollowOff()
		{
			Follow.ChangeToOldTarget();
			Rotate.ChangeToOldTarget();
			_hasAggroTarget = false;
		}

	}
}