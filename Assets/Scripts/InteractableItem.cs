using Oculus.Interaction;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
// [RequireComponent(typeof(Grabbable))]
// [RequireComponent(typeof(GrabInteractable))]
public class InteractableItem : MonoBehaviour
{
    public GameObject PopupUIGameObject;
    public string NarrotorSoundname = "en_us:germangenades";

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<ItemTag>() != null)
        {
            
            PopupUIGameObject.SetActive(true);
            AudioManager.Instance.Play(NarrotorSoundname);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemTag>() != null)
        {
            PopupUIGameObject.SetActive(false);
            AudioManager.Instance.FadeOut(NarrotorSoundname);
        }
    }
}
