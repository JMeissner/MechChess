using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Public Fields

    //BFS Movement
    public List<Tile> AdjacentTiles = new List<Tile>();

    public int distance = 0;

    public bool isSelectable = false;

    //Startplacement
    public bool isStartAreaPlayer1 = false;

    public bool isStartAreaPlayer2 = false;
    public Tile parentTile = null;
    public bool visited = false;

    #endregion Public Fields

    #region Public Methods

    public void CheckTile(Vector3 direction)
    {
        Vector3 halfExtends = new Vector3(0.25f, 1.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null)
            {
                // Nothing on this tile
                if (!Physics.Raycast(tile.transform.position, Vector3.up, 1.0f))
                {
                    AdjacentTiles.Add(tile);
                }
            }
        }
    }

    public void FindNeighbors()
    {
        AdjacentTiles.Clear();
        CheckTile(Vector3.forward);
        CheckTile(-Vector3.forward);
        CheckTile(Vector3.right);
        CheckTile(Vector3.left);
    }

    // Resets all Movement related variables
    public void Reset()
    {
        visited = false;
        parentTile = null;
        distance = 0;
        isSelectable = false;
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
        if (isSelectable)
        {
            this.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    #endregion Private Methods
}