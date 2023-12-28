using System;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Randomizer;

namespace CodeBase.Logic
{
  public interface IHealth
  {
    event Action HealthChanged;
    float Current { get; set; }
    float Max { get; set; }
    void TakeDamage(float damage, IHealth invoker = null);
    void LevelUp();
    void Construct(IRandomService randomService, IPersistentProgressService progressService);
  }

}