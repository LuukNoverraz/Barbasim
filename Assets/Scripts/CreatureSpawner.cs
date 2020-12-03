using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creature;
    
    private int startPopulation;
    private float xPos;
    private float yPos;
    private float zPos;
    private Vector3 randomPos;
    public RaycastHit hit;
    private GameObject newCreature;

    void Start()
    {
        startPopulation = Random.Range(400, 500);

        for (int i = 0; i < startPopulation; i++) 
        {
            xPos = Random.Range(0.0f, 450.0f);
            zPos = Random.Range(0.0f, 450.0f);
            
            randomPos = new Vector3(xPos, 25.0f, zPos);

            newCreature = Instantiate(creature, new Vector3(xPos, 30.0f, zPos), Quaternion.identity);

            // Ray ray = new Ray(newCreature.transform.position, Vector3.down);
            // RaycastHit hit;

            // if (Physics.Raycast(ray, out hit))
            // {
            //     Debug.Log("MAYBE");
            //     newCreature.transform.position = new Vector3(xPos, hit.point.y + 2.0f, zPos);
            // }
        }
    }
}
