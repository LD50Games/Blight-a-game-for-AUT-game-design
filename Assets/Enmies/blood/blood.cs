using UnityEngine;
using System.Collections;


public class blood : MonoBehaviour
{
    
    private Vector3 originalRotation;

    private void Start()
    {
        StartCoroutine(timeout());
        originalRotation = transform.rotation.eulerAngles;
    }

    void Update()
    {


        transform.LookAt(Camera.main.transform.position, Vector3.up);
                

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = originalRotation.x; 
        rotation.y = originalRotation.y; 
 rotation.z = originalRotation.z; 
        transform.rotation = Quaternion.Euler(rotation);
    }
    IEnumerator timeout()
    {
        yield return new WaitForSeconds(0.41675f);
        Destroy(this.gameObject);
    }
}

