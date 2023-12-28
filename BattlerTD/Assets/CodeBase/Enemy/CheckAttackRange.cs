using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
  [RequireComponent(typeof(Attack))]
  public class CheckAttackRange : MonoBehaviour
  {
    public Attack _enemyAttack;
    public TriggerObserver TriggerObserver;

    private void Start()
    {
      TriggerObserver.TriggerEnter += TriggerEnter;
      TriggerObserver.TriggerExit += TriggerExit;
      
      _enemyAttack.DisableAttack();
    }

    private void OnDestroy()
    {
      TriggerObserver.TriggerEnter -= TriggerEnter;
      TriggerObserver.TriggerExit -= TriggerExit;
    }
    
    private void TriggerExit(Collider obj) =>
      _enemyAttack.DisableAttack();

    private void TriggerEnter(Collider obj) =>
      _enemyAttack.EnableAttack();
  }
}