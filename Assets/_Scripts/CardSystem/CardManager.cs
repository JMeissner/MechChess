using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    #region Public Fields

    public List<Card> ActiveCards = new List<Card>();
    public List<Card> AllCards = new List<Card>();
    public List<Card> DiscartedCards = new List<Card>();
    public Queue<Card> DrawableCards = new Queue<Card>();
    public int MaxCards = 5;

    public OnGUICardAdded OnGUICardAddedCallBack;

    public OnGUICardAdded OnGUICardRemovedCallBack;

    public OnGUICardAdded OnReshuffleCallBack;

    #endregion Public Fields

    #region Public Delegates

    public delegate void OnGUICardAdded(Card card);

    public delegate void OnGUICardRemoved(Card card);

    public delegate void OnReshuffle();

    #endregion Public Delegates

    #region Singleton

    public static CardManager instance;

    private void Awake()
    {
        if (instance != null) { return; }
        instance = this;
    }

    #endregion Singleton

    #region Public Methods

    /// <summary>
    /// Consumes the specified card
    /// </summary>
    /// <param name="card">Gameobject of the consumed card</param>
    public void ConsumeCard(Card card)
    {
        ActiveCards.Remove(card);
        DiscartedCards.Add(card);
    }

    /// <summary>
    /// Draws the specified amount of cards
    /// </summary>
    /// <param name="amount">amount of cards to draw</param>
    public void DrawCards(int amount)
    {
        if (DrawableCards.Count < amount)
        {
            Reshuffle();
        }
        for (int i = 0; i < amount; i++)
        {
            Card addedCard = DrawableCards.Dequeue();
            ActiveCards.Add(addedCard);
            if (OnGUICardAddedCallBack != null)
                OnGUICardAddedCallBack.Invoke(addedCard);
        }
    }

    public void LoadDeck(Deck deck)
    {
        foreach (Card card in deck.CardList)
        {
            AllCards.Add(card);
        }
        InitDeck();
    }

    /// <summary>
    /// Adds the given card to AllCards
    /// </summary>
    /// <param name="card">card to add. Needs to have a card component</param>
    public void RegisterCard(Card card)
    {
        if (card == null) { Debug.LogError("The registered Card has no Card Component!"); return; }
        AllCards.Add(card);
    }

    /// <summary>
    /// Removes the first instance of the Card with CardID
    /// </summary>
    /// <param name="CardID">Card ID to remove</param>
    public void RemoveCard(string CardID)
    {
        for (int i = 0; i < AllCards.Count; i++)
        {
            if (AllCards[i].ID == CardID)
            {
                AllCards.RemoveAt(i);
                break;
            }
        }
    }

    /// <summary>
    /// Reshuffles the deck
    /// </summary>
    public void Reshuffle()
    {
        //Discard all remaining cards
        foreach (Card card in DrawableCards)
        {
            DiscartedCards.Add(DrawableCards.Dequeue());
        }
        //Shuffle cards
        for (int i = 0; i < DiscartedCards.Count; i++)
        {
            int rid = Random.Range(0, DiscartedCards.Count - 1);
            DrawableCards.Enqueue(DiscartedCards[rid]);
            DiscartedCards.RemoveAt(rid);
        }
    }

    #endregion Public Methods

    #region Private Methods

    private void InitDeck()
    {
        foreach (Card card in AllCards)
        {
            DiscartedCards.Add(card);
        }
        Reshuffle();
    }

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