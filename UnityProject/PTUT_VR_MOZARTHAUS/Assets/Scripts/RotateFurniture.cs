using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFurniture : MonoBehaviour {

    private InputManager inputManager;
    private ModHandler modHandler;
    private RayCast rayCast;
    private DragFurniture dragFurniture;

    private static readonly Vector2 DEFAULT_TRACKPAD_POSITION = new Vector2(0, 0);
    private float? previousMenuTrackpadAngle = null;

    void Start() {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();
    }
	
	void Update() {
        if (dragFurniture.IsFurnitureSelected())
        {
            dragFurniture.GetFurnitureSelected().transform.Rotate(new Vector3(0, 0, inputManager.GetTrackpadHandler().GetPointerTrackpadRotationOffset()));
        }
    }
}
