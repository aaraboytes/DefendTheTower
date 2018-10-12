using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public int life;
    public int speed;
    public Transform target;
    Rigidbody rb;
    int currentLife;

    private void Start()
    {
        currentLife = life;
        target = GameManager._instance.GetHumanTarget(target);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //FollowTarget
        Vector3 dir = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(target.position);
        transform.rotation = Quaternion.EulerAngles(transform.rotation.x, lookRot.y, transform.rotation.z);
        rb.velocity = dir * speed * Time.deltaTime;
    }
    public bool MakeDamage(int damage)
    {
        if (currentLife - damage > 0)
        {
            currentLife -= damage;
            return true;
        }
        else
        {
            Invoke("Die", 0.2f);
            return false;
        }
    }
    public void Die()
    {
        GameManager._instance.HumanKilled();
        Destroy(gameObject);
    }
    public void Heal()
    {
        currentLife = life;
    }
}
