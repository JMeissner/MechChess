using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    #region Public Fields

    public List<Card> CardList = new List<Card>();
    public string DeckName = "Default Deck";
    public int MaxDeckSize = 20;

    #endregion Public Fields

    #region Public Methods

    public bool AddCardToDeck(Card card)
    {
        if (CardList.Count < MaxDeckSize)
        {
            CardList.Add(card);
            return true;
        }
        return false;
    }

    public void DeserializeDeck()
    {
    }

    public bool RemoveCardFromDeck(Card card)
    {
        if (CardList.Contains(card))
        {
            CardList.Remove(card);
            return true;
        }
        return false;
    }

    //TODO: Serialization for saving etc
    public void SerializeDeck()
    {
    }

    #endregion Public Methods
}