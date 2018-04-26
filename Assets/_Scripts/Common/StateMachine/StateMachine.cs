using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class StateMachine
{
    #region Private Fields

    private string current = string.Empty;
    private Dictionary<string, State> states = new Dictionary<string, State>();

    #endregion Private Fields

    #region Public Properties

    public State CurrentState
    {
        get
        {
            return states.ContainsKey(current) ? states[current] : null;
        }
    }

    #endregion Public Properties

    #region Public Methods

    public void ChangeState(string key)
    {
        if (string.Equals(key, current) || key == null)
            return;
        if (states.ContainsKey(current))
            states[current].Exit();
        current = key;
        if (states.ContainsKey(current))
            states[current].Enter();
    }

    public void Clear()
    {
        ChangeState(string.Empty);
        states.Clear();
    }

    public void Register(string key, State state)
    {
        if (!string.IsNullOrEmpty(key) && state != null)
            states[key] = state;
    }

    public void Unregister(string key)
    {
        if (string.Equals(key, current))
            ChangeState(string.Empty);

        if (states.ContainsKey(key))
            states.Remove(key);
    }

    #endregion Public Methods
}