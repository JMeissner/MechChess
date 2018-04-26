using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : TileMovement
{
    #region Public Fields

    public int Health = 100;

    public int MovementRange = 3;

    public float MovementSpeed = 2.0f;

    public int Shield = 0;

    #endregion Public Fields

    #region Private Fields

    private bool canMove = false;

    [SerializeField]
    private bool isActive = false;

    #endregion Private Fields

    #region Public Methods

    public void BeginTurn()
    {
        FindSelectableTiles(MovementRange, true);
    }

    public void EnableMovement()
    {
        if (isActive)
            canMove = true;
    }

    public void MoveTo(GameObject tile)
    {
        MoveToTarget(tile);
        this.GetComponent<UnitNetwork>().CmdMoveUnit(tile.transform.position);
    }

    public void ReceiveDamage()
    {
    }

    public void RemoteMoveTo(Vector3 tilepos)
    {
        FindSelectableTiles(MovementRange, false);
        GameObject tile = GetTileAtPosition(tilepos);
        MoveToTarget(tile);
    }

    public void SetActive()
    {
        isActive = true;
    }

    public void SetPassiv()
    {
        isActive = false;
    }

    #endregion Public Methods

    #region Private Methods

    private void Die()
    {
    }

    // Use this for initialization
    private void Start()
    {
        Init(MovementSpeed);
    }

    // Update is called once per frame
    private void Update()
    {
        #region Movement

        if (canMove)
        {
            if (!moving)
                this.FindSelectableTiles(MovementRange, true);
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        if (hit.collider.GetComponent<Tile>().isSelectable)
                        {
                            this.MoveTo(hit.collider.gameObject);
                            canMove = false;
                        }
                    }
                }
            }
        }

        #endregion Movement
    }
}

#endregion Private Methods