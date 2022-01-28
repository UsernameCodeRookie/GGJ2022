using UnityEngine;
using UnityEngine.Events;

/*
 * add GameEventListener to GameObjects
 * bind the GameEventSO want to 'response'
 * then add behavior to response
 */
public class GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventSO Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    // register itself to a list in GameEventSO 
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
