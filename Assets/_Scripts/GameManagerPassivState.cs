using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string PassiveState = "Passive";

    #endregion Public Fields

    #region Private Methods

    private void PassiveStateEnter()
    {
        CurrentState = States.Passive;
    }

    private void PassiveStateExit()
    {
    }

    #endregion Private Methods
}