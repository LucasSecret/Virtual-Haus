using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModHandler : MonoBehaviour {


    public static Mod mod = Mod.UTILITIES;
    private InputManager inputManager;

	void Start () {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
	}
	
	void Update () {

        Debug.Log(mod);

        if (inputManager.IsSwitchMenuButtonClicked())
        {
            Debug.Log("Change Mod");
            if (mod == Mod.UTILITIES)
                mod = Mod.EDITION;
            else
                mod = Mod.UTILITIES;
        }
    }
}

public enum Mod
{
    UTILITIES,
    EDITION
}
