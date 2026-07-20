using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour
{
    //Enums
    public enum diffucltySetting { Easy, Normal, Hard, Expert }
    public enum  levelType { kitchen, dungeon }
    public static gamemanager instance;

    [Header("UI Elements")]
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuClear;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuSkillTree;
    [SerializeField] GameObject inventoryCooking;
    [SerializeField] GameObject recipeCooking;
    [SerializeField] GameObject playerInv;
    public GameObject playerDamageFlash;
    public GameObject playerFallingFlash;    

    [Header("HUD Elements")]
    [SerializeField] TMP_Text timeLimit;
    [SerializeField] Image roomProgressBar;
    [SerializeField] Image bladeWeaponIcon;
    [SerializeField] Image bluntWeaponIcon;
    [SerializeField] Image rangedWeaponIcon;
    public Image playerHPBar;


    [Header("Data Objects")]

    [Header("Settings")]
    public diffucltySetting currentDifficulty = diffucltySetting.Normal;
    [SerializeField] float remainingTime;
    [SerializeField] int roomGoalMax;
    
    

    [Header("Automatic, Do Not Touch")]
    public int roomGoalCount;
    
    
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
        else if (Input.GetKeyDown(KeyCode.C)) {
            if (menuActive == null)
            {
                statePause();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menuActive = inventoryCooking;
                menuActive.SetActive(true);
            }
            else if (menuActive == inventoryCooking || recipeCooking)
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

    public void cookSwitch() {
        if (menuActive == recipeCooking)
        {
            menuActive.SetActive(false);
            menuActive = inventoryCooking;
            menuActive.SetActive(true);
        }
        else if (menuActive == inventoryCooking)
        {
            menuActive.SetActive(false);
            menuActive = recipeCooking;
            menuActive.SetActive(true);
        }
    
    }

    public void updateWeaponIcons()
    {

        Sprite blade = playerScript.weaponList[0].icon;
        Sprite blunt = playerScript.weaponList[1].icon;
        Sprite ranged = playerScript.weaponList[2].icon;

        if (blade != null)
        {
            bladeWeaponIcon.enabled = true;
            bladeWeaponIcon.sprite = blade;
        }
        else
        {
            bladeWeaponIcon.enabled = false;
        }

        if (blunt != null)
        {
            bluntWeaponIcon.enabled = true;
            bluntWeaponIcon.sprite = blunt;
        }
        else
        {
            bluntWeaponIcon.enabled = false;
        }

        if (ranged != null)
        {
            rangedWeaponIcon.enabled = true;
            rangedWeaponIcon.sprite = ranged;
        }
        else
        {
            rangedWeaponIcon.enabled = false;
        }

    }

}
