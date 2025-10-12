using UnityEngine;
using UnityEngine.Events;

public class interaction : MonoBehaviour
{
    public UnityEvent function;
    public void interact()
    {
        function.Invoke();
    }
}
