using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeGhosts : MonoBehaviour, IInteractable
{
    public void BeginInteract()
    {
        PlayerController.WandCharge += PlayerController.GhostsHeld * 0.25f;
        PlayerController.Score += PlayerController.GhostsHeld * 100;
        PlayerController.GhostsHeld = 0;
    }

    public void ContinueInteract()
    {
        
    }

    public void EndInteract()
    {
        
    }
}
