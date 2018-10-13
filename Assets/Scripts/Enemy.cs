using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour {
    public int life;
    public float speed;
    public int damage;
    public int score;
    public Transform target;
    public int indexTarget;
    int currentLife;
    Rigidbody rb;
    Vector3 move;
	void Start () {
        currentLife = life;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        //FollowTarget
        Vector3 dir = target.position - transform.position; //Direction to go
        Quaternion lookRot = Quaternion.LookRotation(dir);  //Direction to look
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, lookRot.eulerAngles.y, 0), Time.deltaTime * 10.0f);
        //Move
        float yStore = move.y;
        move = transform.forward * speed;
        move.y = yStore;
        rb.velocity = move;
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
                target = GameManager._instance.humanTargets[0];
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
        if(GameManager._instance.targets[point]!=null)
            target = GameManager._instance.targets[point];
    }
    public void Die()
    {
        GameManager._instance.EnemyKilled(score);
        LevelManager._instance.EnemyKilled();
        Destroy(gameObject);
    }
}
