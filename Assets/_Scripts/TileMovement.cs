using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    #region Public Fields

    public bool moving;
    public List<Tile> selectableTiles = new List<Tile>();

    #endregion Public Fields

    #region Private Fields

    private GameObject[] allTiles;

    private float halfHeight = 0;
    private bool hasInit = false;
    private Stack<Tile> path = new Stack<Tile>();
    private float speed = 0;
    private Vector3 velocity;

    #endregion Private Fields

    #region Protected Methods

    /// <summary>
    /// Finds all selectable tiles within the move range.
    /// </summary>
    /// <param name="moveRange">Range in which the unit can move</param>
    protected void FindSelectableTiles(int moveRange, bool isPlayerAction)
    {
        if (!hasInit)
            Init(1.0f);
        ResetAllTiles();
        //Initialize all Neighbors
        foreach (GameObject tile in allTiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors();
        }
        //Find current Tile
        Tile currentTile = GetCurrentTile();

        Queue<Tile> bfsQueue = new Queue<Tile>();

        bfsQueue.Enqueue(currentTile);
        currentTile.visited = true;

        while (bfsQueue.Count > 0)
        {
            Tile t = bfsQueue.Dequeue();

            selectableTiles.Add(t);
            if (isPlayerAction)
                t.isSelectable = true;

            if (t.distance < moveRange)
            {
                foreach (Tile adjtile in t.AdjacentTiles)
                {
                    if (!adjtile.visited)
                    {
                        adjtile.parentTile = t;
                        adjtile.visited = true;
                        adjtile.distance = t.distance + 1;
                        bfsQueue.Enqueue(adjtile);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Returns the current Tile the unit is standing on
    /// </summary>
    /// <returns>current tile</returns>
    protected Tile GetCurrentTile()
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 10.0f))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    /// <summary>
    /// Returns the Gameobject of the tile at the given position
    /// </summary>
    /// <param name="position">position of the tile</param>
    /// <returns>Gameobject of the tile</returns>
    protected GameObject GetTileAtPosition(Vector3 position)
    {
        RaycastHit hit;
        GameObject tile = null;

        if (Physics.Raycast((position + new Vector3(0, 10, 0)), Vector3.down, out hit, 15.0f))
        {
            tile = hit.collider.gameObject;
        }

        return tile;
    }

    /// <summary>
    /// Init this instance.
    /// </summary>
    protected void Init(float speed)
    {
        allTiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = this.transform.GetComponent<Collider>().bounds.extents.y;
        this.speed = speed;

        //Initialize all Neighbors
        foreach (GameObject tile in allTiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors();
        }

        moving = false;
        hasInit = true;
    }

    /// <summary>
    /// Moves the unit to the target, using the path
    /// </summary>
    /// <param name="target">Target</param>
    protected void MoveToTarget(GameObject target)
    {
        PathFromTile(target);
        moving = true;
        selectableTiles.Clear();
        StartCoroutine(MoveCoroutine());
    }

    /// <summary>
    /// Calls the Reset() function on all tiles
    /// </summary>
    protected void ResetAllTiles()
    {
        foreach (GameObject tile in allTiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.Reset();
        }
        selectableTiles.Clear();
    }

    protected void UpdateSpeed(float speed)
    {
        this.speed = speed;
    }

    #endregion Protected Methods

    #region Private Methods

    private Vector3 calculateHeading(Vector3 target)
    {
        Vector3 heading = target - transform.position;
        heading.Normalize();
        return heading;
    }

    /// <summary>
    /// Updateloop of Unity
    /// </summary>
    private IEnumerator MoveCoroutine()
    {
        #region movement

        while (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 nextTile = t.transform.position;
            nextTile.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            while (Vector3.Distance(transform.position, nextTile) >= 0.05f)
            {
                Vector3 heading = calculateHeading(nextTile);
                SetHorizontalVelocity(heading);

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
                yield return 0;
            }
            //target reached
            transform.position = nextTile;
            path.Pop();
            yield return 0;
        }
        ResetAllTiles();
        moving = false;
        yield return 0;

        #endregion movement
    }

    /// <summary>
    /// Constructs a path from a selected tile
    /// </summary>
    /// <param name="target">Target tile</param>
    private void PathFromTile(GameObject target)
    {
        path.Clear();
        Tile next = target.GetComponent<Tile>();
        while (next != null)
        {
            path.Push(next);
            next = next.parentTile;
        }
    }

    private void SetHorizontalVelocity(Vector3 heading)
    {
        velocity = heading * speed;
    }

    #endregion Private Methods
}