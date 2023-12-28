using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
	public abstract class UpgradesPage : Page
	{
		[SerializeField] private ScrollRect _scrollRect;
		[SerializeField] protected GameObject _statsContent;
		[SerializeField] protected Button _backButtonPageButton;
		[SerializeField] protected UpgradeMenu _upgradeMenu;

		protected void ChangeContentTo(RectTransform content)
		{
			if (content == _scrollRect.content && content.gameObject.activeInHierarchy)
				return;

			if (_statsContent.activeInHierarchy)
			{
				_scrollRect.gameObject.SetActive(true);
				_statsContent.SetActive(false);
			}

			Debug.Log(content.name);
			_scrollRect.content.gameObject.SetActive(false);
			_scrollRect.content = content;
			_scrollRect.content.gameObject.SetActive(true);
		}

		protected void BackToStats()
		{
			_scrollRect.gameObject.SetActive(false);
			_upgradeMenu.HideTalents();
			_upgradeMenu.ChangeSelectionBarToBase();
			//_scrollRect.gameObject.SetActive(false);
			_statsContent.SetActive(true);
		}
	}

}