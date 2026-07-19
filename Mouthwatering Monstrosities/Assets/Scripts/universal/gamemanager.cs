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
    [SerializeField] GameObject menuCredits;
    [SerializeField] GameObject menuStorage;
    [SerializeField] GameObject menuCrafting;
    [SerializeField] GameObject playerInv;
    [SerializeField] TMP_Text timeLimit;
    [SerializeField] float remainingTime;
    [SerializeField] Image roomProgressBar;
    [SerializeField] int roomGoalMax;
    public int roomGoalCount;
    public enum diffucltySetting {Easy, Normal, Hard, Expert}
    public diffucltySetting currentDifficulty = diffucltySetting.Normal;

    public Image playerHPBar;
    public GameObject playerDamageFlash;
    public GameObject playerFallingFlash;
    public bool isPaused;
    public GameObject player;
    public playerController playerScript;
    public GameObject playerSpawnPos;

    float timeScaleOrig;

    int gameGoalCount;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
        player = GameObject.FindWithTag("Player");
        playerSpawnPos = GameObject.FindWithTag("Player Spawn Pos");
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

        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.P))
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
        //else if (Input.GetKeyDown(KeyCode.C)) {
        //    if (menuActive == null)
        //    {
        //        statePause();
        //        Cursor.visible = true;
        //        Cursor.lockState = CursorLockMode.None;
        //        menuActive = inventoryCooking;
        //        menuActive.SetActive(true);
        //    }
        //    else if (menuActive == inventoryCooking || recipeCooking)
        //    {
        //        stateUnpause();
        //        Cursor.visible = false;
        //        Cursor.lockState = CursorLockMode.Locked;
        //    }
        //}

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

    public float GetDifficultyMult()
    {
        switch(currentDifficulty)
        {
            case diffucltySetting.Easy: return 0.5f;
            case diffucltySetting.Normal: return 1.0f;
            case diffucltySetting.Hard: return 1.5f;
            case diffucltySetting.Expert: return 2.5f;
            default: return 1.0f;
        }
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

    public void OpenCredits()
    {
        menuActive.SetActive(false);
        menuActive = menuCredits;
        menuActive.SetActive(true);
    }

    public void BackToMenu() {
        menuActive.SetActive(false);
        menuActive = menuPause;
        menuActive.SetActive(true);
    }

    public void OpenStorageMenu() {
        statePause();
        menuActive = menuStorage;
        menuActive.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseStorageMenu() {

        stateUnpause();
        menuActive = null;
        menuActive.SetActive(false);
    }

    public void OpenCraftingMenu()
    {
        statePause();
        menuActive = menuCrafting;
        menuActive.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseCraftingMenu()
    {

        stateUnpause();
        menuActive = null;
        menuActive.SetActive(false);
    }

}
