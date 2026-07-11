using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Timeline;
using UnityEditor.Build;
using Unity.Mathematics;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;

public class Enemies : MonoBehaviour, IDamage
{
    enum enemyType {goblinoid, hybrid, lizard, undead, abberartion };
    public enum enemyTier { standard, boss, final }
    [Header("Components")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform weaponTrans;

    [Header("Stats")]
    [SerializeField] float HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int FOV;
    [SerializeField] int roamDist;
    [SerializeField] int roamDelay;
    [SerializeField] float attackRange;
    [SerializeField] float attackDuration;
    [SerializeField] enemyType type;
    [SerializeField] enemyTier enmyTier;

    [Header("Weapon")]
    [SerializeField] float damageRate;
    [SerializeField] Collider weaponCollider;
    [SerializeField] float swingSpeed;


    float MaxHp;
    Color colorOrig;

    Vector3 playerDir;
    Vector3 startingPos;

    float damageTimer;
    Quaternion startRotation;

    Spawner enemySpawner;

    int IDamage.Team => 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHp = HP * gamemanager.instance.GetDifficultyMult();
        HP = MaxHp;
        colorOrig = model.material.color;
        startingPos = transform.position;
        enemySpawner = FindAnyObjectByType<Spawner>();
        startRotation = weaponTrans.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer += Time.deltaTime;
        agent.SetDestination(gamemanager.instance.player.transform.position);
        agent.stoppingDistance = 2;

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

        switch (enmyTier)
        {
            case enemyTier.standard:
                StartCoroutine(BasicAttack());
                break;

            case enemyTier.boss:

                if(UnityEngine.Random.value < 0.7)
                {
                    StartCoroutine(BasicAttack());
                }
                else
                {
                    StartCoroutine(SpecialAttack());
                }
                

                break;

            case enemyTier.final:

                break;


            default:
                break;
        }
        
    }

  

    void faceTarget()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, faceTargetSpeed * Time.deltaTime);
    }


    public void takeDamage(float amount)
    {
        HP -= amount;
        agent.SetDestination(gamemanager.instance.player.transform.position);

        if (HP <= 0)
        {
            if (enemySpawner != null)
            {
                enemySpawner.EnemyDied(gameObject);
            }
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

    IEnumerator BasicAttack()
    {
        agent.isStopped = true;

        Quaternion windup = startRotation * Quaternion.Euler(-60, 0, 0);
        Quaternion swing = startRotation * Quaternion.Euler(80, 0, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            weaponTrans.localRotation = Quaternion.Slerp(startRotation, windup, t);
            yield return null;
        }

        weaponCollider.enabled = true;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed * 2;
            weaponTrans.localRotation = Quaternion.Slerp(windup, swing, t);
            yield return null;
        }

        weaponCollider.enabled = false;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            weaponTrans.localRotation = Quaternion.Slerp(swing, startRotation, t);
            yield return null;
        }

        weaponTrans.localRotation = startRotation;
        agent.isStopped = false;
    }

    IEnumerator SpecialAttack()
    {
        agent.isStopped = true;
        weaponCollider.enabled = true;

        Quaternion windup = startRotation * Quaternion.Euler(0, -60, 0);
        Quaternion swing = startRotation * Quaternion.Euler(0, 80, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            weaponTrans.localRotation = Quaternion.Slerp(startRotation, windup, t);
            yield return null;
        }

        weaponCollider.enabled = true;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed * 2;
            weaponTrans.localRotation = Quaternion.Slerp(windup, swing, t);
            yield return null;
        }

        weaponCollider.enabled = false;

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            weaponTrans.localRotation = Quaternion.Slerp(swing, startRotation, t);
            yield return null;
        }

        gamemanager.instance.playerScript.StartCoroutine(gamemanager.instance.playerScript.stun());

        agent.isStopped = false;
    }

}
