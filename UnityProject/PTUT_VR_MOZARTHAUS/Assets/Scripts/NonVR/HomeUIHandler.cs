using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIHandler : MonoBehaviour
{
    private Mod mod;
    private const string CLICKED_TRIGGER_NAME = "Fire1";
    private bool canClick = true;

    public GameObject player;
    Vector3 newCameraPos;


    public void Start()
    {

    }
    public void Update()
    {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (IsTriggerClicked() && canClick && mod == Mod.EDITION)
        {
            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                Teleport
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
        return (Input.GetAxis(CLICKED_TRIGGER_NAME) == 1);
    }
    void UpdateMod()
    {
        this.mod = ModHandler.mod;
    }

    void TeleportToStartPosition()
    {
        newCameraPos.y = 0;
        newCameraPos.x = -2;
        newCameraPos.z = (float)-3.555;
        player.transform.position = newCameraPos;
    }
}
