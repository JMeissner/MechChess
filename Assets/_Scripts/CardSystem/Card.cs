using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    #region Public Fields

    public int Cost;
    public string Description;
    public string Header;
    public string ID;
    public Sprite Image;

    #endregion Public Fields

    #region Public Methods

    public void ApplyEffect(Unit unit)
    {
    }

    public Card Clone()
    {
        Card cloneCard = new Card();
        cloneCard.ID = this.ID;
        cloneCard.Header = this.Header;
        cloneCard.Description = this.Description;
        cloneCard.Cost = this.Cost;
        cloneCard.Image = this.Image;

        return cloneCard;
    }

    #endregion Public Methods
}