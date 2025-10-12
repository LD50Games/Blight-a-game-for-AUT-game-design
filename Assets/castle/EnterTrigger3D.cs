using UnityEngine;
using UnityEngine.Events;

public class EnterTrigger3D : MonoBehaviour
{
    public UnityEvent function;

    private void OnTriggerEnter(Collider other)
    {
        function.Invoke();
    }
}
