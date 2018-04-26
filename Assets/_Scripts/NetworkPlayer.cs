using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour
{
    #region Events

    public const string Destroyed = "NetworkPlayer.Destroyed";
    public const string PlayerNumber = "NetworkPlayer.PlayerNumber";
    public const string Ready = "NetworkPlayer.Ready";
    public const string Started = "NetworkPlayer.Start";
    public const string StartedLocal = "NetworkPlayer.StartedLocal";

    #endregion Events

    #region Public Fields

    [SyncVar]
    public bool isActive;

    public bool isOwned;

    [SyncVar]
    public string PlayerName;

    [SyncVar]
    public bool ready;

    public GameObject unitToSpawn;

    #endregion Public Fields

    #region Private Methods

    private void OnDestroy()
    {
        this.PostNotification(Destroyed);
    }

    //TODO: Select which unit to spawn
    private void OnSpawnUnit(object sender, object args)
    {
        Debug.Log("OnSpawnUnit");
        Vector3 pos = (Vector3)args;
        if (isLocalPlayer)
        {
            Debug.Log("OnSpawnUnit: hasAuthority = true");
            CmdSpawnUnit(unitToSpawn, pos);
        }
    }

    // Use this for initialization
    private void Start()
    {
        if (isLocalPlayer)
        {
            this.PlayerName = GameManager.GetMyTeamKey();
            CmdSetUsername(PlayerName);
            //TestOnly
            //CmdSpawnUnit(unitToSpawn, new Vector3());
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods

    #region Public Methods

    public void BeginTurn()
    {
        CmdSetPlayerActive();
    }

    public void ChangeReadyFlag()
    {
        if (isLocalPlayer)
        {
            CmdPlayerReady();
        }
    }

    public void EndTurn()
    {
        CmdSetPlayerPassive();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        this.PostNotification(Started);
        this.AddObserver(OnSpawnUnit, GameManager.UnitSpawn);
        //TODO: Add Event for EndTurn
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        this.PostNotification(StartedLocal);
    }

    #endregion Public Methods

    #region Commands and RPC

    [Command]
    public void CmdPlayerNoRoll()
    {
        RpcPlayerNoRoll(Random.value < 0.5);
    }

    [Command]
    private void CmdPlayerReady()
    {
        RpcSetReady();
    }

    [Command]
    private void CmdSetPlayerActive()
    {
        isActive = true;
        RpcSetPlayerActive();
    }

    [Command]
    private void CmdSetPlayerPassive()
    {
        //Error handling
        if (!this.isActive)
        {
            Debug.LogError("Error while switching players - Active Player is non-active");
        }

        //Find non-active Player
        GameObject[] ntPlayer = GameObject.FindGameObjectsWithTag("NetworkPlayer");
        for (int i = 0; i < ntPlayer.Length; i++)
        {
            if (!ntPlayer[i].GetComponent<NetworkPlayer>().isActive)
            {
                ntPlayer[i].GetComponent<NetworkPlayer>().BeginTurn();
            }
        }
        //Set self passive
        isActive = false;
        RpcSetPlayerPassive();
    }

    [Command]
    private void CmdSetUsername(string name)
    {
        PlayerName = name;
    }

    /// <summary>
    /// Spawns the array of units at the defined positions
    /// </summary>
    /// <param name="units"></param>
    /// <param name="positions"></param>
    [Command]
    private void CmdSpawnUnit(GameObject unit, Vector3 position)
    {
        if (!isServer)
        {
            return;
        }

        RaycastHit hit;
        GameObject tile = null;
        GameObject spawned = Instantiate(unit);

        if (Physics.Raycast((position + new Vector3(0, 10, 0)), Vector3.down, out hit, 15.0f))
        {
            tile = hit.collider.gameObject;
            float unithalfHeight = spawned.transform.GetComponent<Collider>().bounds.extents.y;
            Vector3 tilepos = tile.transform.position;
            tilepos.y += unithalfHeight + tile.GetComponent<Collider>().bounds.extents.y;
            spawned.transform.position = tilepos;
        }
        else
        {
            spawned.transform.position = position;
        }

        NetworkServer.SpawnWithClientAuthority(spawned, this.gameObject);
    }

    [ClientRpc]
    private void RpcPlayerNoRoll(bool starting)
    {
        Debug.Log("PlayerNo Roll: " + starting);
        this.PostNotification(PlayerNumber, starting);
    }

    [ClientRpc]
    private void RpcSetPlayerActive()
    {
        //Call local GameManager if this item is owned
        if (hasAuthority)
        {
            GameManager.instance.SetActiveState();
        }
    }

    [ClientRpc]
    private void RpcSetPlayerPassive()
    {
        //TODO: Display Enemy Turn?
    }

    [ClientRpc]
    private void RpcSetReady()
    {
        this.ready = !this.ready;
        this.PostNotification(Ready);
    }

    #endregion Commands and RPC
}