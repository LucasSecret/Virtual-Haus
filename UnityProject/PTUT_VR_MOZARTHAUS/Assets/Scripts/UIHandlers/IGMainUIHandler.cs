using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMainUIHandler : MonoBehaviour {

    private RayCast rayCast;
    private InputManager inputManager;
    private GameObject menuController;
    //private SavingManager savingManager;
    

    // Use this for initialization
    void Start () {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        menuController = GameObject.Find("MenuController");
    }
	
	// Update is called once per frame
	void Update () {
        
        //Vector3 controllerPosition = menuController.transform.position;
        
        //controllerPosition.y += (float) 0.5;
        //controllerPosition.z += 1;
        
        //this.transform.position = controllerPosition;

        //Debug.Log(menuController.transform.position);

        if (inputManager.IsTriggerClicked() && rayCast.Hit())
        {
            Debug.Log(rayCast.GetHit().transform.name);

            if (rayCast.GetHit().transform.name == "Sound")
            {
                print("Sound"); // Not Implemented                
            }
            else if (rayCast.GetHit().transform.name == "Save")
            {
                print("Save"); // Not Implemented
            }
            else if (rayCast.GetHit().transform.name == "SaveAs")
            {
                print("SaveAs"); // Not Implemented
            }
            else if (rayCast.GetHit().transform.name == "Edit")
            {
                print("Edit"); // Not Implemented
            }
            else if (rayCast.GetHit().transform.name == "Quit")
            {
                print("Quit"); // Not Implemented
            }
        }
    }
}
