using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMSetName : MonoBehaviour
{
    #region Public Fields

    public Text Inputfieldtext;

    #endregion Public Fields

    #region Public Methods

    public void SetName()
    {
        GameManager.instance.SetName(Inputfieldtext.text);
    }

    #endregion Public Methods
}