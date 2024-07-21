using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    public float Radius = 10f;

    private List<IInteractable> _interactables = new List<IInteractable>();
    private void Update()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, Radius);
        var hits = hit.Select(x => x.GetComponent<IInteractable>());
        var deleted = _interactables.Except(hits).ToArray();
        foreach (var item in deleted)
        {
            _interactables.Remove(item);
            item.EndInteract();
        }
        foreach (var item in hits)
        {
            if (item is not null)
            {
                if (_interactables.Contains(item))
                {
                    continue;
                }
                else
                {
                    _interactables.Add(item);
                    item.BeginInteract();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, Radius);
    }
}
