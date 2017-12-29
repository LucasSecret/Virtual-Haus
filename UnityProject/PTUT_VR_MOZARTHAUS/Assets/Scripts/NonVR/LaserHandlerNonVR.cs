using UnityEngine;

public class LaserHandlerNonVR : MonoBehaviour {

    private Mod mod;
    private LineRenderer laserLine;
    private RayCast rayCast;

    void Start () {
        mod = ModHandlerNonVR.mod;
        laserLine = this.GetComponent<LineRenderer>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
    }

    void Update () {
        UpdateMod();

        if (rayCast.Hit())
            UpdateLaserPos(rayCast.GetHit());
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
