using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Threading;
using System;


public class GameManager : MonoBehaviour
{
    public Question[] ques;
    private Question crntQues;
    private List<Question> UnAnsQues;

    //private static int
    private int quesIndex, yk, saveIndex; 
    public float gameTime=20f;
    public Text quesText, crntScore, crntlavel, gameTimeTxt, lvlUpText, cnvs3txt, cnvs6txt, cnvs6txt1;
    //Rect popupWindow = new Rect(Screen.width /2 -65, Screen.height / 2 - 50, 130, 100);

    private int score=0, solved=0, level=1, x=10, showTime=0, life=3, fstIndex=1, lastIndex=10;

    public AudioSource endGame;
    public GameObject canvas, canvas3, canvas4, canvas5, canvas5_1, canvas6, canvas7, fg1, fg2, fg3;// RedAlart;

    //public GUIStyle GameOverTxt;


    // Start is called before the first frame update
    void Start()
    {
        if (UnAnsQues==null||UnAnsQues.Count==0)
        {
            Debug.Log("if ok");
            UnAnsQues = ques.ToList<Question>();
        } 
        updateQues(fstIndex, lastIndex);
    }
  
    private void updateQues(int startIndex, int endIndex)
    {
        
        while (true)
        {
            quesIndex = UnityEngine.Random.Range(startIndex, endIndex);
            if(UnAnsQues[quesIndex].fact != null)
            {
                if(quesIndex != saveIndex || solved==x-1)
                {
                    saveIndex = quesIndex;
                    crntQues = UnAnsQues[quesIndex];
                    quesText.text = crntQues.fact;
                    break;
                }
                
            }    
        }
        Debug.Log("quesIndex " + quesIndex);
        Debug.Log("Ans- " + crntQues.isTrue);

        //UnAnsQues.RemoveAt(quesIndex);

    }
    IEnumerator hideUI(GameObject Canvas5hide, float secondsToWait, bool hide)
    {
        showTime = 0;
        yield return new WaitForSeconds(secondsToWait);
        Canvas5hide.SetActive(hide);
        
        showTime = 1;
    }
    IEnumerator DelayQues(int fstIn,int lastIn)
    {
        showTime = 0;
        yield return new WaitForSeconds(.5f);
        updateQues(fstIn, lastIn);

        showTime = 1;
    }

    public void canvas4hide()
    {
        canvas4.SetActive(false);
        showTime = 1;
    }

