using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTUnitMover : MonoBehaviour {

    public GameObject UnitGO;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Unit unit = UnitGO.GetComponent<Unit>();
        if(!unit.moving)
            unit.BeginTurn();
        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.CompareTag("Tile"))
                {
                    if(hit.collider.GetComponent<Tile>().isSelectable)
                        unit.MoveTo(hit.collider.gameObject);
                }
            }
        }
	}
}
