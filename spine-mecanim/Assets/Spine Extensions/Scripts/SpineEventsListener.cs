using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Spine.Unity.Extensions
{
    /// <summary>
    /// A simple wrapper around Spine2D's event system for custom events.
    /// </summary>
    public class SpineEventsListener : MonoBehaviour
    {
        private SkeletonAnimation _skeletonAnimation;

        private Dictionary<int, List<Action>> _eventSubscribers;
        
        public void Awake()
        {
            _eventSubscribers = new Dictionary<int, List<Action>>();
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
            var eventHash = Animator.StringToHash(e.data.name);

            if (_eventSubscribers.ContainsKey(eventHash))
            {
                _eventSubscribers[eventHash].ForEach(sub => sub());
            }
        }

        public void SubscribeToEvent(int eventHash, Action action)
        {
            if (_eventSubscribers.ContainsKey(eventHash))
            {
                _eventSubscribers[eventHash].Add(action);
            }
            else
            {
                var subscriberList = new List<Action>();
                subscriberList.Add(action);
                _eventSubscribers.Add(eventHash,subscriberList);
            }
        }

        public void UnsubscribeToEvent(int eventHash, Action action)
        {
            _eventSubscribers[eventHash].Remove(action);
        }
    }
}
