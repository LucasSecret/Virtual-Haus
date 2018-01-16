using UnityEngine;

public class RayCast : MonoBehaviour {

    public GameObject source;
 
    private RaycastHit hit;
    private bool rayCastHit;
	
	void Update () {
        rayCastHit = Physics.Raycast(source.transform.position, source.transform.forward, out hit);
    }

    public bool Hit()
    {
        return rayCastHit;
    }
    public bool HitFurniture()
    {
        return hit.transform.tag == "Furniture";
    }

    public bool HitMoveFurnitureButton()
    {
        
        return hit.transform.name == "MoveButton";
    }
    
    public RaycastHit GetHit()
    {
        return hit;
    } 
}
