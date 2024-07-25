using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform text;
    public float scale;
    public float speed;

    private Vector3 _scale;
    private void Start()
    {
        _scale = text.localScale;
    }

    private void Update()
    {
        text.localScale = _scale + new Vector3(Mathf.Sin(Time.time * speed % 360)*scale, Mathf.Sin(Time.time*speed % 360) * scale, 1f);
    }
    public void GoToScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
