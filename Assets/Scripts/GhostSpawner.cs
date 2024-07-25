using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public Transform[] Nodes;
    public GameObject Ghost;
    public float Timer = 10f;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > Timer)
        {
            _timer = 0f;
            GameObject.Instantiate(Ghost, Nodes[Random.Range(0, Nodes.Length)].position, Quaternion.identity);
        }
    }
}
