 using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class damage : MonoBehaviour
{
    public enum Type { Projectile, DOT }

    [SerializeField] public Type type;
    [SerializeField] public bool autoDelete = true;
    [SerializeField] public int team;
    [SerializeField] public float damageAmount;
    [SerializeField] float damageRate;

    [SerializeField] public Rigidbody rb;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletDestroyTime;

    bool isDamaging;

    void Start()
    {
        if (type == Type.Projectile)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
            if (autoDelete)
            {
                Destroy(gameObject, bulletDestroyTime);
            }

        }
    }

    public void destroyobject(float time)
    {
        Destroy(gameObject, time);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.isTrigger) return;

        IDamage dmg = other.GetComponent<IDamage>();
        bool hit = false;
        if (dmg != null && dmg.Team != team)
        {
            if (team == 1)
            { dmg.takeDamage(damageAmount * gamemanager.instance.GetDifficultyMult()); }
            else
            {
              dmg.takeDamage(damageAmount);
            }
            hit = true;
        }
        else if (dmg == null)
        {
            hit = true;
        }
        else if (dmg.Team != team)
        {
            hit = true;

            if (type != Type.DOT)
            {
                dmg.takeDamage(damageAmount);
            }
        }

        if (hit && type == Type.Projectile)
        {
            Destroy(gameObject);
        }
    }

    

    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger) return;

        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null && dmg.Team != team && type == Type.DOT && !isDamaging)
        {
            StartCoroutine(damageOther(dmg));
        }
    }

    IEnumerator damageOther(IDamage d)
    {
        isDamaging = true;
        d.takeDamage(damageAmount);
        yield return new WaitForSeconds(damageRate);
        isDamaging = false;
    }
}