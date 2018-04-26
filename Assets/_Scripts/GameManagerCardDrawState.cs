using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    #region Public Fields

    public const string CardDrawState = "CardDraw";

    #endregion Public Fields

    #region Private Methods

    private void CardDrawStateEnter()
    {
        CurrentState = States.CardDraw;
        if (DebugMode)
            PrintDebug("GameManager: CardDrawState");

        cardManagerInstance.DrawCards(CardDrawAmount);
        stateMachine.ChangeState(UnitState);
    }

    private void CardDrawStateExit()
    {
    }

    #endregion Private Methods
}