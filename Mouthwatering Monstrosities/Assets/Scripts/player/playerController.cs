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
    [SerializeField] Transform shootPos;
    [SerializeField] Transform weaponPivot;
    [SerializeField] Renderer weaponModel;
    [SerializeField] public int team = 0;
    public int Team => team;

    [Header("Stats")]
    [SerializeField, Range(5, 50)] public float speed = 10;
    [SerializeField, Range(1.1f, 3f)] public float sprintMod = 1.5f;
    [SerializeField, Range(25f, 250f)] public float HP = 100f;
    [SerializeField, Range(0, 3)] public int jumpMax = 1;
    [SerializeField, Range(10f, 50f)] public float jumpSpeed = 30f;
    [SerializeField, Range(10f, 50f)] public float gravity = 30f;
    int lookSens = 30;

    [Header("Enabled Weapons")]
    [SerializeField] weapon blade;
    [SerializeField] weapon blunt;
    [SerializeField] weapon ranged;
    [SerializeField, Range(0.1f, 10f)] float switchDelay = 1f;
    public weapon[] weaponList;
    weapon activeWeapon;
    int activeWeaponNum;
    float attackRate;

    [Header("Audio")]
    [SerializeField] AudioSource audPlayer;
    [SerializeField] AudioClip[] audJump;
    [Range(0, 1)][SerializeField] float audJumpVol;
    [SerializeField] AudioClip[] audHurt;
    [Range(0, 1)][SerializeField] float audHurtVol;
    [SerializeField] AudioClip[] audStep;
    [Range(0, 1)][SerializeField] float audStepVol;

    [Header("Buffs/Temporary")]
    public float tempHP = 0;
    public int damageReduction = 0;

    [Header("Internals")]
    public Vector3 moveDir;
    public Vector3 playerVel;
    float attackTimer;
    public float originalHP;
    bool isSprinting = false;
    int jumpCount;
    public int activeWebs = 0;
    public int maxWebs = 3;
    public bool isStunned;


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
        isStunned = false;
        changeWeaponModel();
    }

    // Update is called once per frame
    void Update()
    {
       if (!isStunned)
        {
            movement();
            sprint();
        }
    }

    void movement()
    {
        attackTimer += Time.deltaTime;

        if (controller.isGrounded)
        {
            jumpCount = 0;
            playerVel.y = 0;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        jump();
        controller.Move(playerVel * Time.deltaTime);

        playerVel.y -= gravity * Time.deltaTime;

        if (Input.GetButton("Fire1") && weaponList.Length > 0 && attackTimer >= weaponList[activeWeaponNum].attackSpeed && gamemanager.instance.isPaused == false)
        {
            Debug.Log("Attack Pressed");
            attack();
        }
        selectWeapon();
    }

    void attack()
    {

        if (activeWeaponNum == 2)
        {
            GameObject bullet = Instantiate(activeWeapon.projectile, shootPos.position, shootPos.rotation);
            bullet.GetComponent<damage>().damageAmount = activeWeapon.damage;
            bullet.GetComponent<damage>().team = team;
        }
        else
        {
            GameObject hitbox = Instantiate(activeWeapon.projectile, shootPos.position, transform.rotation);
            hitbox.GetComponent<damage>().damageAmount = activeWeapon.damage;
            Vector3 scale = hitbox.transform.localScale;
            scale.x *= activeWeapon.range;
            scale.z *= activeWeapon.range;
            hitbox.transform.localScale = scale;
            hitbox.GetComponent<damage>().team = team;
            hitbox.GetComponent<damage>().destroyobject(hitbox.GetComponent<damage>().bulletDestroyTime);
        }
        attackTimer = 0;
    }

    

    void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
        }
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
            changeWeaponModel();
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
            changeWeaponModel();
            attackRate = activeWeapon.attackSpeed;
        }
    }

    void changeWeaponModel()
    {
        MeshFilter sourceFilter = activeWeapon.model.GetComponent<MeshFilter>();
        MeshRenderer sourceRenderer = activeWeapon.model.GetComponent<MeshRenderer>();
        MeshFilter targetFilter = weaponModel.GetComponent<MeshFilter>();
        MeshRenderer targetRenderer = weaponModel.GetComponent<MeshRenderer>();
        targetFilter.sharedMesh = sourceFilter.sharedMesh;
        targetRenderer.sharedMaterials = sourceRenderer.sharedMaterials;
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
            updatePlayerUI();
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

    public void changePlayerPos()
    {
        controller.transform.position = gamemanager.instance.playerSpawnPos.transform.position;
        Physics.SyncTransforms();
    }

    public IEnumerator stun()
    {
        isStunned = true;
        yield return new WaitForSeconds(1f);
        isStunned = false;
    }

}

