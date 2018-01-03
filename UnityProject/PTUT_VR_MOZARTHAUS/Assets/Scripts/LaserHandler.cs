using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHandler : MonoBehaviour
{

    private LineRenderer laserLine;
    private RayCast rayCast;
    private ModHandler modHandler;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
    }

    void Update()
    {
        if (rayCast.Hit())
        {
            laserLine.enabled = true;
            UpdateLaserPos(rayCast.GetHit());
            UpdateLaserColor();
        }
        else
        {
            laserLine.enabled = false;
        }
    }

    private void UpdateLaserPos(RaycastHit hit)
    {
        laserLine.SetPosition(1, hit.point);
        laserLine.SetPosition(0, new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z - 0.1f));

        UpdateLaserColor();
    }
    private void UpdateLaserColor()
    {
        if (modHandler.IsInEditionMod())
        {
            laserLine.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            laserLine.GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
