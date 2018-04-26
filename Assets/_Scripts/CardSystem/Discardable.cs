using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Discardable : MonoBehaviour, IPointerUpHandler, IPointerClickHandler
{
    #region Public Methods

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CardManager.instance.ConsumeCard(this.GetComponent<CardDisplay>().card);
        Destroy(this.gameObject);
    }

    #endregion Public Methods

    #region Private Methods

    private void OnDisable()
    {
        this.GetComponent<CardDisplay>().DiscartedIcon.enabled = false;
    }

    private void OnEnable()
    {
        this.GetComponent<CardDisplay>().DiscartedIcon.enabled = true;
    }

    #endregion Private Methods
}