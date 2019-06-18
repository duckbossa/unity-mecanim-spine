using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Spine.Unity.Extensions
{
    public class SpineEventsListener : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;

        public delegate void OnCustomEvent(string name);

        public event OnCustomEvent OnSpineCustomEvent;
        
        public void Awake()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        public void Start()
        {
            _skeletonAnimation.AnimationState.Event += AnimationStateOnEvent;
        }

        public void OnDestroy()
        {
            _skeletonAnimation.AnimationState.Event -= AnimationStateOnEvent;
        }

        private void AnimationStateOnEvent(TrackEntry trackEntry, Event e)
        {
            OnSpineCustomEvent?.Invoke(trackEntry.animation.name);
        }
    }
}
