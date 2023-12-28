using CodeBase.Tower;
using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "TowerData", menuName = "StaticData/Towers")]
	public class TowerStaticData : ScriptableObject
	{
		public TowerType TowerTypeId;

		public int Cost;
    
		[Range(1,1000)]
		public float Damage;
    
		[Range(.5f,1)]
		public float Cleavage = .5f;

		[Range(0,10)]
		public float ShootSpeed = 3;
    
		public GameObject Prefab;
	}
}