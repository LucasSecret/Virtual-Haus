using UnityEngine;

public class MovableUIHandler : MonoBehaviour {

    private RayCast rayCast;
    private InputManager inputManager;

    private DragFurniture dragFurniture;

    void Start () {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();

        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();
    }

    void Awake()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update () {
        if (MoveButtonClicked())
        {
            dragFurniture.MoveSelectedObject();
        }
        else if (RemoveButtonClicked())
        {
            dragFurniture.HideSelectedObject();
        }
    }

    private bool MoveButtonClicked()
    {
        return rayCast.Hit() && (rayCast.GetHit().transform.name == "MoveButton" && inputManager.IsTriggerClicked());
    }
    private bool RemoveButtonClicked()
    {
        return rayCast.Hit() && (rayCast.GetHit().transform.name == "RemoveButton" && inputManager.IsTriggerClicked());
    }
}
