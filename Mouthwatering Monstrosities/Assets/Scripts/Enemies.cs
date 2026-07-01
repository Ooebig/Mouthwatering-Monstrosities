using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemies : MonoBehaviour , IDamage
{
    enum enemyType {goblinoid, hybrid, lizard, undead, abberartion};
    [Header("Components")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;

    [Header("Stats")]
    [SerializeField] float HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int FOV;
    [SerializeField] int roamDist;
    [SerializeField] int roamDelay;
    [SerializeField] enemyType type;

    [Header("Weapon")]
    [SerializeField] float damageRate;
   

    Color colorOrig;

    Vector3 playerDir;
    Vector3 startingPos;

    bool playerInTrigger;

    float damageTimer;

    float angleToPlayer;
    float roamTimer;
    float stoppingDistOrig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        //gamemanager.instance.updateGameGoal(1);
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTrigger && !canSeePlayer())
        {
            checkRoam();

        }
        else if (!playerInTrigger)
        {
            checkRoam();
        }

    }

    void checkRoam()
    {
        if (agent.remainingDistance < 0.1f)
        {
            roamTimer += Time.deltaTime;
            if (roamTimer > roamDelay)
            {
                roam();
            }
        }
    }

    void roam()
    {
        roamTimer = 0;
        agent.stoppingDistance = 0;

        Vector3 ranPos = Random.insideUnitSphere * roamDist;

        ranPos += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);
        agent.SetDestination(hit.position);
    }

    bool canSeePlayer()
    {
        damageTimer += Time.deltaTime;

        //playerDir = gamemanager.instance.player.transform.position - transform.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);

        Debug.DrawRay(transform.position, playerDir);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer < FOV)
            {
                //agent.SetDestination(gamemanager.instance.player.transform.position);
                faceTarget();
                //rotateGun();

                if (damageTimer > damageRate)
                {
                    attack();
                }
                agent.stoppingDistance = stoppingDistOrig;
                return true;
            }
        }
        agent.stoppingDistance = 0;

        return false;

    }

    void attack()
    {
        damageTimer = 0;
        //Instantiate(bullet, shootPos.position, gunPivot.rotation);
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
        //agent.SetDestination(gamemanager.instance.player.transform.position);

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
}
