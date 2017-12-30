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
    public GameObject gameObject;
    Vector3 newCameraPos;


    RayCast rayCast = new RayCast();


    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }
    public void Update()
    {

        UpdateMod();
        Debug.Log(rayCast);
        Debug.Log(rayCast.GetHit().collider.gameObject.tag);

        if (IsTriggerClicked() && canClick &&!(mod == Mod.UTILITIES))
        {

            if (rayCast.GetHit().collider.gameObject.tag == "MozartHaus")
            {
                Debug.Log("You have clicked the button!");
                TeleportToStartPosition(player);
                canClick = false;
                mod = Mod.UTILITIES;
        
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
        newCameraPos.y = 0;
        newCameraPos.x = -2;
        newCameraPos.z = (float)-3.555;
        gameObject.transform.position = newCameraPos;
    }
}
