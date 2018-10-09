using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour {
    public int life;
    public float speed;
    public int damage;
    public Transform target;
    public int indexTarget;
    int currentLife;
    Rigidbody rb;

	void Start () {
        currentLife = life;
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        //FollowTarget
        Vector3 dir = target.position - transform.position;
        transform.LookAt(target);
        rb.velocity = dir * speed * Time.deltaTime;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Human"))
        {
            NPC human = collision.collider.GetComponent<NPC>();
            if (human.MakeDamage(damage))//If keeps alive
            {
                target = collision.transform;
            }
            else
            {
                target = GameManager._instance.GetHumanTarget();
            }
        }
    }

    public void MakeDamage(int damage)
    {
        if (currentLife - damage > 0)
            currentLife -= damage;
        else
            Die();
    }
    public void FindPath(int point)
    {
        if (GameManager._instance.targets[point] != null)
        {
            target = GameManager._instance.targets[point];
        }
    }
    public void Die()
    {
        GameManager._instance.enemiesOnScene--;
        Destroy(gameObject);
    }
}
