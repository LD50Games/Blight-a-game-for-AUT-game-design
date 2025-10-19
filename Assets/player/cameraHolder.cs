using UnityEngine;

public class cameraHolder : MonoBehaviour
{

    public GameObject object_;
    void Update()
    {
        object_.transform.position = transform.position;
        object_.transform.rotation = transform.rotation;
        
    }
}
