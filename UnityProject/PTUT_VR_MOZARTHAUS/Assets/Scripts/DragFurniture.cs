using UnityEngine;

public class DragFurniture : MonoBehaviour {

    private bool isOnDrag = false;
    private bool isClicked = false;
    private GameObject furnitureSelected;
    private GameObject planOfFurnitureSelected;

    private RayCast rayCast;
    private InputManager inputManager;
    private ModHandler modHandler;

    private bool canClick = true;

    public GameObject furnitureUI;
    private GameObject instanciatedUI;
    void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
    }

    void Update ()
    {
        if (modHandler.IsInEditionMod() && rayCast.Hit())
        {
            if (inputManager.IsTriggerClicked() && canClick)
            {
                
                if ((isOnDrag || isClicked) && !rayCast.HitFurniture() && !rayCast.HitMoveFurnitureButton())
                {
                    furnitureSelected.GetComponent<Collider>().enabled = true;
                    furnitureSelected = null;
                    isClicked = false;
                    isOnDrag = false;
                    canClick = false;
                    DestroyFurniturePlan();
                    DestroyFurnitureUI();
                }
                else if (rayCast.HitFurniture() && !isClicked)
                {
                    furnitureSelected = GameObject.Find(rayCast.GetHit().transform.name);
                    isClicked = true;
                    canClick = false;
                    DrawFurniturePlan();
                    DrawFurnitureUI();
                }

                else if (rayCast.HitMoveFurnitureButton() && isClicked && !isOnDrag)
                {
                    
                    isOnDrag = true;
                    furnitureSelected.GetComponent<Collider>().enabled = false;
                    DestroyFurniturePlan();
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
                canClick = !inputManager.IsTriggerClicked();
            }
        }
    }

    private void UpdateFurniturePosition(RaycastHit hit)
    {
        Vector3 newPos = hit.point;
        newPos.x -= (furnitureSelected.transform.localScale.x / 1.5f);
        newPos.y = furnitureSelected.transform.localScale.y / 2;
        newPos.z += furnitureSelected.transform.localScale.z * hit.transform.forward.z/Mathf.Abs(hit.transform.forward.z);
        furnitureSelected.transform.position = newPos;
    }

    private void DrawFurniturePlan()
    {
        planOfFurnitureSelected = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 pos = furnitureSelected.GetComponent<Collider>().bounds.center;
        pos.y = 0.1f;
        planOfFurnitureSelected.transform.position = pos;
        planOfFurnitureSelected.transform.rotation = furnitureSelected.transform.rotation;
        planOfFurnitureSelected.transform.localScale = new Vector3(furnitureSelected.transform.localScale.x * 1.5f, 0.01f, furnitureSelected.transform.localScale.x * 1.5f);
        planOfFurnitureSelected.GetComponent<Renderer>().material.color = Color.green;
    }
    private void DrawFurnitureUI()
    {
        Vector3 newpos = new Vector3(furnitureSelected.transform.position.x, 
                                    furnitureSelected.transform.localScale.y,
                                    furnitureSelected.transform.position.z);
        furnitureUI.GetComponent<RectTransform>().anchoredPosition3D = newpos;      
        furnitureUI.GetComponent<RectTransform>().LookAt(rayCast.source.transform);
        instanciatedUI =  Instantiate(furnitureUI);
    }

    private void DestroyFurnitureUI()
    {
        Destroy(instanciatedUI);
    }


    private void DestroyFurniturePlan()
    {
        if(planOfFurnitureSelected != null)
            GameObject.Destroy(planOfFurnitureSelected);
    }
}
