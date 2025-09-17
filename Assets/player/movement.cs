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
    bool attacking = false;

    public bool canMove = true;
    public bool canLook = true;


    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = !UnityEngine.Cursor.visible;
    }

    void Update()
    {
        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"),rb.linearVelocity.magnitude * Input.GetAxis("Vertical"), Time.deltaTime * 2f)); //sets the animator to negitive if s is pressed

        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) {
            animator.SetFloat("Turn", rb.linearVelocity.magnitude * Input.GetAxis("Horizontal")*4f);//I didn't record a walk to the side animation but this is close enough
                                                                                                 }
            animator.SetFloat("Turn", Mathf.Lerp(animator.GetFloat("Turn"), Input.GetAxis("Mouse X"), Time.deltaTime * 2f)); //Ok so this line of code sets the animator's turn float to a lerp between the the animators turn float and the mouse x input in order to make the characters feet do a tiny shuffle while turning.
        

        if (canLook == true)
        {            //Rotation
            animator.SetFloat("AttackHeight",rotationX);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            
        }
        if (Input.GetKeyDown(KeyCode.Q) && attacking == false)
        {
            animator.SetTrigger("MidSlash");
            StartCoroutine(attack(1.8f));
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
        attacking = true;
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
        attacking = false;
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