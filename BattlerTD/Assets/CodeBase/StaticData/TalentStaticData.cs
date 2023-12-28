using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "TalentData", menuName = "StaticData/Talent")]
	public class TalentStaticData : ScriptableObject
	{
		public int Price;
		public string Description;
		public Image Icon;
	}
}