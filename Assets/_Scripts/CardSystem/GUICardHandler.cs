using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICardHandler : MonoBehaviour
{
    #region Public Fields

    public GameObject CardPrefab;

    #endregion Public Fields

    #region Private Fields

    private CardManager cardmanager;

    #endregion Private Fields

    #region Public Methods

    public void AddCard(Card card)
    {
        GameObject go = Instantiate(CardPrefab);
        go.GetComponent<CardDisplay>().card = card;
        go.transform.SetParent(this.transform);
    }

    public Discardable[] GetDiscardablesOfCards()
    {
        Discardable[] cards = transform.GetComponentsInChildren<Discardable>();
        return cards;
    }

    public Draggable[] GetDraggablesOfCards()
    {
        Draggable[] cards = transform.GetComponentsInChildren<Draggable>();
        return cards;
    }

    public void RemoveCard(Card card)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().ID == card.ID)
            {
                Destroy(transform.GetChild(i));
            }
        }
    }

    #endregion Public Methods

    #region Private Methods

    // Use this for initialization
    private void Start()
    {
        cardmanager = CardManager.instance;
        cardmanager.OnGUICardAddedCallBack += AddCard;
        cardmanager.OnGUICardRemovedCallBack += RemoveCard;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods
}