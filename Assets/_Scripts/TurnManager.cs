using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public static Dictionary<string, List<Unit>> UnitLookup = new Dictionary<string, List<Unit>>();
    public static Queue<string> TurnKey = new Queue<string>();
    private static Queue<Unit> TurnTeam = new Queue<Unit>();

	// Use this for initialization
	void Start () {
		
	}

    private static void InitializeTurn()
    {
        
    }

    private static void StartTurn()
    {
        InitializeTeamQueue(TurnKey.Peek());

    }

    private static void EndTurn()
    {
        //TODO: No Ownership etc of units
        string team = TurnKey.Dequeue();
        TurnKey.Enqueue(team);
        StartTurn();
    }

    private static void InitializeTeamQueue(string team)
    {
        TurnTeam.Clear();
        List<Unit> units = UnitLookup[team];
        foreach(Unit unit in units)
        {
            TurnTeam.Enqueue(unit);
        }
    }

    public static void AddUnit(string key, Unit unit)
    {
        if(UnitLookup.ContainsKey(key))
        {
            List<Unit> list;
            list = UnitLookup[key];
            list.Add(unit);
            UnitLookup[key] = list;
        }
        else
        {
            List<Unit> list = new List<Unit>();
            list.Add(unit);
            UnitLookup.Add(key, list);
            if(!TurnKey.Contains(key))
            {
                TurnKey.Enqueue(key);
            }
        }
    }

    public static void RemoveUnit(string key, Unit unit)
    {
        if (UnitLookup[key].Contains(unit))
        {
            UnitLookup[key].Remove(unit);
        }
        if (UnitLookup[key].Count == 0)
        {
            UnitLookup.Remove(key);
        }
    }
}
