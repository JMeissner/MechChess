using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string LobbyState = "Lobby";

    #endregion Public Fields

    #region Private Methods

    private void LobbyStateEnter()
    {
        CurrentState = States.Lobby;
        if (DebugMode)
            PrintDebug("GameManager: LobbyState");

        this.AddObserver(OnMatchReady, MatchController.MatchReady);
        this.AddObserver(OnPlayerNumberRoll, NetworkPlayer.PlayerNumber);
    }

    private void LobbyStateExit()
    {
        this.PostNotification(PlayerNoRoll);
        this.RemoveObserver(OnMatchReady, MatchController.MatchReady);
        this.RemoveObserver(OnPlayerNumberRoll, NetworkPlayer.PlayerNumber);

        //TODO: Add Selected units and deck to Cardmanager and GameManager
    }

    private void OnMatchReady(object sender, object args)
    {
        //Roll Number
        if (matchController.clientPlayer.isLocalPlayer)
        {
            matchController.clientPlayer.CmdPlayerNoRoll();
        }
    }

    private void OnPlayerNumberRoll(object sender, object args)
    {
        bool starting = (bool)args;
        if (starting)
        {
            if (matchController.clientPlayer.isLocalPlayer)
            {
                this.SetPlayerNo(1);
            }
            else
            {
                this.SetPlayerNo(2);
            }
        }
        else
        {
            if (matchController.clientPlayer.isLocalPlayer)
            {
                this.SetPlayerNo(2);
            }
            else
            {
                this.SetPlayerNo(1);
            }
        }
        //Change State to Gamestart
        stateMachine.ChangeState(StartGameState);
    }

    #endregion Private Methods
}