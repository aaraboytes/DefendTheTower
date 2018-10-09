using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour {
    public float distance;
    public GameObject shootEffect,bloodEffect;
    [Header("Gameplay Properties")]
    public int damage;
    public float cadence;
    public int ammo;
    public float timeForReload;
    public float horSpeed;
    public float vertSpeed;
    public float moveSpeed;

    float timer;
    int currentAmmo;
    bool reloading = false;
    float h = 0, v = 0;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAmmo = ammo;   
    }
    private void Update()
    {
        timer += Time.deltaTime;
        //Shoot
        if (Input.GetMouseButton(0))
        {
            if (timer > cadence && !reloading)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
                {
                    Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        Enemy currentEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                        currentEnemy.MakeDamage(damage);
                        Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(transform.position));
                    }else if (hit.collider.CompareTag("Human")) {
                        GameManager._instance.ShopPanel.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                    }
                    else
                        Instantiate(shootEffect, hit.point, Quaternion.LookRotation(transform.position));
                }
                timer = 0;
                currentAmmo--;

                //Reload
                if (currentAmmo == 0)
                {
                    currentAmmo = ammo; //Relleno
                    reloading = true;
                }
            }
        }
        //Waiting for reload
        if (reloading)
        {
            if (timer > timeForReload)
                reloading = false;
        }
        h += horSpeed * Input.GetAxis("Mouse X");
        v -= vertSpeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(v, h, 0);

        //Moving in 
        if (Input.GetAxis("Vertical")!=0)
        {
            transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * moveSpeed * Input.GetAxis("Horizontal"));
        }
    }
}
