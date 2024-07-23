using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static PlayerController;

public class InteractableController : MonoBehaviour
{
    public static float Radius = 10f;

    public static GameObject Interactable
    {
        get
        {
            Collider[] hit = Physics.OverlapSphere(GameObject.FindGameObjectWithTag("Player").transform.position, Radius);
            var hits = hit.Where(x => x.GetComponent<IInteractable>() is not null).Select(x => new { INT = x.GetComponent<IInteractable>(), OBJ = x.gameObject });

            return hits.Select(x => x.OBJ).ToArray()?.FirstOrDefault();
        }
    }

    private static List<IInteractable> _interactables = new List<IInteractable>();
    private static Transform _player;
    private void Update()
    {
        //Collider[] hit = Physics.OverlapSphere(GameObject.FindGameObjectWithTag("Player").transform.position, Radius);
        //var hits = hit.Select(x => x.GetComponent<IInteractable>());
        //var deleted = _interactables.Except(hits).ToArray();
        //foreach (var item in deleted)
        //{
        //    _interactables.Remove(item);
        //    item.EndInteract();
        //}
        //foreach (var item in hits)
        //{
        //    if (item is not null)
        //    {
        //        if (_interactables.Contains(item))
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            _interactables.Add(item);
        //            item.BeginInteract();
        //        }
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, Radius);
    }
}
