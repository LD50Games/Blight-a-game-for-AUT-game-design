using UnityEngine;
using System.Collections;


public class movement : MonoBehaviour
{
    public GameObject playerCamera;
    public Rigidbody rb;
    //public Animator animator;
    public float _speed = 9f;
    private Vector2 _input;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    private Vector3 _movementVector;
    public Animator animator;
    float rotationX = 0;

    public bool canMove = true;
    public bool canLook = true;


    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = !UnityEngine.Cursor.visible;
    }

    void Update()
    {
        animator.SetFloat("Speed", rb.linearVelocity.magnitude / 4f + 0.05f);



        if (canLook == true)
        {            //Rotation
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("MidSlash");
            StartCoroutine(attack(1f));
        }
    }
    public void BeStill()
    {
        canLook = false;
        canMove = false;
        animator.enabled = false;
    }
    IEnumerator attack(float seconds)
    {
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }
    private void FixedUpdate()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //Gets the movement vector from inputs
        _input = _input.normalized;
        if (canMove == true)
        {
            //Movement
            _movementVector = _input.x * transform.right * _speed + _input.y * transform.forward * _speed;
            float t = 1 * Time.deltaTime; 
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(_movementVector.x, rb.linearVelocity.y, _movementVector.z), t); //Applies the movement using a Lerp 

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) //Sets the running speed
            {
                _speed = 13;
            }
            else
            {
                _speed = 9;
            }
        }
    }
}