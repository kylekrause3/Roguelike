using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    /* Raycasting */
    RaycastHit hitInfo;

    float damage;
    float range;
    float fireRate; //bullets per second

    private float nextFireTime = 0f;

    public Gun(float damage = 10f, float range = Mathf.Infinity, float fireRate = 1f)
    {
        this.damage = damage;
        this.range = range;
        this.fireRate = fireRate;
    }

    public void shoot(Transform origin)
    {
        if (nextFireTime <= Time.time)
        {
            origin.position += origin.transform.forward * VirtualCamera.getCamDistance();
            nextFireTime = Time.time + (1f / fireRate);
            if (Physics.Raycast(origin.position, origin.transform.forward, out hitInfo, range))
            {
                Enemy enemyHit = hitInfo.transform.GetComponent<Enemy>();
                Debug.DrawLine(origin.position, hitInfo.point, Color.red, .5f); //this is weird, espeically with large objects, because the hitinfo position is the center of the object hit, not the impact point.
                if (enemyHit != null)
                {
                    enemyHit.TakeDamage(damage);
                }
            }
        }
    }
}



//HERE LIES UNNECESSARY CALCULUS 3 WORK:
    //calculation using r = 1, theta = origin.rotation.eulerAngles.y, and phi = origin.rotation.eulerAngles.x
        /*float q = Mathf.Cos(origin.rotation.eulerAngles.y * Mathf.Deg2Rad);
        direction.x = Mathf.Cos(origin.rotation.eulerAngles.x * Mathf.Deg2Rad) * q;
        direction.y = Mathf.Sin(origin.rotation.eulerAngles.x * Mathf.Deg2Rad) * q;
        direction.z = Mathf.Sin(origin.rotation.eulerAngles.y * Mathf.Deg2Rad);*/
        //direction is now a vector representation of polar origin.rotation
