using UnityEngine;

public class RayGrabbing : MonoBehaviour
{
    public Transform ObjectGrabbed;
    public Transform ObjectParent;

    public void Pickup(Transform grabObj)
    {
        ObjectGrabbed = grabObj;
        ObjectGrabbed.GetComponent<Rigidbody>().isKinematic = true;
        ObjectGrabbed.GetComponent<Rigidbody>().useGravity = false;
        ObjectGrabbed.parent = ObjectParent;
    }
    public void Drop()
    {
        ObjectGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        ObjectGrabbed.GetComponent<Rigidbody>().useGravity = true;
        ObjectGrabbed.parent = null;
        ObjectGrabbed = null;
    }
}
