using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour {

    public GameObject player;

    LineRenderer laser;

    private GameObject controller;
    private Vector3 controllerPos;
    private Vector3 controllerDirection;

    private const string CURSOR_CONTROLLER_NAME = "PointerController"; // Left Controller
    private const string TELEPORTATION_TRIGGER_NAME = "LeftControllerTrigger";

    private bool canTeleport = true;

    private Mod mod;

    

    /******************/
    /* Initialisation */
    /******************/

    void Start() {
        controller = GameObject.Find(CURSOR_CONTROLLER_NAME);

        laser = gameObject.GetComponent<LineRenderer>();
        laser.enabled = true;
    }


    /**********/
    /* Update */
    /**********/

    void Update()
    {
        
        UpdateControllerPosAndDirection();
        UpdateMod();

        RaycastHit hit;
        Ray ray = new Ray(controllerPos, controllerDirection);

        if (Physics.Raycast(ray, out hit, 10000.0f))
        {
           
            UpdateLaserPos(hit.point);
            laser.enabled = true;
        }
        else
        {
            laser.enabled = false;
        }


        if (IsTriggerClicked() && canTeleport && mod == Mod.UTILITIES)
        {
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                Teleport(hit.point, controllerDirection, controllerPos);
                canTeleport = false;
            }
        }
        else if (!IsTriggerClicked()) {
            canTeleport = true;
        }
    }


    void UpdateControllerPosAndDirection()
    {
        controllerPos = controller.transform.position;
        controllerDirection = controller.transform.forward;
    }

    void UpdateLaserPos(Vector3 hitPosition)
    {
        laser.SetPosition(0, controllerPos);
        laser.SetPosition(1, hitPosition);

        UpdateLaserColor();
    }
    void UpdateLaserColor()
    {
        if (mod == Mod.UTILITIES)
            laser.GetComponent<Renderer>().material.color = Color.cyan;

        else
            laser.GetComponent<Renderer>().material.color = Color.green;
    }

    void UpdateMod()
    {
        this.mod = ModHandler.mod;
    }

    bool IsTriggerClicked() {
        return (Input.GetAxis(TELEPORTATION_TRIGGER_NAME) == 1);
    }
    void Teleport(Vector3 hitPosition, Vector3 leftControllerDirection, Vector3 leftControllerPos)
    {
        Vector3 newCameraPos = hitPosition;
        newCameraPos.y = 0;
        player.transform.position = newCameraPos;
    }

}



