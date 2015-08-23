using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    int score;
    int highScore;

    PlayerController player;
    FadeIn fade;

    public Text scoreText;
    public GameObject highScoreText;
    public GameObject highScoreTrophy;
    public bool playerDead;

	void Start () {
        player = FindObjectOfType<PlayerController>();
        fade = GetComponent<FadeIn>();
        highScore = PlayerPrefs.GetInt("highScore");
        Debug.Log(highScore);
    }

    bool isRestarting = false;
	
	void Update () {
        scoreText.text = score.ToString();

        if (score > highScore && playerDead)
        {
            highScoreText.SetActive(true);
            highScoreTrophy.SetActive(true);
            PlayerPrefs.SetInt("highScore", score);
            Debug.Log(PlayerPrefs.GetInt("highScore"));
        }

        if (Input.anyKeyDown && playerDead)
        {
            StartCoroutine(Restart());
            isRestarting = true;
        }
    }

    IEnumerator Restart()
    {
        if(!isRestarting)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            var fadeTime = fade.BeginFade(1);

            yield return new WaitForSeconds(fadeTime);
            Application.LoadLevel(Application.loadedLevel);

        }
    }

    public void SetScore(int points)
    {
        score += points;
    }
}
