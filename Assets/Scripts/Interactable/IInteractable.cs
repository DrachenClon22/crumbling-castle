using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void BeginInteract();
    public void ContinueInteract();
    public void EndInteract();
}
