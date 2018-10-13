using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour {
    public Transform target;
    public float speed;
    public float maxDistance;
    Rigidbody rb;
    Vector3 move;
    Vector3 origin;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, transform.forward * maxDistance);
    }
    private void FixedUpdate()
    {
        //Check if there is a wall
        Collider col = GetComponent<Collider>();
        origin = new Vector3(col.bounds.extents.z, col.bounds.extents.y / 2, transform.position.z);
        Ray ray = new Ray(transform.position,transform.forward * maxDistance);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit)){
            Debug.Log("Colliding");
            rb.useGravity = false;
            rb.AddForce(speed * Vector3.up);
        }
        else
        {
            rb.useGravity = true;
        }
        //FollowTarget
        Vector3 dir = target.position - transform.position; //Direction to go
        Quaternion lookRot = Quaternion.LookRotation(dir);  //Direction to look
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRot, Time.deltaTime * 10.0f);
        //Move
        float yStore = move.y;
        move = transform.forward * speed;
        move.y = yStore;
        rb.velocity = move;
    }
}
