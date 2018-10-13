using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public int life;
    public int speed;
    public Transform target;
    Rigidbody rb;
    int currentLife;
    bool alive = true;
    Animator anim;
    Vector3 move = new Vector3();

    private void Start()
    {
        currentLife = life;
        target = GameManager._instance.GetHumanTarget(target);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (alive)
        {
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
        if (alive)
        {
            GameManager._instance.HumanKilled();
            anim.SetTrigger("die");
            alive = false;
            tag = "DeathHuman";
            gameObject.layer = 9;
        }
    }
    public void Heal()
    {
        currentLife = life;
    } 
}
