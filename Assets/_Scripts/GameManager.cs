using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Events

    public const string EndTurnEvent = "GameManager.EndTurnEvent";
    public const string PlayerNoRoll = "GameManager.PlayerNoRoll";
    public const string UnitSpawn = "GameManager.UnitSpawn";

    #endregion Events

    #region Public Fields

    public int CardDrawAmount = 2;
    public States CurrentState;
    public bool DebugMode = true;
    public bool DontDestroyOnLoadBool = true;
    public int myPlayerNo = 0;

    //Placeable units. Currently: Size = 3
    public List<GameObject> MyUnits = new List<GameObject>();

    public enum States { Lobby, StartGame, CardDraw, Unit, EndTurn, Passive, Ended };

    #endregion Public Fields

    #region Private Fields

    private static string myTeamKey = "Default";
    private CardManager cardManagerInstance;
    private MatchController matchController;
    private StateMachine stateMachine = new StateMachine();

    #endregion Private Fields

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null) { return; }
        instance = this;
        matchController = this.GetComponent<MatchController>();
        RegisterStates();
    }

    #endregion Singleton

    #region Public Methods

    public static string GetMyTeamKey()
    {
        return myTeamKey;
    }

    public NetworkPlayer GetMyNetworkPlayer()
    {
        return matchController.localPlayer;
    }

    public void SetActiveState()
    {
        stateMachine.ChangeState(CardDrawState);
    }

    public void SetName(string name)
    {
        if (DebugMode)
            PrintDebug("Changed Name to: " + name);

        myTeamKey = name;
    }

    public void SetPlayerNo(int number)
    {
        myPlayerNo = number;
    }

    #endregion Public Methods

    #region Private Methods

    private void OnDisable()
    {
    }

    private void OnEnable()
    {
    }

    private void OnRegisterUnit(object sender, object args)
    {
        UnitNetwork unitNetwork = (UnitNetwork)sender;
        MyUnits.Add(unitNetwork.gameObject);
    }

    private void PrintDebug(string message)
    {
        Debug.Log(message);
    }

    private void RegisterStates()
    {
        stateMachine.Register(CardDrawState, new State(CardDrawStateEnter, CardDrawStateExit));
        stateMachine.Register(EndTurnState, new State(EndTurnStateEnter, EndTurnStateExit));
        stateMachine.Register(UnitState, new State(UnitStateEnter, UnitStateExit));
        stateMachine.Register(PassiveState, new State(PassiveStateEnter, PassiveStateExit));
        stateMachine.Register(StartGameState, new State(StartGameStateEnter, StartGameStateExit));
        stateMachine.Register(LobbyState, new State(LobbyStateEnter, LobbyStateExit));
        Debug.Log("Registered States");
    }

    // Use this for initialization
    private void Start()
    {
        cardManagerInstance = CardManager.instance;
        if (DontDestroyOnLoadBool)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        //Setup Notifications
        this.AddObserver(OnRegisterUnit, UnitNetwork.Register);

        //Start into Lobby
        stateMachine.ChangeState(LobbyState);
        Debug.Log("Start finished");
    }

    // Update is called once per frame
    private void Update()
    {
        //Game has just started, place Units and select beginning Player
        //TODO: Highlight Startareas
        //TODO: Select which Unit to spawn
        if (CurrentState == States.StartGame)
        {
            //Check if all Units are already spawned
            if (MyUnits.Count == 3)
            {
                if (myPlayerNo == 2)
                {
                    this.stateMachine.ChangeState(PassiveState);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        //GetStartTiles
                        //Player ID = 1
                        if (myPlayerNo == 1)
                        {
                            if (hit.collider.GetComponent<Tile>().isStartAreaPlayer1)
                            {
                                //Post Notification for Unit Spawn
                                this.PostNotification(UnitSpawn, hit.collider.transform.position);
                                hit.collider.GetComponent<Tile>().isStartAreaPlayer1 = false;
                            }
                        }
                        //Player ID = 2
                        else if (myPlayerNo == 2)
                        {
                            if (hit.collider.GetComponent<Tile>().isStartAreaPlayer2)
                            {
                                //Post Notification for Unit Spawn
                                this.PostNotification(UnitSpawn, hit.collider.transform.position);
                                hit.collider.GetComponent<Tile>().isStartAreaPlayer2 = false;
                            }
                        }
                    }
                }
            }
        }

        //EndTurn - has 5 or less cards on hand
        if (CurrentState == States.EndTurn)
        {
            if (cardManagerInstance.ActiveCards.Count <= 5)
            {
                stateMachine.ChangeState(PassiveState);
            }
        }
    }

    #endregion Private Methods
}