using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilder : MonoBehaviour
{
    #region Public Fields

    public List<Card> cards = new List<Card>();
    public Deck deck;

    #endregion Public Fields

    #region Public Methods

    public void AddCardWithID(string ID)
    {
    }

    public void AddDefaultDeck()
    {
        Deck deck = new Deck();
        int times = 20 / cards.Count;
        foreach (Card card in cards)
        {
            for (int i = 0; i < times; i++)
            {
                deck.AddCardToDeck(card);
            }
        }
        CardManager.instance.LoadDeck(deck);
    }

    public Deck ConstructDefaultDeck()
    {
        Deck deck = new Deck();
        int times = 20 / cards.Count;
        foreach (Card card in cards)
        {
            for (int i = 0; i <= times; i++)
            {
                deck.AddCardToDeck(card);
            }
        }
        return deck;
    }

    public void RemoveCardWithID(string ID)
    {
    }

    #endregion Public Methods

    #region Private Methods

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods
}