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
        if (inputManager.IsModChangeButtonClicked())
            SwitchMod();
    }

    private void SwitchMod()
    {
        if (mod == Mod.UTILITIES)
        {
            mod = Mod.EDITION;
        }
        else
        {
            mod = Mod.UTILITIES;
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
    EDITION
}
