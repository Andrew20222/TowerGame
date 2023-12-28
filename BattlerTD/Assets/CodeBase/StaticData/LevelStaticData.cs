using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public Vector3 BossSpawnerPosition;
    public List<EnemySpawnerStaticData> EnemySpawners;
    public Vector3 InitialHeroPosition;
    public Vector3 InitialMainBuildingPosition;
    public Grid Grid;
  }

}