using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
	public class KingAnimator : MonoBehaviour, IAnimationStateReader
	{
		private static readonly int HitHash = Animator.StringToHash("Hit");
		private static readonly int DieHash = Animator.StringToHash("Death");
    
		[SerializeField] public Animator _animator;
    
		public AnimatorState State { get; }
		
		public void PlayHit()
		{
			_animator.SetTrigger(HitHash);
		}		
		
		public void PlayDeath()
		{
			_animator.SetTrigger(DieHash);
		}
		
		public void EnteredState(int stateHash)
		{
      
		}

		public void ExitedState(int stateHash)
		{
      
		}

	}
}