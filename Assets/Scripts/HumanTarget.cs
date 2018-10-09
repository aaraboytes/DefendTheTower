using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTarget : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Human") || other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Human"))
            {
                NPC human = other.GetComponent<NPC>();
                human.target = GameManager._instance.GetHumanTarget(human.target);
            }
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.target = GameManager._instance.GetHumanTarget(enemy.target);
            }
        }
    }
}
