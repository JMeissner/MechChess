using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string StartGameState = "StartGame";

    #endregion Public Fields

    #region Private Methods

    private void StartGameStateEnter()
    {
        CurrentState = States.StartGame;
        if (DebugMode)
            PrintDebug("GameManager: StartGameState, PlayerID: " + myPlayerNo);

        if (myPlayerNo == 0)
        {
            Debug.LogError("ERROR: PlayerID is still 0");
        }

        //TODO: Highlight the correct starting fields
    }

    private void StartGameStateExit()
    {
        //Draw 3 start cards
        cardManagerInstance.DrawCards(3);
    }

    #endregion Private Methods
}