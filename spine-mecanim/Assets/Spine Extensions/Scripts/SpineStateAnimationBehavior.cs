using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;



namespace  Spine.Unity.Extensions
{  
    public class SpineStateAnimationBehavior : StateMachineBehaviour
    {
        [SerializeField] [Tooltip(("The motion attached to this state"))]
        private Motion _motion;

        [SerializeField] [Tooltip(("Whether or not this is a looping animation"))]
        private bool _isLooping;
        
        [SerializeField] [Tooltip("Overrides current animation")]
        private bool _overrideAnimation = false;
        
        [SerializeField] [Tooltip("Which track index the spine animation should be played in")]
        private int _track;

        [SerializeField] [Tooltip(("Parameter name of flipX in the animator"))]
        private string _flipX = "flipX";

        private int _flipXHash;
        
        [SerializeField] [Tooltip("Parameter name of flipY in the animator")]
        private string _flipY = "flipY";

        private int _flipYHash;
        private SkeletonAnimation _skeletonAnimation;

        public virtual void Awake()
        {
            _flipXHash = Animator.StringToHash(_flipX);
            _flipYHash = Animator.StringToHash(_flipY);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            //init
            _skeletonAnimation = _skeletonAnimation ? _skeletonAnimation : animator.GetComponent<SkeletonAnimation>();

            var currTrack = _skeletonAnimation.AnimationState.GetCurrent(_track);
            
            //if you don't care what the current animation of this entry is
            if (_overrideAnimation)
            {
                _skeletonAnimation.AnimationState.SetAnimation(_track, _motion.name, _isLooping);
            }
            else
            {
                
                // checks if the current track is null or if the current animation
                // is the same as the animation you are setting
                // will not reset the animation if it's the same one.
                if (currTrack == null || currTrack.Animation.name != _motion.name)            
                {
                    _skeletonAnimation.AnimationState.SetAnimation(_track, _motion.name, _isLooping);
                }
            }
        }
        

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var flipX = animator.GetBool(_flipXHash);
            var flipY = animator.GetBool(_flipYHash);
            // changes the scale of the animation depending on the animator parameters
            _skeletonAnimation.Skeleton.scaleX = flipX ? -1 : 1;
            _skeletonAnimation.Skeleton.scaleY = flipY ? -1 : 1;
        }

    }

}
