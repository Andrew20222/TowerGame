using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public State HeroState;
    public State KingState;
    public WorldData WorldData;
    public Stats HeroStats;
    public KillData KillData;
    public ProgressMultiplierData ProgressMultiplierData;


    public PlayerProgress(string initialLevel)
    {
      WorldData = new WorldData(initialLevel);
      HeroState = new State();
      KingState = new State();
      HeroStats = new Stats();
      KillData = new KillData();
      ProgressMultiplierData = new ProgressMultiplierData();
    }
  }

}