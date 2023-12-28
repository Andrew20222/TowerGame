using System;

namespace CodeBase.Data
{
  [Serializable]
  public class KillData
  {
    public int KilledMobs;
    public int CurrentMonsterWaves;
    public int MonsterWavesForFinish;
    
    public Action KilledMobsChanged;
    public Action WaveChanged;


    public void NextWave()
    {
      CurrentMonsterWaves += 1;
      WaveChanged?.Invoke();
    }

    public void ResetWaveData()
    {
      MonsterWavesForFinish = 2;
      CurrentMonsterWaves = 0;
      WaveChanged?.Invoke();
    }
    public void Add(int lootValue)
    {
      KilledMobs += lootValue;
      KilledMobsChanged?.Invoke();
    } 
    
    public void ResetKillData()
    {
      KilledMobs = 0;
    }
  }
}