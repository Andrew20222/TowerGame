using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
	public class KingUpgradesPage : UpgradesPage
	{
		[SerializeField] private Button _kingDefensePageButton;
		[SerializeField] private Button _economyPageButton;
		[SerializeField] private RectTransform _economyContent;
		[SerializeField] private RectTransform _kingDefenseContent;
		private void Awake()
		{
			_kingDefensePageButton.onClick.AddListener(ChangeToEconomyContent);
			_economyPageButton.onClick.AddListener(ChangeToDefenseContent);
			if (_backButtonPageButton)
				_backButtonPageButton.onClick.AddListener(BackToStats);
		}

		public void ChangeToDefenseContent() =>
			ChangeContentTo(_kingDefenseContent);

		private void ChangeToEconomyContent() =>
			ChangeContentTo(_economyContent);

	}
}