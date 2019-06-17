using UnityEngine;



namespace  Spine.Unity.Extensions
{  
    public class SpineStateAnimationBehavior : StateMachineBehaviour
    {
        [SerializeField] [Tooltip(("The motion attached to this state"))]
        private Motion _motion;

        [SerializeField] [Tooltip(("Whether or not this is a looping animation"))]
        private bool _isLooping;

        [SerializeField] [Tooltip("Which track index the spine animation should be played in")]
        private int _track;
    
        private Animator _animator;
        private SkeletonAnimation _spineAnimation;

    
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            _animator = _animator ? _animator : animator;
            _spineAnimation = _spineAnimation ? _spineAnimation : _animator.GetComponent<SkeletonAnimation>();
            _spineAnimation.AnimationState.SetAnimation(_track, _motion.name, _isLooping);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }

}
