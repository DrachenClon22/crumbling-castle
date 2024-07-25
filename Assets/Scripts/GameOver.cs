using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private static GameOver instance;
    public GameObject ui;
    public TMPro.TMP_Text text;

    private void Start()
    {
        instance = this;
    }
    public static void GameOverGame()
    {
        instance.ui.SetActive(true);
        instance.text.text = instance.text.text.Replace("%scr", PlayerController.Score.ToString());
        SoundManager.SetActived(false);
    }

    private void Update()
    {
        if (CastleManager.Integrity <= 0f)
        {
            GameOverGame();
        }
    }
}
