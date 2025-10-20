using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float time;
    public UnityEvent function;
    void Start()
    {
        StartCoroutine(Timer_(time));
    }

    
    IEnumerator Timer_(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        function.Invoke();
    }
}
