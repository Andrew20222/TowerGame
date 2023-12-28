using System;
using CodeBase.Hero;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class UIAttackButton : MonoBehaviour
	{
		[SerializeField] private Joystick _shootJoystick;
		[SerializeField] private Sprite _sword;
		[SerializeField] private Sprite _bow;
		[SerializeField] private Image _attackButton;
		
		private HeroRangeAttack _heroRangeAttack;
		public void Construct(HeroRangeAttack heroRangeAttack) =>
			_heroRangeAttack = heroRangeAttack;

		private void Start()
		{
			_heroRangeAttack.OnStartAiming += StartAiming;
			_heroRangeAttack.OnFinishAiming += FinishAiming;
			ChangeSpriteToSword();
		}

		private void OnDestroy()
		{
			_heroRangeAttack.OnStartAiming -= StartAiming;
			_heroRangeAttack.OnFinishAiming -= FinishAiming;
		}

		private void FinishAiming()
		{
			_shootJoystick.enabled = false;
			ChangeSpriteToSword();
		}

		private void StartAiming()
		{
			_shootJoystick.enabled = true;
			ChangeSpriteToBow();
		}

		private void ChangeSpriteToSword() =>
			_attackButton.sprite = _sword;

		private void ChangeSpriteToBow() =>
			_attackButton.sprite = _bow;

	}
}