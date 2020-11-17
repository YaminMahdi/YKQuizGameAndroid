using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class StartManager : MonoBehaviour
{

    public GameObject canvas2;
    public Text BestScoreText, GamePlayed;
    public AudioSource startBtn;
    void Start()
    {
        canvas2.SetActive(true);
        if (PlayerPrefs.GetInt("score", 0) != 0)
            BestScoreText.text = "Last Best Score: " + PlayerPrefs.GetInt("score").ToString();
        if (PlayerPrefs.GetInt("playcount", 0) != 0)
            GamePlayed.text = "Game Played    : " + PlayerPrefs.GetInt("playcount").ToString() + " Times";
        //Debug.Log("play count" + PlayerPrefs.GetInt("playcount", 0));
    }
    public void startGame()
    {
        startBtn.Play();
        StartCoroutine(DelayUI());
        
    }
    IEnumerator DelayUI()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("StarterScene");
        //gameTime = 30;

    }

}
