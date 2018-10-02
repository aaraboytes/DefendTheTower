using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour {
    public int life;
    public float speed;
    public Transform target;
    NavMeshAgent agent;

    int currentLife;
    bool alive;

	void Start () {
        currentLife = life;
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        agent.SetDestination(target.position);
	}
    public void MakeDamage(int damage)
    {
        if (currentLife - damage > 0)
            currentLife -= damage;
        else
            Die();
    }
    public void Die()
    {
        alive = false;
        Destroy(gameObject);
    }
}
