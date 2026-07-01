using UnityEngine;

public class damage : MonoBehaviour
{
    enum Type { Projectile, Melee }
    [SerializeField] Type type;
    [SerializeField] int team;
    [SerializeField] float damageAmount;

    [SerializeField] Rigidbody rb;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDestroyTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.forward * bulletSpeed;
        //Destroy(gameObject, bulletDestroyTime);
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
        if (hit && type == Type.Projectile)
        {
            Destroy(gameObject);
        }
    }
} 
