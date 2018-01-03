using UnityEngine;

public class ModHandler : MonoBehaviour {

    public static Mod mod = Mod.EDITION;

    private static string MENU_CHANGE_BUTTON_NAME = "ChangeMenuButton";
    private bool canChange = true;


	void Update () {

        Debug.Log(mod);

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
        return Input.GetButton(MENU_CHANGE_BUTTON_NAME);
    }
}

public enum Mod
{
    UTILITIES,
    EDITION
}
