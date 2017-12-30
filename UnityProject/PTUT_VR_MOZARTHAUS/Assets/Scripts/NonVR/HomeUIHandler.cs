using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIHandler : MonoBehaviour
{
    private const string CLICKED_TRIGGER_NAME = "PointerTrigger";

    private bool canClick = true;

    public GameObject player;
    private Vector3 mozartHausSpawn;
    private RayCast rayCast;
    

    public void Start()
    {
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }

    public void Update()
    {     

        if (IsTriggerClicked() && canClick)
        {
            if (rayCast.GetHit().transform.name != null)
            {

                if (rayCast.GetHit().transform.name.ToString() == "MozartHaus")
                {
                    TeleportToStartPosition(player);
                    canClick = false;

                }
                else if (rayCast.GetHit().transform.name.ToString() == "Appartment")
                {
                    print("Appartment");
                }
                else if (rayCast.GetHit().transform.name.ToString() == "SettingsImport")
                {
                    print("SettingsImport");
                }
            }

        }
        else if (!IsTriggerClicked())
        {
            canClick = true;
        }
    }

    private bool IsTriggerClicked()
    {
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1 || Input.GetButton(CLICKED_TRIGGER_NAME));
    }

    private void TeleportToStartPosition(GameObject gameObject)
    {
        mozartHausSpawn.Set((float)-2, (float)0.574, (float)-3.555);
        gameObject.transform.position = mozartHausSpawn;
       
    }

}
