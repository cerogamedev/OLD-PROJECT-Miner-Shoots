using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSC : MonoBehaviour
{
    private Transform bugTarget;
    Vector2 bugDirection;

    public float Range;
    bool Detected = false;
    public GameObject gun;
    public GameObject Bullet;
    public float FireRate;
    float nextTimeToFire = 0;
    public Transform shootPoint;
    public float Force;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (bugTarget == null)
            return;
        else
        {
            Vector2 bugsPos = bugTarget.position;
            bugDirection = bugsPos - (Vector2)transform.position;
            RaycastHit2D rayInfo1 = Physics2D.Raycast(transform.position, bugDirection, Range);
            if (rayInfo1)
                if (rayInfo1.collider.gameObject.tag == "Rock")
                {
                    if (Detected == false)
                    {
                        Detected = true;

                    }
                    else
                    {
                        if (Detected == true)
                        {
                            Detected = false;

                        }
                    }
                    if (Detected)
                    {
                        gun.transform.up = bugDirection;
                        if (Time.time > nextTimeToFire)
                        {
                            nextTimeToFire = Time.time + 1 / FireRate;
                            Shoot1();
                        }
                    }
                }
        }

    }

    void Shoot1()
    {
        GameObject BulletIns = Instantiate(Bullet, shootPoint.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(bugDirection * Force);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Rock");
        float shortestDistance = Mathf.Infinity;
        GameObject nearnestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearnestEnemy = enemy;
            }
        }
        if (nearnestEnemy != null && shortestDistance <= Range)
        {
            bugTarget = nearnestEnemy.transform;
        }
    }
}