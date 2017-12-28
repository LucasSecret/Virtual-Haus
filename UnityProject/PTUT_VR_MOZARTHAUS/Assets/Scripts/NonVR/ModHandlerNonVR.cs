using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModHandlerNonVR : MonoBehaviour
{

    public static Mod mod = Mod.UTILITIES;
    private static KeyCode MENU_CHANGE_KEY_NAME = KeyCode.M;

    private bool canChange = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(mod);
        if (IsModChangeButtonClicked() && canChange)
        {
            if (mod == Mod.UTILITIES)
                mod = Mod.EDITION;
            else
                mod = Mod.UTILITIES;

            canChange = false;
        }
        else if (!IsModChangeButtonClicked())
        {
            canChange = true;
        }

    }

    private bool IsModChangeButtonClicked()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
}
