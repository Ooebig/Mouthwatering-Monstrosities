using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public enum  levelType { kitchen, dungeon }
    public static gamemanager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuClear;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuSkillTree;
    [SerializeField] TMP_Text timeLimit;
    [SerializeField] float remainingTime;
    [SerializeField] Image roomProgressBar;
    [SerializeField] int roomGoalMax;
    public int roomGoalCount;
    
    public Image playerHPBar;
    public GameObject playerDamageFlash;

    public bool isPaused;
    public GameObject player;
    public playerController playerScript;

    float timeScaleOrig;

    int gameGoalCount;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        Cursor.visible = false;
        if (player != null)
        {
            playerScript = player.GetComponent<playerController>();
        }
        else return;
    }

    void Update()
    {
        countdownTimer();

        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuActive.SetActive(true);
            }

            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuSkillTree;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuSkillTree)
            {
                stateUnpause();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void statePause()
    {
        isPaused = true;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    public void stateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        menuActive.SetActive(false);
        menuActive = null;
    }

    public void updateGameGoal(int amount)
    {
        roomGoalCount += amount;
        roomProgressBar.fillAmount = (float)roomGoalCount / roomGoalMax;

        if (gameGoalCount >= roomGoalMax)
        {
            // Hey you beat the level
            statePause();
            menuActive = menuClear;
            menuActive.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void youLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }

    public void countdownTimer()
    {
        if (remainingTime > 0) remainingTime -= Time.deltaTime;
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            statePause();
            menuActive = menuLose;
            menuActive.SetActive(true);

        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timeLimit.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
