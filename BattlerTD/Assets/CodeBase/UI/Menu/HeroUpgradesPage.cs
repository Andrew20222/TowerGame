using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
	
	public class HeroUpgradesPage : UpgradesPage
	{
		[SerializeField] private Button _heroAttackPageButton;
		[SerializeField] private Button _heroDefensePageButton;
		
		[SerializeField] private RectTransform _heroAttackContent;
		[SerializeField] private RectTransform _heroDefenseContent;

		private void Awake()
		{
			_heroAttackPageButton.onClick.AddListener(ChangeToAttackContent);
			_heroDefensePageButton.onClick.AddListener(ChangeToDefenseContent);
			if (_backButtonPageButton)
				_backButtonPageButton.onClick.AddListener(BackToStats);
		}

		public void ChangeToAttackContent() =>
			ChangeContentTo(_heroAttackContent);
		
		private void ChangeToDefenseContent() =>
			ChangeContentTo(_heroDefenseContent);
	}

}