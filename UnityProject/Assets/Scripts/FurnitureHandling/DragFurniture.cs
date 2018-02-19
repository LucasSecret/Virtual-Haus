using UnityEngine;

public class DragFurniture : MonoBehaviour {

    private RayCast rayCast;
    private InputManager inputManager;
    private ModHandler modHandler;

    public GameObject movableUI;
    private MovableUIHandler movableUIHandler;

    private GameObject furnitureSelected;

    private ServerNetworkManager networkManager;

    private bool isOnDrag = false;
    private bool isClicked = false;
    private bool canClick = true;


    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        movableUIHandler = movableUI.GetComponent<MovableUIHandler>();

        networkManager = GameObject.Find("NetworkManager").GetComponent<ServerNetworkManager>();

        isOnDrag = isClicked = false;
        canClick = true;
    }

    void Update()
    {
        if (modHandler.IsInEditionMod() && rayCast.Hit())
        {
            if (inputManager.IsTriggerClicked() && canClick)
            {
                canClick = false;

                if (rayCast.HitFurniture() && !isClicked) // Select Game Object
                {
                    furnitureSelected = GameObject.Find(rayCast.GetHit().transform.name);
                    DisplayMovableUIInFrontOfFurniture();

                    isClicked = true;
                }
                else if (isClicked && !isOnDrag && !movableUIHandler.HitMovableUI()) // UnSelect Game Object
                {
                    DestroyMovableUI();

                    isClicked = false;
                }
                else if(isOnDrag) // Place Game Object
                {
                    furnitureSelected.GetComponent<Collider>().enabled = true;
                    furnitureSelected = null;

                    isClicked = isOnDrag = false;
                }
               
            }
            if (isOnDrag) // Move Game Object
            {
                UpdateFurniturePosition(rayCast.GetHit());
                networkManager.SendFurniturePosUpdate(furnitureSelected);
            }

            if (!canClick)
            {
                canClick = !inputManager.IsTriggerClicked();
            }
        }
    }

    private void DisplayMovableUIInFrontOfFurniture()
    {
        movableUI.GetComponent<RectTransform>().anchoredPosition3D = furnitureSelected.transform.position;
        movableUI.GetComponent<RectTransform>().LookAt(rayCast.source.transform);

        Vector3 newpos = furnitureSelected.transform.position + (movableUI.transform.forward - 0.5f * movableUI.transform.right);
        newpos.y = 0.5f;

        movableUI.GetComponent<RectTransform>().anchoredPosition3D = newpos;
    }
    private void DestroyMovableUI()
    {
        movableUI.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, -40, 0);
    }

    public void SelectObject(GameObject gameObject)
    {
        furnitureSelected = gameObject;
        furnitureSelected.GetComponent<Collider>().enabled = false;

        isOnDrag = true;
        canClick = false;
    }
    public void RemoveSelectedObject()
    {
        furnitureSelected.transform.position = new Vector3(0, -50, 0);
        networkManager.SendFurniturePosUpdate(furnitureSelected);

        furnitureSelected.GetComponent<Collider>().enabled = true;
        furnitureSelected = null;

        isClicked = false;

        DestroyMovableUI();
    }
    public void MakeSelectedObjectMovable()
    {
        furnitureSelected.GetComponent<Collider>().enabled = false;
        isOnDrag = true;
        canClick = false;

        DestroyMovableUI();
    }

    private void UpdateFurniturePosition(RaycastHit hit)
    {
        Vector3 newPos = hit.point;
        newPos.y = furnitureSelected.transform.localScale.y / 2;

        furnitureSelected.transform.position = newPos;
    }

    public bool IsFurnitureSelected()
    {
        return furnitureSelected != null;
    }
    public GameObject GetFurnitureSelected()
    {
        return furnitureSelected;
    }
    public bool CanClick()
    {
        return canClick;
    }
}