using UnityEngine;
using System;
using System.Collections;

public class State
{
    #region Fields

    public Action Enter;
    public Action Exit;

    #endregion Fields

    #region Factory Methods

    public static State Empty()
    {
        return new State(DoNothing, DoNothing);
    }

    public static State EnterOnly(Action enter)
    {
        return new State(enter, DoNothing);
    }

    public static State ExitOnly(Action exit)
    {
        return new State(DoNothing, exit);
    }

    #endregion Factory Methods

    #region Constructor

    public State(Action enter, Action exit)
    {
        Enter = enter;
        Exit = exit;
    }

    #endregion Constructor

    #region Private

    private static void DoNothing()
    {
    }

    #endregion Private
}