using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public static float Integrity = 1f;
    private string _text;

    private void Start()
    {
        _text = text.text;
    }

    private void Update()
    {
        text.text = _text.Replace("{PERC}", string.Format("{0:P1}", Integrity/1f));
    }
}
