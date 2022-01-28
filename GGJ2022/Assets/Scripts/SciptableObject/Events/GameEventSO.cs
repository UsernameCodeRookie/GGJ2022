using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent",menuName = "ScriptableObjects/Game Event")]
public class GameEventSO : ScriptableObject
{

    // The list of listeners that this event will notify if it is raised
    private readonly List<GameEventListener> eventListeners = 
        new List<GameEventListener>();

    public string eventTag;

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    public void Raise()
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
