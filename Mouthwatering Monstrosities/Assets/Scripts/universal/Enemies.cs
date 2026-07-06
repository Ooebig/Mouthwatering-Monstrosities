using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemies : MonoBehaviour, IDamage
{
    enum enemyType { goblinoid, hybrid, lizard, undead, abberartion };
    [Header("Components")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;

    [Header("Stats")]
    [SerializeField] float HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int FOV;
    [SerializeField] int roamDist;
    [SerializeField] int roamDelay;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    [SerializeField] enemyType type;

    [Header("Weapon")]
    [SerializeField] float damageRate;
    [SerializeField] Collider weaponCollider;


    Color colorOrig;

    Vector3 playerDir;
    Vector3 startingPos;

    bool playerInTrigger;

    float damageTimer;

    float stoppingDistOrig;

    int IDamage.Team => 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        //gamemanager.instance.updateGameGoal(1);
        startingPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
        agent.SetDestination(gamemanager.instance.player.transform.position);
        agent.stoppingDistance = 1;

        float distance = Vector3.Distance(transform.position, gamemanager.instance.player.transform.position);
        faceTarget();
        if (distance <= attackRange && damageTimer >= damageRate)
        {
            attack();
        }
    }

    void attack()
    {
        damageTimer = 0;
        faceTarget();
        StartCoroutine(MeleeAttack());
    }

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, faceTargetSpeed * Time.deltaTime);
    }

    /* void rotateGun()
     {
         Quaternion rot = Quaternion.LookRotation(playerDir);
         gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, rot, Time.deltaTime * gunRotateSpeed);
     }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            agent.stoppingDistance = 0;
        }
    }


    public void takeDamage(float amount)
    {
        HP -= amount;
        agent.SetDestination(gamemanager.instance.player.transform.position);

        if (HP <= 0)
        {
            //gamemanager.instance.updateGameGoal(-1);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }

    IEnumerator MeleeAttack()
    {
        agent.isStopped = true;
        weaponCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        weaponCollider.enabled = false;
        agent.isStopped = false;
    }
}
