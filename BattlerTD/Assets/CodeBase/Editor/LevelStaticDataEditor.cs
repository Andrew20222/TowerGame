﻿using System.Linq;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor
  {
    private const string InitialPointTag = "InitialPoint";
    private const string InitialPointMainBuildingTag = "InitialPointMainBuilding";
    private const string LevelTransferInitialPointTag = "LevelTransferInitialPoint";
    
    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      LevelStaticData levelData = (LevelStaticData) target;

      if (GUILayout.Button("Collect"))
      {
        levelData.EnemySpawners = FindObjectsOfType<SpawnMarker>()
          .Select(x => new EnemySpawnerStaticData(x.GetComponent<UniqueId>().Id, x.MeleeMonsterTypeId,x.RangeMonsterTypeId ,x.transform.position, x.transform.rotation))
          .ToList();

        levelData.LevelKey = SceneManager.GetActiveScene().name;
        
        levelData.InitialHeroPosition =  GameObject.FindWithTag(InitialPointTag).transform.position;
        
        levelData.InitialMainBuildingPosition =  GameObject.FindWithTag(InitialPointMainBuildingTag).transform.position;
      }
      
      EditorUtility.SetDirty(target);
    }
  }
}