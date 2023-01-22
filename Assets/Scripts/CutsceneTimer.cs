using System;
using UnityEngine;

 public class CutsceneTimer : MonoBehaviour
 {
     public float Finish = 60f;
     public GameObject PlayerCamera;
     public GameObject CutsceneCamera;
     public GameObject CustsceneTimeline;

     private float currentTime = 0f;

     private void Update()
     {
            currentTime += Time.deltaTime;
    
            if (currentTime >= Finish)
            {
                PlayerCamera.SetActive(false);
                CutsceneCamera.SetActive(true);
                CustsceneTimeline.SetActive(true);
            }
     }
 }
