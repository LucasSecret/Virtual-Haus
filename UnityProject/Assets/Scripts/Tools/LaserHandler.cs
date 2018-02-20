using UnityEngine;

public class LaserHandler : MonoBehaviour
{
    private LineRenderer laserLine;
    private RayCast rayCast;
    private ModHandler modHandler;
    private GameObject cylinder;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        rayCast = GameObject.Find("PointerController").GetComponent<RayCast>();
        modHandler = GameObject.Find("ModHandler").GetComponent<ModHandler>();
        InitCylinderPointer();
    }

    void Update()
    {
        if (rayCast.Hit())
        {
            laserLine.enabled = true;
            UpdateLaserAndPointerPos(rayCast.GetHit());
            UpdateLaserAndPointerColor();
        }
        else
        {
            laserLine.enabled = false;
        }
    }

    private void UpdateLaserAndPointerPos(RaycastHit hit)
    {
        laserLine.SetPosition(1, hit.point);
        laserLine.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));

        cylinder.transform.position = new Vector3(hit.point.x, 0.05f, hit.point.z);


        UpdateLaserAndPointerColor();
    }
    private void UpdateLaserAndPointerColor()
    {
        if (modHandler.IsInEditionMod())
        {
            laserLine.GetComponent<Renderer>().material.color = Color.green;
            cylinder.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (modHandler.IsInUtilitiesMod())
        {
            laserLine.GetComponent<Renderer>().material.color = Color.blue;
            cylinder.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            laserLine.GetComponent<Renderer>().material.color = Color.red;
            cylinder.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void InitCylinderPointer()
    {
        cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.localScale = new Vector3(0.3f, 0.03f, 0.3f);
        cylinder.GetComponent<Collider>().enabled = false;
        cylinder.GetComponent<MeshRenderer>().allowOcclusionWhenDynamic = false;
        cylinder.GetComponent<MeshRenderer>().receiveShadows = false;
        cylinder.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
