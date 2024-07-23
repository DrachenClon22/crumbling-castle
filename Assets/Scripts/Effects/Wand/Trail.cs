using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public static Trail Instance { get; private set; }

    public LineRenderer[] lineRenderer;
    public int LineAmounts = 5;
    public float Min = -1f;
    public float Max = 1f;

    private float _lightningTimer = 0.2f;
    private float _timer = 0f;

    private void Start()
    {
        Instance = this;
        foreach (var item in lineRenderer)
        {
            item.positionCount = LineAmounts;
        }
    }
    private void BeginDraw(GameObject obj)
    {
        Func<float> getRand = () => UnityEngine.Random.Range(Min, Max);
        foreach (var item in lineRenderer)
        {
            item.SetPosition(0, transform.position);
            item.SetPosition(item.positionCount - 1, obj.transform.position + new Vector3(getRand(), getRand(), getRand()));
            for (int i = 1; i < LineAmounts - 1; i++)
            {
                var vector = Vector3.Lerp(transform.position, obj.transform.position, (float)i / LineAmounts);
                var rand = new Vector3(getRand(), getRand(), getRand());
                item.SetPosition(i, vector + rand);
            }
        }
    }
    public void DrawLine()
    {
        _timer += Time.deltaTime;
        var obj = InteractableController.Interactable;
        
        if (obj is not null)
        {
            foreach (var item in lineRenderer)
            {
                if (_timer > _lightningTimer)
                {
                    BeginDraw(obj);
                }
            }
            if (_timer > _lightningTimer)
            {
                _timer = 0f;
            }
        } else
        {
            for (int i = 0; i < LineAmounts; i++)
            {
                foreach (var item in lineRenderer)
                {
                    item.SetPosition(i, Vector3.one * 99999);
                }
            }
        }
    }

    public void ClearLine()
    {
        for (int i = 0; i < LineAmounts; i++)
        {
            foreach (var item in lineRenderer)
            {
                item.SetPosition(i, Vector3.one * 99999);
            }
        }
    }
}
