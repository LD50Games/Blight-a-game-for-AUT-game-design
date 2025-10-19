using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;


public class movement : MonoBehaviour
{
    public GameObject playerCamera;
    public Rigidbody rb;
    public float _speed = 7f;
    private Vector2 _input;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    private Vector3 _movementVector;
    public Animator animator;
    float rotationX = 0;
    bool attacking = false;
    public int health;
    public bool in_combat = false;
    public bool canMove = true;
    public bool canLook = true;
    public GameObject Camera;
    public GameObject PressE;
    public Transform SwordTip;
    public Transform SwordEdge;
    public LayerMask enemies;
    public LayerMask interactable;
    GameObject npc;
    public GameObject settings;
    public SkinnedMeshRenderer HealthBar;
    private Coroutine attackCoroutine;
    public GameObject blood;
    void Start()
    {
        LockMouse(true);
    }

    void Update()
    {
        HealthBar.SetBlendShapeWeight(0, 100 - health );
        if(health > 1)
        {
            if (in_combat = false && health < 100) 
            {
                health += 1;
            }
        }
        else
        {
            die();
        }
        animator.SetFloat("Speed", Mathf.Lerp(animator.GetFloat("Speed"), rb.linearVelocity.magnitude * Input.GetAxis("Vertical"), Time.deltaTime * 2f)); //sets the animator to negitive if s is pressed

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetFloat("Turn", rb.linearVelocity.magnitude * Input.GetAxis("Horizontal") * 4f);//I didn't record a walk to the side animation but this is close enough
        }
        animator.SetFloat("Turn", Mathf.Lerp(animator.GetFloat("Turn"), Input.GetAxis("Mouse X"), Time.deltaTime * 2f)); //Ok so this line of code sets the animator's turn float to a lerp between the the animators turn float and the mouse x input in order to make the characters feet do a tiny shuffle while turning.


        if (canLook == true)
        {            //Rotation
            animator.SetFloat("AttackHeight", rotationX);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && attacking == false && canLook == true)
        {
            animator.SetTrigger("MidSlash");
            attackCoroutine = StartCoroutine(attack(1.6f));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && attacking == false && canLook == true)
        {
            animator.SetTrigger("Poke");
            attackCoroutine = StartCoroutine(attack(1.4f));
        }
        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out RaycastHit hit, 4f, interactable) && canLook == true && canLook == true)
        {
            PressE.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && attacking == false)
            {
                hit.collider.gameObject.SendMessage("interact");
                npc = hit.collider.gameObject;
                
            }
        }
        else
        {
            PressE.SetActive(false);
        }
        if (canLook == false && Input.GetKeyDown(KeyCode.E)) {
            npc.SendMessage("next");
        }

        if (Input.GetKeyDown(KeyCode.Q) && attacking == false)
        {
            if (canLook == false)
            {
                npc.SendMessage("qOption");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(Time.timeScale != 0);
            
        }
    }
    public void LockMouse(bool locked)
    {
        if (locked)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None; 
            UnityEngine.Cursor.visible = true;
        }
    }
    public void BeStill(bool still)
    {
        canLook = !still;
        canMove = !still;  
    }

    public void Pause(bool PauseON)
    {
        LockMouse(!PauseON);
        BeStill(PauseON);
        if (PauseON)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1; }
        settings.SetActive(PauseON);
    }

    IEnumerator attack(float seconds) //the more contact made with enemy the more damage that will be done
    {
        attacking = true;
        canMove = false;
        yield return new WaitForSeconds(seconds*0.2f);
        CollisionCheck(SwordTip);
        CollisionCheck(SwordEdge);
        yield return new WaitForSeconds(seconds *0.1f);
        CollisionCheck(SwordTip);
        CollisionCheck(SwordEdge);
        yield return new WaitForSeconds(seconds * 0.1f);
        CollisionCheck(SwordTip);
        CollisionCheck(SwordEdge);
        yield return new WaitForSeconds(seconds * 0.1f);
        CollisionCheck(SwordTip);
        CollisionCheck(SwordEdge);
        yield return new WaitForSeconds(seconds *0.1f);
        canMove = true;
        attacking = false;
    }
    public void die()
    {
        SceneManager.LoadScene("YouDied");
    }
    public void CollisionCheck(Transform orgin_) 
    {
        Collider[] hits = Physics.OverlapSphere(orgin_.position, 0.25f, enemies);

        foreach (Collider hit in hits)
            {
            hit.SendMessage("TakeDamage",2);
            Instantiate(blood, hit.transform.position, transform.rotation);
            }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("hit");
        StartCoroutine(hit());
    } 
    IEnumerator hit()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        attacking = true;
        canMove = true;
        yield return new WaitForSeconds(0.6f);
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
            float t = 1.9f * Time.deltaTime; 
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(_movementVector.x, rb.linearVelocity.y, _movementVector.z), t); //Applies the movement using a Lerp 

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) //Sets the running speed
            {
                _speed = 12;
            }
            else
            {
                _speed = 7;
            }
        }
    }
}