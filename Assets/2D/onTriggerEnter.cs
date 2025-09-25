using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class onTriggerEnter : MonoBehaviour
{
    public UnityEvent function;
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        function.Invoke();
    }
}
