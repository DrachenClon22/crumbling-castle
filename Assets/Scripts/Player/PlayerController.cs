using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 10f;
    public float SprintSpeed = 14f;

    private float _currentSpeed = 0f;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _currentSpeed = Speed;
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            InteractableController.Interactable?.GetComponent<IInteractable>().BeginInteract();
        }
        if (Input.GetMouseButton(0))
        {
            InteractableController.Interactable?.GetComponent<IInteractable>().ContinueInteract();
            Trail.Instance.DrawLine();
        }
        if (Input.GetMouseButtonUp(0))
        {
            InteractableController.Interactable?.GetComponent<IInteractable>().EndInteract();
            Trail.Instance.ClearLine();
        }

        // TODO reassign keys
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, SprintSpeed, 10f*Time.deltaTime);
        } else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, Speed, 10f * Time.deltaTime);
        }

        Vector3 moveDirectionForward = transform.forward * verticalInput;
        Vector3 moveDirectionSide = transform.right * horizontalInput;

        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        Vector3 distance = direction * _currentSpeed * Time.deltaTime;

        _characterController.Move(distance);
    }
}
