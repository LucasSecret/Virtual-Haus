using UnityEngine;
using System.Collections;

public class DragFurniture : MonoBehaviour {

    private GameObject controller;
    private Vector3 controllerPos;
    private Vector3 controllerDirection;

    private GameObject furnitureSelected;
    private bool onDrag = false;
    private bool isDragable = true;

    private float nextDrag = 0.0f;
    private float timeBeforeNextDrag = 1.0f;

    private const string CURSOR_CONTROLLER_NAME = "PointerController"; // Left Controller
    private const string EDITION_TRIGGER_NAME = "LeftControllerTrigger";

    Mod mod;

    void Start()
    {
        controller = GameObject.Find(CURSOR_CONTROLLER_NAME);
    }

    // Update is called once per frame
    void Update () {
        UpdateMod();
        UpdateControllerPosAndDirection();

        RaycastHit hit;
        if (Physics.Raycast(controllerPos, controllerDirection, out hit) && mod == Mod.EDITION)
        {
            if (FurnitureSelected(hit))
            {
                furnitureSelected = GameObject.Find(hit.transform.name);
                furnitureSelected.GetComponent<Collider>().enabled = false;
                onDrag = true;
                isDragable = false;
            }

            else if (FurnitureDeselected())
            {
                onDrag = false;
                isDragable = true;
                furnitureSelected.GetComponent<Collider>().enabled = true;
                furnitureSelected = null;
            }

            if (onDrag && !IsTriggerClicked())
            {
                UpdateFurniturePosition(hit);
            }
        }
    }

    void UpdateControllerPosAndDirection()
    {
        controllerPos = controller.transform.position;
        controllerDirection = controller.transform.forward;
    }

    bool FurnitureSelected(RaycastHit hit) { return hit.transform.tag == "Furniture" && IsTriggerClicked() && isDragable; }
    bool FurnitureDeselected() { return IsTriggerClicked() && onDrag; }

    void UpdateFurniturePosition(RaycastHit hit)
    {
        Vector3 newPos = hit.point;
        newPos.x -= furnitureSelected.transform.localScale.x / 1.5f;
        newPos.y = furnitureSelected.transform.localScale.y / 2;
        newPos.z += furnitureSelected.transform.localScale.z / 1.5f;
        furnitureSelected.transform.position = newPos;
    }

    void UpdateMod()
    {
        mod = ModHandler.mod;
    }

    bool IsTriggerClicked()
    {
        return (Input.GetAxis(EDITION_TRIGGER_NAME) == 1);
    }


}
