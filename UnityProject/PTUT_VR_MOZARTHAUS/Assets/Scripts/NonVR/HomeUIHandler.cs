using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIHandler : MonoBehaviour
{
    private Mod mod;

    private const string CLICKED_TRIGGER_NAME = "PointerTrigger";
    private const string CLICKED_BUTTON_TRIGGER_NAME = "PointerTrigger";

    private bool canClick = true;

    public GameObject player;

    Vector3 newCameraPos;


    RayCast rayCast;
    

    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }
    public void Update()
    {

        UpdateMod();
      

        if (IsTriggerClicked() && canClick)
        { 

            if (rayCast.GetHit().transform.name.ToString() == "MozartHaus")
            {
                TeleportToStartPosition(player);
                canClick = false;

                mod = Mod.UTILITIES;        
            }
            else if(rayCast.GetHit().transform.name.ToString() == "Appartment")
            {
                print("Appartment");
            }
            else if(rayCast.GetHit().transform.name.ToString() == "SettingsImport")
            {
                print("SettingsImport");
            }

        }
        else if (!IsTriggerClicked())
        {
            canClick = true;
        }
    }

    bool IsTriggerClicked()
    {
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1 || Input.GetButton(CLICKED_BUTTON_TRIGGER_NAME));
    }
    void UpdateMod()
    {
        this.mod = ModHandlerNonVR.mod;
    }

    void TeleportToStartPosition(GameObject gameObject)
    {
        newCameraPos.y = (float) 0.574;
        newCameraPos.x = -2;
        newCameraPos.z = (float)-3.555;
        gameObject.transform.position = newCameraPos;
    }

}
