using NUnit.Framework;
using UnityEngine;

public class damage : MonoBehaviour
{
    public enum Type { Projectile, Melee }
    [SerializeField] public Type type;
    [SerializeField] public int team;
    [SerializeField] public float damageAmount;

    [SerializeField] public Rigidbody rb;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public int bulletDestroyTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if  (type == Type.Projectile)
        {
            rb.linearVelocity = transform.forward * bulletSpeed;
            Destroy(gameObject, bulletDestroyTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        IDamage dmg = other.GetComponent<IDamage>();
        bool hit = false;

        if (dmg != null && dmg.Team != team)
        {
            dmg.takeDamage(damageAmount);
            hit = true;
        }
        else if (dmg == null)
        {
            hit = true;
        }
        if (hit && type == Type.Projectile && other.gameObject.layer != 3)
        {
            Destroy(gameObject);
        }
    }
} 
