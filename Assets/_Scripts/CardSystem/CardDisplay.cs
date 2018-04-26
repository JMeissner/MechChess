using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Public Fields

    //public Animator animator;
    public Card card;

    public Image DiscartedIcon;
    public Text GUICost;
    public Text GUIDescription;
    public Text GUIHeader;
    public Image GUIImage;

    #endregion Public Fields

    #region Public Methods

    //Used for Selected Animation
    public void OnPointerEnter(PointerEventData eventData)
    {
        //animator.SetBool("selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //animator.SetBool("selected", false);
    }

    #endregion Public Methods

    #region Private Methods

    private void Start()
    {
        GUICost.text = card.Cost.ToString();
        GUIHeader.text = card.Header;
        GUIDescription.text = card.Description;
        GUIImage.sprite = card.Image;
        //animator = this.GetComponent<Animator>();

        DiscartedIcon.enabled = false;
    }

    #endregion Private Methods
}