using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class onTriggerEnter : MonoBehaviour
{
    public UnityEvent function;
    void OnTriggerEnter2D(Collider2D col)
    {
        
        function.Invoke();
    }
}
