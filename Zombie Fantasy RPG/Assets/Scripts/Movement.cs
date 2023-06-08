using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10f;
    public float minFall = -1.5f;

    private float vertSpeed;

    private Animator animator;
    private CharacterController charController;

    void Start() 
    {
        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        vertSpeed = minFall;
    }

    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horInput, 0f, vertInput);

        movement = Vector3.ClampMagnitude(movement, 1f);

        if (movement != Vector3.zero) { // Rotate the player to face the direction of movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        movement *= speed;

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (charController.isGrounded) {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity) {
                vertSpeed = terminalVelocity;
            }
        }
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        charController.Move(movement);

        
    }
}
