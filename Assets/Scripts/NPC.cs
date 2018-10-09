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
        target = GameManager._instance.GetHumanTarget();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //FollowTarget
        Vector3 dir = target.position - transform.position;
        transform.LookAt(target);
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
            Die();
            return false;
        }
    }
    public void Die()
    {
        GameManager._instance.aliveHumans--;
        Destroy(gameObject);
    }
}
