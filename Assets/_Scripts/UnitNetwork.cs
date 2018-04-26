using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnitNetwork : NetworkBehaviour
{
    #region Public Fields

    public const string Register = "UnitNetwork.Register";
    public bool isOwned;

    #endregion Public Fields

    #region Private Fields

    private Unit unit;

    #endregion Private Fields

    #region Private Methods

    // Use this for Network initialization
    public override void OnStartAuthority()
    {
        this.PostNotification(Register);
        this.isOwned = true;
    }

    // Use this for initialization
    private void Start()
    {
        unit = this.GetComponent<Unit>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods

    #region Public Methods

    [Command]
    public void CmdMoveUnit(Vector3 pos)
    {
        RpcMoveUnit(pos);
    }

    [ClientRpc]
    public void RpcMoveUnit(Vector3 pos)
    {
        //Only Move Unit if it isnt mine
        if (!hasAuthority)
        {
            unit.RemoteMoveTo(pos);
        }
    }

    #endregion Public Methods
}