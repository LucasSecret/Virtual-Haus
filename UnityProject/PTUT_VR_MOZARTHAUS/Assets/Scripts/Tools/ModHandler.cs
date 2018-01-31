using UnityEngine;

public class ModHandler : MonoBehaviour
{
    private Mod mod;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        mod = Mod.EDITION;
    }

    void Update()
    {
        if (inputManager.GetTrackpadHandler().IsMenuTrackpadClicked())
        {
            Vector2 menuTrackPadPos = inputManager.GetTrackpadHandler().GetMenuTrackpadPos();
            if (menuTrackPadPos.x < 0 && menuTrackPadPos.y < 0)
            {
                mod = Mod.REMOVE;
            }
            else if (menuTrackPadPos.x > 0 && menuTrackPadPos.y < 0)
            {
                mod = Mod.UTILITIES;
            }
            else
            {
                mod = Mod.EDITION;
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
}

public enum Mod
{
    UTILITIES,
    EDITION,
    REMOVE
}
