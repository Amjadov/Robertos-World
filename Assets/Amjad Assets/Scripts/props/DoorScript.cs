using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public bool IsOpen = false;
    public bool StopPlayerMoving = false;  // this is in case we needed to zero the x velocity of the player
    public Transform winScreen;
    public float showWinAfter = 2f;
    public bool UseKongRunaway = false; // used in the level where kong runs away at the end
    private bool ActionDone = false;
    private Text GUIS; //Score
    private Text GUIT; //TotalScore
    private Score score;
    private bool StartScoreAnim = false;
    private float S; //Score Var
    private float T; // Total Score Var before Adding the Score
    private float T2; // Total Score var after adding the Score
    public bool SkipCounting = false;

    void Awake()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" && IsOpen == true && ActionDone == false)
        {
            col.transform.GetComponent<Animator>().SetTrigger("Win");
            if (StopPlayerMoving)
                col.rigidbody2D.velocity = new Vector2(0, col.transform.rigidbody2D.velocity.y);
            if (col.transform.GetComponent<PlayerControl_InfiniteRunner>())
            {
                col.transform.GetComponent<PlayerControl_InfiniteRunner>().enabled = false;
                col.audio.Stop();
                if (col.transform.Find("trail1"))
                    col.transform.Find("trail1").gameObject.SetActive(false);
                if (col.transform.Find("trail2"))
                    col.transform.Find("trail2").gameObject.SetActive(false);
            }
            GameObject.Find("music").audio.Stop();
            if (GameObject.Find("Alarm"))
                GameObject.Find("Alarm").audio.Stop();

            audio.Play();
            if (UseKongRunaway)
            {
                GameObject.Find("Kong_Ladder").GetComponent<Kong_Runaway>().Runaway();
            }
            Invoke("DoWinProcedure", showWinAfter);
            ActionDone = true;

        }

    }

    void DoWinProcedure()
    {
        //GameObject winscreen = Camera.main.transform.FindChild ("YouWin");
        //winscreen.transform. 
        GameManager.instance.NextLevel = GameManager.instance.GetNextLevel();
        PlayerPrefs.SetInt(GameManager.instance.unlockNextLevel(), 1);
        PlayerPrefs.Save();
        GameObject.Find("Pauser").GetComponent<Pauser>().paused = true;
        winScreen.gameObject.SetActive(true);

        GUIS = winScreen.Find("YouWin_Score").GetComponent<Text>();
        GUIS.text = score.score.ToString();
        GUIT = winScreen.Find("YouWin_TotalScore").GetComponent<Text>();
        GUIT.text = GameManager.instance.TotalScore.ToString();
        GameManager.instance.TotalScore += score.score;
        PlayerPrefs.SetFloat("TotalScore", GameManager.instance.TotalScore);
        PlayerPrefs.Save();
        S = score.score;
        T = float.Parse(GUIT.text);
        T2 = GameManager.instance.TotalScore;
        StartScoreAnim = true;
        winScreen.audio.Play();
    }
    void DoScoreAnimation()
    {
        //if (Time.time > lastHitTime + DelayBetweenShootings) {

        //do {
        T += 10;
        S -= 10;

        if (S < 0 || T > T2 || SkipCounting)
        {
            T = T2;
            S = 0f;
        }

        GUIS.text = S.ToString("000000");
        GUIT.text = T.ToString("000000");


        //} while (S > 0);
        if (S <= 0)
        {
            StartScoreAnim = false;
            winScreen.audio.Stop();
        }

    }
    void Update()
    {
        if (StartScoreAnim)
            DoScoreAnimation();
    }
}


