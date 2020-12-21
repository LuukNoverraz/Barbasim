using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameController gameController;
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
        startPopulation = Random.Range(300, 400);
        
        SpawnCreature();
    }

    public void SpawnCreature()
    {
        for (int i = 0; i < startPopulation; i++)
        {
            xPos = Random.Range(0.0f, 450.0f);
            zPos = Random.Range(0.0f, 450.0f);
            
            randomPos = new Vector3(xPos, 25.0f, zPos);

            newCreature = Instantiate(creature, new Vector3(xPos, 30.0f, zPos), Quaternion.identity);

            Ray ray = new Ray(newCreature.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Ground")
                {
                    gameController.totalBirths++;
                    newCreature.transform.position = new Vector3(xPos, hit.point.y + 1.0f, zPos);
                }
                else
                {
                    Destroy(newCreature);
                }
            }
        }       
    }
}
