using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandlerNonVR : MonoBehaviour {
    Mod mod;
    LineRenderer laserLine;

    // Use this for initialization
    void Start () {
        mod = ModHandlerNonVR.mod;
        laserLine = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {
        UpdateMod();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            UpdateLaserPos(hit);
    }


    void UpdateLaserPos(RaycastHit hit)
    {
        laserLine.SetPosition(1, hit.point);
        laserLine.SetPosition(0, new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z - 0.1f));

        UpdateLaserColor();
    }
    void UpdateLaserColor()
    {
        if (mod == Mod.UTILITIES)
            laserLine.GetComponent<Renderer>().material.color = Color.blue;
        else
            laserLine.GetComponent<Renderer>().material.color = Color.green;

    }
    void UpdateMod()
    {
        mod = ModHandlerNonVR.mod;
    }
}
