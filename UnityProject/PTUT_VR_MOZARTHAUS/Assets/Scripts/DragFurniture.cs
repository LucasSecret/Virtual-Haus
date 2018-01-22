using UnityEngine;

public class DragFurniture : MonoBehaviour {

    private bool isOnDrag = false;
    private bool isClicked = false;
    private GameObject furnitureSelected;

    private MovableUIHandler movableUIHandler;
    private RayCast rayCast;
    private InputManager inputManager;
    private ModHandler modHandler;

    private bool canClick = true;

    public GameObject furnitureUI; //Stock the prefab UI passed in parameters
    private GameObject instanciatedUI; //Stock the instanciated UI
    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        movableUIHandler = Resources.Load("Scripts/MovableUIHandler") as MovableUIHandler;
    }

    void Update ()
    {
        if (modHandler.IsInEditionMod() && rayCast.Hit())
        {
            if (inputManager.IsTriggerClicked() && canClick)
            {
                //If user click outside of UI and Furniture is selected or is being dragged 
                if ((isOnDrag || isClicked) && !rayCast.HitFurniture() && !movableUIHandler.HitMoveFurnitureButton() && !movableUIHandler.HitRemoveFurnitureButton())
                {
                    //Deselect object and destroy UI
                    furnitureSelected.GetComponent<Collider>().enabled = true;
                    furnitureSelected = null;
                    isClicked = false;
                    isOnDrag = false;
                    canClick = false;
                    DestroyFurnitureUI();                
                }
                
                //If furniture isn't selected and user click on it
                else if (rayCast.HitFurniture() && !isClicked)
                {
                    //Get GO selected and draw UI
                    furnitureSelected = GameObject.Find(rayCast.GetHit().transform.name);
                    isClicked = true;
                    canClick = false;
                    DrawFurnitureUI();
                }

                //If user click on MoveButton
                else if (movableUIHandler.HitMoveFurnitureButton() && isClicked && !isOnDrag)
                {
                    //Move furniture and destroy UI
                    isOnDrag = true;
                    furnitureSelected.layer = 2;
                    DestroyFurnitureUI();
                    canClick = false;
                }

                //If user click on RemoveButton
                else if (movableUIHandler.HitRemoveFurnitureButton() && isClicked && !isOnDrag)
                {
                    Debug.Log("Remove");
                    furnitureSelected.transform.position = new Vector3(0, -20, 0);
                    DestroyFurnitureUI();
                    canClick = false;
                }

            }
            else if (isOnDrag)
            {
                UpdateFurniturePosition(rayCast.GetHit());
            }

            if (!canClick)
            {
                canClick = !inputManager.IsTriggerClicked(); //Avoid double click
            }
        }
    }

    private void UpdateFurniturePosition(RaycastHit hit)
    {
        Vector3 newPos = hit.point;
        newPos.x += furnitureSelected.transform.localScale.x * hit.transform.right.x / Mathf.Abs(hit.transform.right.x);
        newPos.y = furnitureSelected.transform.localScale.y / 2;
        newPos.z += furnitureSelected.transform.localScale.z * hit.transform.forward.z/Mathf.Abs(hit.transform.forward.z);
        furnitureSelected.transform.position = newPos;
    }

   
    private void DrawFurnitureUI()
    {
        furnitureUI.GetComponent<RectTransform>().anchoredPosition3D = furnitureSelected.transform.position;  
        furnitureUI.GetComponent<RectTransform>().LookAt(rayCast.source.transform);
        Vector3 newpos = furnitureSelected.transform.position + (furnitureUI.transform.forward - 0.5f * furnitureUI.transform.right);
        newpos.y = 0.5f;
        furnitureUI.GetComponent<RectTransform>().anchoredPosition3D = newpos;

        instanciatedUI =  Instantiate(furnitureUI);
    }

    private void DestroyFurnitureUI()
    {
        Destroy(instanciatedUI);
    }
}
