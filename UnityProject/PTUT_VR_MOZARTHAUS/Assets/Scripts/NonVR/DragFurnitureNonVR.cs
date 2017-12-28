using UnityEngine;
using System.Collections;

public class DragFurnitureNonVR : MonoBehaviour {
    LineRenderer laserLine;
    GameObject furnitureSelected;
    GameObject selectPlan;
    GameObject sphere;
    bool onDrag = false;
    bool isSelected = false;
    Mod mod;



	// Use this for initialization
	void Start () {
        mod = ModHandlerNonVR.mod;
    }

    // Update is called once per frame
    void Update () {

        UpdateMod();
        RaycastHit hit;
               
        if (Physics.Raycast(transform.position, transform.forward, out hit) && mod == Mod.EDITION)
        {
            if (SelectFurniture(hit) && !isSelected && !onDrag)
            {
                furnitureSelected = GameObject.Find(hit.transform.name);
                isSelected = true;
                DrawPlan();
            }

            else if(SelectFurniture(hit) && isSelected && !onDrag)
            {
                furnitureSelected.GetComponent<Collider>().enabled = false;
                GameObject.Destroy(selectPlan);
                onDrag = true;
            }


            else if (FurnitureDeselected())
            {
                onDrag = false;
                isSelected = false;
                furnitureSelected.GetComponent<Collider>().enabled = true;
                furnitureSelected = null;
                GameObject.Destroy(selectPlan);
            }
                

            if (onDrag)
            {
                furnitureSelected.transform.position = Move(furnitureSelected, hit);
            }
        }
    }

    void UpdateMod()
    {
        this.mod = ModHandlerNonVR.mod;
    }

    bool FurnitureSelected(RaycastHit hit) { return hit.transform.tag == "Select_Sphere" && Input.GetMouseButtonDown(0) && !onDrag; }
    bool SelectFurniture(RaycastHit hit) { return hit.transform.tag == "Furniture" && Input.GetMouseButtonDown(0); }
    bool FurnitureDeselected() { return Input.GetMouseButtonDown(0) && (onDrag || isSelected); }
    
    
    void DrawPlan()
    {
        selectPlan = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Vector3 pos = furnitureSelected.GetComponent<Collider>().bounds.center;
        pos.y = 0.1f;
        selectPlan.transform.position = pos;
        selectPlan.transform.rotation = furnitureSelected.transform.rotation;
        selectPlan.transform.localScale = new Vector3(furnitureSelected.transform.localScale.x*1.5f, 0.01f, furnitureSelected.transform.localScale.x*1.5f);
        selectPlan.GetComponent<Renderer>().material.color = Color.green;
    }

    Vector3 Move(GameObject gameObject, RaycastHit destination)
    {
        Vector3 newPos = destination.point;
        newPos.x -= (gameObject.transform.localScale.x) / 2 * (transform.forward.x / Mathf.Abs(transform.forward.x));
        newPos.y = (gameObject.transform.localScale.y) / 2;
        newPos.z -= (gameObject.transform.localScale.z) / 2 * (transform.forward.z / Mathf.Abs(transform.forward.z));

        return newPos;
    }
    
}
