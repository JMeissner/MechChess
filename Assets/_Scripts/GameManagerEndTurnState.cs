using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string EndTurnState = "EndTurn";

    #endregion Public Fields

    #region Private Methods

    private void EndTurnStateEnter()
    {
        CurrentState = States.EndTurn;
        if (DebugMode)
            PrintDebug("GameManager: EndTurnState");

        //Discart Cards
        GameObject cardLayout = GameObject.FindGameObjectWithTag("CardLayout");
        Draggable[] draggables = cardLayout.GetComponent<GUICardHandler>().GetDraggablesOfCards();
        Discardable[] discardables = cardLayout.GetComponent<GUICardHandler>().GetDiscardablesOfCards();

        foreach (Draggable draggable in draggables)
        {
            draggable.enabled = false;
        }

        foreach (Discardable discartable in discardables)
        {
            discartable.enabled = true;
        }
    }

    private void EndTurnStateExit()
    {
        //Discart Cards
        GameObject cardLayout = GameObject.FindGameObjectWithTag("CardLayout");
        Draggable[] draggables = cardLayout.GetComponent<GUICardHandler>().GetDraggablesOfCards();
        Discardable[] discardables = cardLayout.GetComponent<GUICardHandler>().GetDiscardablesOfCards();

        foreach (Draggable draggable in draggables)
        {
            draggable.enabled = true;
        }

        foreach (Discardable discartable in discardables)
        {
            discartable.enabled = false;
        }

        //EndTurn network wide
        GameObject[] ntplayers = GameObject.FindGameObjectsWithTag("NetworkPlayer");
        for (int i = 0; i < ntplayers.Length; i++)
        {
            if (ntplayers[i].GetComponent<NetworkPlayer>().hasAuthority)
            {
                ntplayers[i].GetComponent<NetworkPlayer>().EndTurn();
            }
        }
    }

    #endregion Private Methods
}