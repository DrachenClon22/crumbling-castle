using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float Speed = 10f;
    public float SprintSpeed = 14f;

    public TMPro.TMP_Text TMP_Ghosts;
    public TMPro.TMP_Text TMP_Score;
    public TMPro.TMP_Text TMP_Charge;

    private string _ghostsStart;
    private string _scoreStart;
    private string _chargeStart;

    public static int GhostsHeld = 0;
    public static int Score = 0;
    public static float WandCharge = 1f;

    private float _currentSpeed = 0f;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _currentSpeed = Speed;

        _ghostsStart = TMP_Ghosts.text;
        _scoreStart = TMP_Score.text;
        _chargeStart = TMP_Charge.text;
    }
    private void Update()
    {
        WandCharge = Mathf.Clamp01(WandCharge);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        TMP_Ghosts.text = _ghostsStart.Replace("%GHS", GhostsHeld.ToString());
        TMP_Charge.text = _chargeStart.Replace("%CHRG", string.Format("{0:f}", WandCharge));
        TMP_Score.text = TMP_Charge.text.Replace("%scr", Score.ToString());

        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractableController.Interactable?.GetComponent<IInteractable>().BeginInteract();
        }

        if (WandCharge > 0f)
        {
            if (Input.GetMouseButton(0))
            {
                InteractableController.Interactable?.GetComponent<IInteractable>().ContinueInteract();
                Trail.Instance.DrawLine();
                WandCharge -= 0.05f * Time.deltaTime;
            }
        }
        if (WandCharge < 0f || Input.GetMouseButtonUp(0))
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

        _characterController.SimpleMove(distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Finish")
        {
            GameOver.GameOverGame();
        }
    }
}
