using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController CharacterController;
    [Space(10)]
    public float Speed = 1f;

    private void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal")*Speed,0,Input.GetAxis("Vertical")*Speed);
        CharacterController.Move(move+transform.forward*Time.deltaTime);
    }
}
