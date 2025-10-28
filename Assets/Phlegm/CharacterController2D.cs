using System.Collections;
using UnityEngine;
using System;
public class CharacterController2D : MonoBehaviour
{
    private Vector2 _input;
    public Rigidbody2D _rb;
    public SpriteRenderer sprite;
    public Animator animator;
    private void FixedUpdate()
    {
        _input = new Vector2 (Input.GetAxis("Horizontal")*3f,0); //Gets the movement vector from inputs
        _rb.linearVelocity=_input; //applies to movement vector to the velocity 
        sprite.flipX = _input.x < 0; //determines the direction of the sprite
        animator.SetFloat("Moving", Mathf.Abs(_input.x)); //determines if the sprite is in motion
    }
}