using Oculus.Interaction;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(Grabbable))]
// [RequireComponent(typeof(GrabInteractable))]
public class InteractableItem : MonoBehaviour
{ public GameObject PopupUIGameObject;
    public string NarrotorSoundname = "en_us:germangenades";
    public int TrackedId = 0;
    
    private void OnTriggerExit(Collider other)
    {
        ItemTag tag = other.gameObject.GetComponent<ItemTag>();
        if (tag != null && tag.id == TrackedId)
        {
            PopupUIGameObject.SetActive(true);
            AudioManager.Instance.Play(NarrotorSoundname);
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        ItemTag tag = other.gameObject.GetComponent<ItemTag>();
        if (tag != null)
        {
            PopupUIGameObject.SetActive(false);
            AudioManager.Instance.FadeOut(NarrotorSoundname);
        }
    }
}
