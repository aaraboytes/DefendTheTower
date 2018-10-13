using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour {
    public float speed;
    public Transform target;
    Rigidbody rb;
    Vector3 move;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //FollowTarget
        Vector3 dir = target.position - transform.position; //Direction to go
        Quaternion lookRot = Quaternion.LookRotation(dir);  //Direction to look
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,lookRot.eulerAngles.y,0), Time.deltaTime * 10.0f);
        //Move
        float yStore = move.y;
        move = transform.forward * speed;
        move.y = yStore;
        rb.velocity = move;
    }
}
