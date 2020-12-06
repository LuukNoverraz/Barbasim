using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideAversion : MonoBehaviour
{
    public CreatureController creatureController;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Water")
        {
            // 2% chance to still be suicidal

            if (Random.Range(0, 100) < 98)
            {
                creatureController.movementAngle *= -1;
            }
        }
    }
}
