using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    public float pollingTime = 1.0f; // Time interval in seconds
    private float time = 0f;
    private int frameCount = 0;
    public Material debugMaterial;
    public TextMeshProUGUI TextMesh;
    void Update()
    {
        time += Time.deltaTime;
        frameCount++;

        if (time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            Debug.Log("FPS: " + frameRate);

            time = 0f;
            frameCount = 0;
            debugMaterial.color = new Color (1, frameRate / 30, frameRate / 60);
            TextMesh.text = frameRate.ToString();
        }
    }
}
