using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI.Table;

public class playerController : MonoBehaviour, IDamage
{

    [Header("Build")]
    [SerializeField] public CharacterController controller;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject bladeZone;
    [SerializeField] GameObject bluntZone;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform weaponPivot;
    [SerializeField] Renderer weaponModel;
    [SerializeField] public int team = 0;
    public int Team => team;

    [Header("Stats")]
    [SerializeField, Range(5, 50)] public float speed = 10;
    [SerializeField, Range(1.1f, 3f)] public float sprintMod = 1.5f;
    [SerializeField, Range(25f, 250f)] public float HP = 100f;
    int lookSens = 30;

    [Header("Enabled Weapons")]
    [SerializeField] weapon blade;
    [SerializeField] weapon blunt;
    [SerializeField] weapon ranged;
    [SerializeField, Range(0.1f, 10f)] float switchDelay = 1f;
    weapon[] weaponList;
    weapon activeWeapon;
    int activeWeaponNum;
    float attackRate;

    [Header("Buffs/Temporary")]
    public float tempHP = 0;
    public int damageReduction = 0;

    [Header("Internals")]
    public Vector3 moveDir;
    public Vector3 playerVel;
    float attackTimer;
    public float originalHP;
    bool isSprinting = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalHP = HP;
        weaponList = new weapon[] { blade, blunt, ranged };
        for (int i = 0; activeWeapon == null; i++)
        {
            activeWeapon = weaponList[i];
            activeWeaponNum = i;
        }

    }

    // Update is called once per frame
    void Update()
    {
        movement();

        sprint();
    }

    void movement()
    {
        attackTimer += Time.deltaTime;
        //if (moveDir.magnitude > 0.3f && !isPlayingStep)
        //{
        //    StartCoroutine(playStep());
        //}
        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        
        controller.Move(playerVel * Time.deltaTime);


        
        if (Input.GetButton("Fire1") && weaponList.Length > 0 && attackTimer >= weaponList[activeWeaponNum].attackSpeed)
        {
            attack();
        }
        selectWeapon();
    }

    void attack()
    {
        if (activeWeaponNum == 0)
        {
            bladeZone.SetActive(true);
            StartCoroutine(disableAttackZone(bladeZone));
        }
        else if (activeWeaponNum == 1)
        {
            bluntZone.SetActive(true);
            StartCoroutine(disableAttackZone(bluntZone));
        }
        else if (activeWeaponNum == 2)
        {
            GameObject bullet = Instantiate(activeWeapon.projectile, shootPos.position, shootPos.rotation);
            bullet.GetComponent<damage>().damageAmount = activeWeapon.damage;
            bullet.GetComponent<damage>().team = team;
        }
        attackTimer = 0;
    }

    IEnumerator disableAttackZone(GameObject zone)
    {
        yield return new WaitForSeconds(0.1f);
        zone.SetActive(false);
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            speed /= sprintMod;
        }
    }

    void selectWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {

            activeWeaponNum++;
            activeWeapon = null;
            for (int i = activeWeaponNum; activeWeapon == null; i++)
            {
                if (i == 3) { i = 0; }
                activeWeapon = weaponList[i];
                activeWeaponNum = i;

            }
            //weaponModel = activeWeapon.model;
            attackRate = activeWeapon.attackSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            activeWeaponNum--;
            activeWeapon = null;
            for (int i = activeWeaponNum; activeWeapon == null; i--)
            {
                if (i == -1) { i = 2; }
                activeWeapon = weaponList[i];
                activeWeaponNum = i;

            }
            //weaponModel = activeWeapon.model;
            attackRate = activeWeapon.attackSpeed;
        }
    }

    public void takeDamage(float amount)
    {
        if (tempHP != 0)
        {
            float dmg = amount - damageReduction;
            tempHP -= dmg;
            if (tempHP < 0)
            {
                HP += tempHP;
                tempHP = 0;
            }
        }
        else
        {
            float dmg = amount - damageReduction;
            HP -= dmg;
        }
        
        

        if (HP <= 0)
        {
            gamemanager.instance.youLose();
        }
        else
        {
            updatePlayerUI();
            StartCoroutine(flashDamage());
        }
    }

    public void updatePlayerUI()
    {
        gamemanager.instance.playerHPBar.fillAmount = (float)HP / originalHP;
    }

    IEnumerator flashDamage()
    {
        gamemanager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gamemanager.instance.playerDamageFlash.SetActive(false);
    }

}
