using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    #region Notifications

    public const string MatchReady = "MatchController.Ready";

    #endregion Notifications

    #region Public Fields

    public NetworkPlayer clientPlayer;
    public NetworkPlayer hostPlayer;
    public NetworkPlayer localPlayer;
    public List<NetworkPlayer> players = new List<NetworkPlayer>();
    public NetworkPlayer remotePlayer;

    #endregion Public Fields

    #region Public Properties

    public bool IsReady { get { return (localPlayer.ready && remotePlayer.ready); } }

    #endregion Public Properties

    #region Private Methods

    private void Configure()
    {
        if (localPlayer == null || players.Count < 2)
            return;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != localPlayer)
            {
                remotePlayer = players[i];
                break;
            }
        }

        if (localPlayer.isServer)
        {
            hostPlayer = localPlayer;
            clientPlayer = remotePlayer;
        }
        else
        {
            hostPlayer = remotePlayer;
            clientPlayer = localPlayer;
        }

        if (IsReady)
        {
            this.PostNotification(MatchReady);
        }
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnPlayerStarted, NetworkPlayer.Started);
        this.RemoveObserver(OnLocalPlayerStarted, NetworkPlayer.StartedLocal);
        this.RemoveObserver(OnPlayerDestroyed, NetworkPlayer.Destroyed);
        this.RemoveObserver(OnPlayerReady, NetworkPlayer.Ready);
    }

    private void OnEnable()
    {
        this.AddObserver(OnPlayerStarted, NetworkPlayer.Started);
        this.AddObserver(OnLocalPlayerStarted, NetworkPlayer.StartedLocal);
        this.AddObserver(OnPlayerDestroyed, NetworkPlayer.Destroyed);
        this.AddObserver(OnPlayerReady, NetworkPlayer.Ready);
    }

    private void OnLocalPlayerStarted(object sender, object args)
    {
        localPlayer = (NetworkPlayer)sender;
        Configure();
    }

    private void OnPlayerDestroyed(object sender, object args)
    {
        NetworkPlayer ntp = (NetworkPlayer)sender;
        if (localPlayer == ntp)
            localPlayer = null;
        if (remotePlayer == ntp)
            remotePlayer = null;
        if (clientPlayer == ntp)
            clientPlayer = null;
        if (hostPlayer == ntp)
            hostPlayer = null;
        if (players.Contains(ntp))
            players.Remove(ntp);
    }

    private void OnPlayerReady(object sender, object args)
    {
        Configure();
    }

    private void OnPlayerStarted(object sender, object args)
    {
        players.Add((NetworkPlayer)sender);
        Configure();
    }

    #endregion Private Methods
}