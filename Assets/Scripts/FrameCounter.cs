using System;
using UnityEngine;

using Text = TMPro.TextMeshProUGUI;

public class FrameCounter : MonoBehaviour
{
    public Text FrameText;

    private void Update()
    {
        FrameText.text = "FPS: " + (1f / Time.deltaTime).ToString("F0") + "\n" + Time.deltaTime.ToString("F3") + "ms";
    }
}
