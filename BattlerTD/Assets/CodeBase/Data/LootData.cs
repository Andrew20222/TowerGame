using System;

namespace CodeBase.Data
{
  [Serializable]
  public class LootData
  {
    public int CollectedSilver;
    public int CollectedGold = 5000000;
    public float RequiredPointForNextLevel = 5000000000000;
    public int Level;
    public LootPieceDataDictionary LootPiecesOnScene = new LootPieceDataDictionary();
    
    public Action ChangedSilver;
    public Action ChangedGold;
    public Action LevelUp;

    public void Collect(Loot loot)
    {
      CollectedSilver += loot.Value;
      ChangedSilver?.Invoke();

      if (CollectedSilver <= RequiredPointForNextLevel)
        return;

      RequiredPointForNextLevel *= 1.33f;
      LevelUp?.Invoke();
      Level++;                           
      CollectedSilver = 0;
    }

    public void AddSilver(int lootValue)
    {
      CollectedSilver += lootValue;
      ChangedSilver?.Invoke();
    }   
    
    public void AddGold(int lootValue)
    {
      CollectedGold += lootValue;
      ChangedGold?.Invoke();
    }
  }
}