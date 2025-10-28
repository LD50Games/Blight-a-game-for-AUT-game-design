
using UnityEngine;
using static UnityEngine.UI.Image;

public class CameraCollision : MonoBehaviour
{
    public Transform Camera;
    public Transform CameraEnd;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, CameraEnd.position - transform.position, out hit, 1.537475f))
        {
            Camera.position = hit.point;
        }
        else
        {
            Camera.position = Vector3.Lerp(Camera.position, CameraEnd.position, 2 * Time.deltaTime);
        }
    }
}
