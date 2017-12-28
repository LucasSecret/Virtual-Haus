using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationNonVR : MonoBehaviour {

    public GameObject player;

    private bool canTeleport = true;

    private Mod mod;

    

    /******************/
    /* Initialisation */
    /******************/

    void Start() {
       
    }


    /**********/
    /* Update */
    /**********/

    void Update()
    {
        
      
        UpdateMod();

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (IsTriggerClicked() && canTeleport && mod == Mod.UTILITIES)
        {
            if (Physics.Raycast(ray, out hit, 10000.0f))
            {
                Teleport(player, hit);
                canTeleport = false;
            }
        }
        else if (!IsTriggerClicked())
        {
            canTeleport = true;
        }
        
    }




    void UpdateMod()
    {
        this.mod = ModHandlerNonVR.mod;
    }

    bool IsTriggerClicked()
    {
        return (Input.GetMouseButtonDown(0));
    }


    void Teleport(GameObject gameObject, RaycastHit destination)
    {
        Vector3 newPos = destination.point;
        newPos.y = 0;

        gameObject.transform.position = destination.point;
    }

}



