using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text scoreTextIngame;

    void Awake() => Instance = this;

    public void End(int score)
    {
        scoreText.text = $"SCORE{Environment.NewLine}{score}m";
        StartCoroutine(RestartRoutine());
        scoreTextIngame.gameObject.SetActive(false);
    }

    IEnumerator RestartRoutine()
    {
        yield return new WaitForSecondsRealtime(5f);
        SceneManager.LoadScene(0);
    }
    public void setScore(int score)
    {
        scoreTextIngame.text  = $"SCORE{Environment.NewLine}{score}m";
    }
}