    public void result(bool isTrue)
    {

        if (isTrue == crntQues.isTrue)
        {
            canvas5_1.SetActive(true);
            //StartCoroutine(hideUI(canvas5.1, .5f, true)); // show UI then Wait .5 seconds 
            StartCoroutine(hideUI(canvas5_1, .5f, false)); //hide UI then Wait .5 seconds  
            UnAnsQues[quesIndex].fact = null;
            solved++;
            if (solved == x && level <= 5)
            {
                //showTime = 0;
                level++;
                if (level == 5 && life == 1)
                {
                    life++;
                }
                
            }
            Debug.Log("solved- " + solved);
            if (solved == 50)
            {
                canvas6.SetActive(true);
                showTime = 0;
                endGame.Play();
            }
            else if (solved == x)
            {
                showTime = 0;
                crntlavel.text = "Lavel: " + level.ToString();
                canvas4.SetActive(true);
                lvlUpText.text = (level - 1).ToString() + "-->" + level.ToString();
                x += 10;
                if (level == 2)
                    gameTime = 14f;
                else if (level == 3)
                    gameTime = 16f;
                else if (level == 4)
                    gameTime = 12f;
                else
                    gameTime = 10f;
            }  
            if (level == 1)
            {
                Debug.Log("lvl 1");
                fstIndex = 0;
                lastIndex = 10;
                score += 10;
                gameTime = 20f;
            }
            else if (level == 2)
            {
                Debug.Log("lvl 2");
                fstIndex = 10;
                lastIndex = 20;
                score += 15;
                gameTime = 14f;
            }
            else if(level == 3)
            {
                Debug.Log("lvl 3");
                fstIndex = 20;
                lastIndex = 30;
                score += 20;
                gameTime = 16f;
            }
            else if (level == 4)
            {
                Debug.Log("lvl 4");
                fstIndex = 30;
                lastIndex = 40;
                score += 25;
                gameTime = 12f;
            }
            else
            {
                Debug.Log("lvl 5");
                fstIndex = 40;
                lastIndex = 49;
                score += 30;
                gameTime = 10f;
            }
            if (PlayerPrefs.GetInt("score") < score)
                PlayerPrefs.SetInt("score", score);
            if (solved == 49)
            {
                crntQues = UnAnsQues[49];
                quesText.text = crntQues.fact;
            }
            else if (solved != 50)
                StartCoroutine(DelayQues( fstIndex, lastIndex));

            crntScore.text = score.ToString();
            if(canvas4.activeSelf==false)
                showTime = 1;
            //Debug.Log("true");
            
        }
        else
        {
            //Debug.Log("false");
            life--;
            score -= 5;
            if (PlayerPrefs.GetInt("score") < score)
                PlayerPrefs.SetInt("score", score);
            crntScore.text = score.ToString();

            canvas5.SetActive(true);
            //StartCoroutine(hideUI(canvas5, .5f, true)); // show UI then Wait .5 seconds 
            StartCoroutine(hideUI(canvas5, .5f, false)); //hide UI then Wait .5 seconds  
            if (life != 0)
            {
                if (level == 1)
                {
                    gameTime = 20f;
                    StartCoroutine(DelayQues(0, 10));
                }
                else if (level == 2)
                {
                    gameTime = 14f;
                    StartCoroutine(DelayQues(10, 20));
                }
                else if (level == 3)
                {
                    gameTime = 16f;
                    StartCoroutine(DelayQues(20, 30));
                }
                else if (level == 4)
                {
                    gameTime = 12f;
                    StartCoroutine(DelayQues(30, 40));
                }
                else if(solved == 49)
                {
                    cnvs6txt1.color = Color.red;
                    cnvs6txt1.text = "LoSeR!!";
                    cnvs6txt.text = "You Failed To Win.";
                    canvas6.SetActive(true);
                    showTime = 0;
                    endGame.Play();
                }
                else
                {
                    gameTime = 10f;
                    StartCoroutine(DelayQues( 40, 49));
                }
            }
            if (life == 2)
            {
                fg2.SetActive(true);
                fg3.SetActive(false);
            }                
            else if(life == 1)
            {
                fg2.SetActive(false);
            }
            else if (life == 0)
            {
                fg1.SetActive(false);
                gameTime = 0.23236f;
                showTime = 0;
                cnvs3txt.text = "Game Over!!";
                canvas3.SetActive(true);
            }
                
            //_ = quesText.hideFlags;
            //gameOver = true;
        }
    }
  
    public void PopUp(bool KhelaHobe)
    {
        yk = PlayerPrefs.GetInt("playcount", 0);
        yk++;
        PlayerPrefs.SetInt("playcount", yk);
        if(KhelaHobe)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            /*UnAnsQues.Clear();
            new List<Question>();
            UnAnsQues = ques.ToList<Question>();
            //canvas.SetActive(true);
            canvas3.SetActive(false);
            canvas6.SetActive(false);
            cnvs3txt.text = "Game Over!!";
            life = 3;
            fg1.SetActive(true);
            fg2.SetActive(true);
            fg3.SetActive(true);
            level = 1;
            score = 0;
            solved = 0;
            x = 10;
            updateQues(0, 10);
            crntlavel.text = "Lavel: " + level.ToString();
            crntScore.text = score.ToString();
            gameTime = 20f;
            showTime = 1;
            */
        }
        else
        {
            Application.Quit();
        }
    }

    //public CanvasDelay objA;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTime > 0.23236f && showTime == 1)
        {
            gameTimeTxt.text = (Convert.ToInt32(gameTime)).ToString();
            gameTime -= Time.deltaTime;
        }
        //Debug.Log("overrrrrr " + gameTime);
        if (gameTime < 0.3f && gameTime != 0.23236f)
        {
            gameOver();
            //Debug.Log("overrrrrr " + gameTime);
        }
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                exitWindow();
                // Quit the application
            }
        }
    }

    public void exitWindow()
    {
        canvas7.SetActive(true);
        showTime = 0;
        
    }
    public void canvas7hide(bool isTrue)
    {
        if (isTrue)
        {
            yk = PlayerPrefs.GetInt("playcount", 0);
            yk++;
            PlayerPrefs.SetInt("playcount", yk);
            Application.Quit();
        }
        else
        {
            canvas7.SetActive(false);
            showTime = 1;
        }
    }    
    public void gameOver()
    {
        canvas3.SetActive(true);
        //playaudio.Play();
        cnvs3txt.text = "Time Over!!";
        showTime = 0;
    }
}
