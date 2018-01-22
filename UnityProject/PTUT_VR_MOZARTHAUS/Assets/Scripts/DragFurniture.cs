using UnityEngine;

public class DragFurniture : MonoBehaviour {

    private bool isOnDrag = false;
    private GameObject furnitureSelected;

    private RayCast rayCast;
    private InputManager inputManager;
    private ModHandler modHandler;

    private bool canClick = true;

    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
    }

    void Update()
    {
        if (modHandler.IsInEditionMod() && rayCast.Hit())
        {
            if (inputManager.IsTriggerClicked() && canClick)
            {
                if (isOnDrag)
                {
                    furnitureSelected.GetComponent<Collider>().enabled = true;
                    furnitureSelected = null;
                    isOnDrag = false;
                    canClick = false;
                }
                else if (rayCast.HitFurniture())
                {
                    furnitureSelected = GameObject.Find(rayCast.GetHit().transform.name);
                    furnitureSelected.GetComponent<Collider>().enabled = false;
                    isOnDrag = true;
                    canClick = false;
                }
            }
            else if (isOnDrag)
            {
                UpdateFurniturePosition(rayCast.GetHit());
            }

            if (!canClick)
            {
                canClick = !inputManager.IsTriggerClicked();
            }
        }
    }

    private void UpdateFurniturePosition(RaycastHit hit)
    {
        Vector3 newPos = hit.point;
        newPos.x -= furnitureSelected.transform.localScale.x / 1.5f;
        newPos.y = furnitureSelected.transform.localScale.y / 2;
        newPos.z += furnitureSelected.transform.localScale.z / 1.5f;
        furnitureSelected.transform.position = newPos;
    }

    public void SelectObject(GameObject gameObject)
    {
        furnitureSelected = gameObject;
        furnitureSelected.GetComponent<Collider>().enabled = false;
        isOnDrag = true;
    }

    public bool IsFurnitureSelected()
    {
        return furnitureSelected != null;
    }
    public GameObject GetFurnitureSelected()
    {
        return furnitureSelected;
    }

}
