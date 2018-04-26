using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    #region Public Methods

    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            //Debug.Log("Card was dropped on a Dropzone");

            Card card = eventData.pointerDrag.GetComponent<CardDisplay>().card;

            /* Adjust behavior according to type */
            //General Card disappears

            //Unit Cards need to hit a Unit
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Unit"))
                {
                    Unit unit = hit.collider.GetComponent<Unit>();

                    Debug.Log("Card was dropped on a Unit");
                    eventData.pointerDrag.GetComponent<Draggable>().RemovePlaceholder();
                    CardManager.instance.ConsumeCard(card);

                    /* Based on card, do stuff with the Unit. Use Card IDs for behavior */
                    switch (card.ID)
                    {
                        case "0": break;
                        case "1": unit.EnableMovement(); break;
                        default: break;
                    }

                    Destroy(eventData.pointerDrag);
                }
            }

            //AoE Cards need a Tile
        }
    }

    #endregion Public Methods
}