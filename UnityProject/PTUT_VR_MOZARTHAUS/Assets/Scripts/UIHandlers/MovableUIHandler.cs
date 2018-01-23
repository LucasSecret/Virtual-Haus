using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableUIHandler : MonoBehaviour {

    private RayCast rayCast;
    private DragFurniture dragFurniture;
    private InputManager inputManager;

    void Start () {
        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }

    private void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }

    void Update () {
       
        if (ClickMoveFurnitureButton())
            dragFurniture.MoveSelectedObject();

        else if (ClickRemoveFurnitureButton())
            dragFurniture.HideSelectedObject();
    }

    

    private bool ClickMoveFurnitureButton()
    {
        if(rayCast.Hit())
            return (rayCast.GetHit().transform.name == "MoveButton" && inputManager.IsTriggerClicked());
        return false;
    }

    private bool ClickRemoveFurnitureButton()
    {
        if(rayCast.Hit())
            return (rayCast.GetHit().transform.name == "RemoveButton" && inputManager.IsTriggerClicked());
        return false;
    }
}
