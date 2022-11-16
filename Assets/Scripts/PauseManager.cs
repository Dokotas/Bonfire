using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : IPauseHandler
{
    private readonly List<IPauseHandler> _handlers = new List<IPauseHandler>();
    
    public bool isPaused { get; private set; }

    public void Register(IPauseHandler handler)
    {
        _handlers.Add(handler);
    }
    
    public void UnRegister(IPauseHandler handler)
    {
        _handlers.Remove(handler);
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
        foreach (var handler in _handlers)
        {
            handler.SetPaused(isPaused);
        }
    }
}