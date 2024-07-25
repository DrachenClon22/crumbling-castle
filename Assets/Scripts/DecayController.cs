using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayController : MonoBehaviour
{
    public Transform[] Nodes;
    public GameObject Ghost;
    public float Timer = 10f;
    public float Lifetime = 10f;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > Timer*CastleManager.Integrity)
        {
            _timer = 0f;
            StartCoroutine(destr(GameObject.Instantiate(Ghost, Nodes[Random.Range(0, Nodes.Length)].position,
                Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)))));
        }
    }

    private IEnumerator destr(GameObject go)
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(go);
    }
}
