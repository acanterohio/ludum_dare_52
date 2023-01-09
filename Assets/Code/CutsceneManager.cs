using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    [System.Serializable]
    private class CutsceneEvent
    {
        public string name;
        public UnityEvent start;
        public UnityEvent end;
    }

    [SerializeField] private List<CutsceneEvent> events;
    private int current = 0;

    void Start()
    {
        if (events.Count > 0) events[0].start.Invoke();
    }

    public void NextEvent()
    {
        events[current].end.Invoke();
        current++;
        if (current >= events.Count) return;
        events[current].start.Invoke();
    }
}
