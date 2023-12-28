using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] public Animator _animator;

    private static readonly int MoveHash = Animator.StringToHash("Speed");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int HideSword = Animator.StringToHash("HideSword");
    private static readonly int HideBow = Animator.StringToHash("HideBow");

    private readonly int _idleStateHash = Animator.StringToHash("Idle");
    private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int _attackStateHash = Animator.StringToHash("Attack");
    private readonly int _walkingStateHash = Animator.StringToHash("Run");
    private readonly int _hitStateHash = Animator.StringToHash("Hit");
    private readonly int _deathStateHash = Animator.StringToHash("Death");

    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;

    public AnimatorState State { get; private set; }
    public bool IsAttacking => State == AnimatorState.Attack;


    private void Update() =>
      _animator.SetFloat(MoveHash, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);

    public void PlayHit() =>
      _animator.SetTrigger(HitHash);

    public void PlayAttack() =>
      _animator.SetTrigger(AttackHash);

    public void PlayDeath() =>
      _animator.SetTrigger(DieHash);

    public void PlayHideSword() =>
      _animator.SetTrigger(HideSword);

    public void PlayHideBow() =>
      _animator.SetTrigger(HideBow);

    public void ResetToIdle() =>
      _animator.Play(_idleStateHash, -1);

    public void PlayBowShoot() =>
      _animator.SetTrigger(Shoot);

    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));

    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _idleStateHash)
      {
        state = AnimatorState.Idle;
      }
      else if (stateHash == _attackStateHash)
      {
        state = AnimatorState.Attack;
      }
      else if (stateHash == _walkingStateHash)
      {
        state = AnimatorState.Walking;
      }
      else if (stateHash == _deathStateHash)
      {
        state = AnimatorState.Died;
      }    
      else if (stateHash == _hitStateHash)
      {
        state = AnimatorState.Hit;
      }
      else
      {
        state = AnimatorState.Unknown;
      }

      return state;
    }

  }
}