using UnityEngine;

public class ModHandler : MonoBehaviour
{
    private static Mod mod = Mod.EDITION;
    private InputManager inputManager;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
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
