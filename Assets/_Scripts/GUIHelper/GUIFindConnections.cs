using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIFindConnections : MonoBehaviour
{
    #region Public Fields

    public Text[] Players;

    #endregion Public Fields

    #region Private Methods

    // Update is called once per frame
    private void Update()
    {
        GameObject[] conn = GameObject.FindGameObjectsWithTag("NetworkPlayer");
        for (int i = 0; i < conn.Length; i++)
        {
            NetworkPlayer c = conn[i].GetComponent<NetworkPlayer>();
            if (c.ready)
            {
                Players[i].text = c.PlayerName + ":  [READY]";
            }
            else
            {
                Players[i].text = c.PlayerName + ":  [NOT READY]";
            }
            if (conn.Length == 1)
            {
                Players[2].text = "";
            }
        }
    }

    #endregion Private Methods
}