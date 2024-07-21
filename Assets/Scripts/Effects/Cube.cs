using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public bool Rotate = true;
    public float BobHeight = 0.5f;
    public float BobSpeed = 1f;
    public float CurrentCorrupt = 0f;
    public float CurrentCorruptRate = 30f;

    private Vector3 _scale = Vector3.one;
    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _tempPosition = Vector3.zero;
    private Vector3 _rotation = Vector3.one;
    private Transform _parentObject;

    private float _corruptTimerBased = 0f;
    private float _corruptTimer = 0f;

    private bool _lookAtPlayer = false;

    public void BeginInteract()
    {
        _lookAtPlayer = true;
    }

    public void ContinueInteract()
    {
        
    }

    public void EndInteract()
    {
        _lookAtPlayer = false;
    }

    private void Start()
    {
        var go = new GameObject();
        go.transform.position = transform.position;
        go.transform.rotation = Quaternion.identity;

        _startPosition = go.transform.position;
        _tempPosition = _startPosition;

        _parentObject = go.transform;
        transform.parent = _parentObject;
        transform.localPosition = Vector3.zero;
    }
    private void Update()
    {
        if (CurrentCorruptRate == 0)
        {
            CurrentCorruptRate = 0.1f;
        }
        _corruptTimerBased = CurrentCorrupt / CurrentCorruptRate;
        _corruptTimer += Time.deltaTime;

        if (_corruptTimer > _corruptTimerBased)
        {
            _corruptTimer = 0f;
            _tempPosition = new Vector3(
                _startPosition.x + Random.Range(-CurrentCorrupt, CurrentCorrupt),
                _startPosition.y + Random.Range(-CurrentCorrupt, CurrentCorrupt),
                _startPosition.z + Random.Range(-CurrentCorrupt, CurrentCorrupt)
                );
            _rotation = new Vector3(
                1 + Random.Range(-CurrentCorrupt, CurrentCorrupt),
                1 + Random.Range(-CurrentCorrupt, CurrentCorrupt),
                1 + Random.Range(-CurrentCorrupt, CurrentCorrupt)
                );
        }

        _parentObject.position = new Vector3(
            _tempPosition.x,
            _tempPosition.y + Mathf.Sin(Time.time * BobSpeed % 360) * BobHeight,
            _tempPosition.z);
        _parentObject.localScale = new Vector3(
            _scale.x,
            _scale.y + Mathf.Sin(Time.time * BobSpeed % 360) * BobHeight,
            _scale.z);

        if (_lookAtPlayer)
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        } else
        {
            transform.Rotate(_rotation);
        }
    }
}
