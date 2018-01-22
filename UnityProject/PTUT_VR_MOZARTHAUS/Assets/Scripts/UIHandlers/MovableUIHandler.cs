using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableUIHandler : MonoBehaviour {

    private RaycastHit rayCastHit;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        rayCastHit = GameObject.Find("PointerController").GetComponent<RayCast>().GetHit();
    }

    public bool HitMoveFurnitureButton()
    {

        return rayCastHit.transform.name == "MoveButton";
    }

    public bool HitRemoveFurnitureButton()
    {

        return rayCastHit.transform.name == "RemoveButton";
    }

}
