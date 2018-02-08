using UnityEngine;

public class ModHandler : MonoBehaviour
{
    private Mod mod;
    private InputManager inputManager;
    private GameObject furnitureMenu;

    private void Start()
    {
        mod = Mod.EDITION;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        furnitureMenu = GameObject.Find("FurnitureMenu");
    }

    private void Update()
    {
        if (inputManager.GetTrackpadHandler().IsMenuTrackpadClicked())
        {
            Vector2 menuTrackPadPos = inputManager.GetTrackpadHandler().GetMenuTrackpadPos();
            if (menuTrackPadPos.x < 0 && menuTrackPadPos.y < 0)
            {
                mod = Mod.REMOVE;
                furnitureMenu.SetActive(false);
            }
            else if (menuTrackPadPos.x > 0 && menuTrackPadPos.y < 0)
            {
                mod = Mod.UTILITIES;
                furnitureMenu.SetActive(false);
            }
            else
            {
                mod = Mod.EDITION;
                furnitureMenu.SetActive(true);
            }
        }

        UpdateForNonVR();
    }    
    private void UpdateForNonVR()
    {
        if (Input.GetKey(KeyCode.I))
        {
            mod = Mod.EDITION;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            mod = Mod.UTILITIES;
        }
        else if (Input.GetKey(KeyCode.P))
        {
            mod = Mod.REMOVE;
        }
    }

    /* Getter */

    public bool IsInUtilitiesMod()
    {
        return mod == Mod.UTILITIES;
    }
    public bool IsInEditionMod()
    {
        return mod == Mod.EDITION;
    }
    public bool IsInRemoveMod()
    {
        return mod == Mod.REMOVE;
    }
}

public enum Mod
{
    UTILITIES,
    EDITION,
    REMOVE
}
