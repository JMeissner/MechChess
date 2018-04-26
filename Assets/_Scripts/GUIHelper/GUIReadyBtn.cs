using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIReadyBtn : MonoBehaviour
{
    #region Public Methods

    public void ReadyUp()
    {
        GameManager.instance.GetMyNetworkPlayer().ChangeReadyFlag();
    }

    #endregion Public Methods
}