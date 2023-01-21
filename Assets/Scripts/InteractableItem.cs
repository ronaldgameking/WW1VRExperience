using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public GameObject PopupUIGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerTag>() != null)
        {
            PopupUIGameObject.SetActive(true);
            AudioManager.Instance.Play("en_us:germangenades");
        }
    }
}
