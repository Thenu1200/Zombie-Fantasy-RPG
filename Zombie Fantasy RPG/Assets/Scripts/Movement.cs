using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float speed = 5f;

    private Animator animator;

    void Start() 
    {
        animator = GetComponent<Animator>();
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

        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
}
