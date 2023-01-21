using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject PopupUIGameObject;
    public string NarrotorSoundname = "en_us:germangenades";

    public GameObject ActivatorObject = null;
    //TODO: Somehow get a held object
    public GameObject AbstractHeldObject = null;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerTag>() != null)
        {
            if (AbstractHeldObject == null ||
                AbstractHeldObject != ActivatorObject)
                return;
            
            PopupUIGameObject.SetActive(true);
            AudioManager.Instance.Play(NarrotorSoundname);
        }
    }
}
