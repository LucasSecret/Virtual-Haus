using UnityEngine;

public class MovableUIHandler : MonoBehaviour {

    private RayCast rayCast;
    private InputManager inputManager;

    private DragFurniture dragFurniture;

    void Start() {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        dragFurniture = GameObject.Find("EditionHandler").GetComponent<DragFurniture>();
    }

    void Update () {
        if (inputManager.IsTriggerClicked() && rayCast.Hit())
        {
            if (HitMoveButton())
            {
                dragFurniture.MakeSelectedObjectMovable();
            }
            else if (HitRemoveButton())
            {
                dragFurniture.RemoveSelectedObject();
            }
        }
    }

    private bool HitMoveButton()
    {
        return (rayCast.GetHit().transform.name == "MoveButton");
    }
    private bool HitRemoveButton()
    {
        return (rayCast.GetHit().transform.name == "RemoveButton");
    }
    public bool HitMovableUI()
    {
        return HitMoveButton() || HitRemoveButton();
    }
}
