
using UnityEngine;
using Spine.Unity.Extensions;
public class SampleEventListener : MonoBehaviour
{
    [SerializeField] private SpineEventsListener _spineEvents;

    [SerializeField] private string _eventToListen;


    private void Start()
    {
        
        _spineEvents.SubscribeToEvent(Animator.StringToHash(_eventToListen), () =>
        {
            Debug.Log(_eventToListen + " event was fired");            
        });
    }
}
