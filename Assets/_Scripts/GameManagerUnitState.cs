using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string UnitState = "Unit";

    #endregion Public Fields

    #region Private Methods

    private void UnitStateEnter()
    {
        CurrentState = States.Unit;
        if (DebugMode)
            PrintDebug("GameManager: UnitState");

        foreach (GameObject unit in MyUnits)
        {
            unit.GetComponent<Unit>().SetActive();
        }
    }

    private void UnitStateExit()
    {
        foreach (GameObject unit in MyUnits)
        {
            unit.GetComponent<Unit>().SetPassiv();
        }
    }

    #endregion Private Methods
}