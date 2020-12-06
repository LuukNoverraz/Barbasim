﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameController gameController;
    public bool firstGeneration = true;
    public string father = "???";
    public string mother = "???";

    // Variable options

    public string[] namePresets1 = {"A", "Be", "Ca", "Do", "E", "Fe", "Fo", "Gi", "Ha", "Ho", "I", "Jo", "Ka", "Ko", "Ki", "La", "Lu", "Ma", "Mi", "Mo", 
                                    "Na", "No", "O", "Pa", "Po", "Q'", "Ro", "Sa", "To", "U", "Vo", "We", "Xy", "Y", "Zo"};
    public string[] namePresets2 = {"ba", "be", "ca", "da", "do", "gi", "ho", "ka", "ko", "la", "l", "me", "no", "pe", "ra", "ro", "s", "sa", "so", "ta", "to",
                                    "uk", "va", "vo", "wa", "wo", "xo", "za", "zo"};
    public string[] namePresets3 = {"ban", "chem", "cus", "das", "de", "ix", "lon", "m", "n", "s", "son", "trid", "kan"};
    
    public TextMesh nameTitle;

    public GameObject[] creatures;
    public Material[] creatureMaterials;

    public string creatureName;
    public Material[] creatureColors;

    // Chosen variables

    public Material chosenColor;
    public float chosenScale;
    public int chosenStrength;
    public int chosenFriendliness;
    public int chosenSpeed;

    // Movement
    
    private float timeBetweenActions;

    public float movementAngle;
    private Vector3 currentAngle;

    private float movementSpeed = 0.5f;
    private float movementVariation = 0.2f;

    // Interaction

    private float chanceOfNothing;
    private float chanceOfKnex;
    private float chanceOfFight;

    // Knexing
    private GameObject newChild;

    // Particles
    public ParticleSystem knexParticles;
    public ParticleSystem fightParticles;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        // Chosen name

        creatureName = namePresets1[Random.Range(0, namePresets1.Length)];
        if (Random.Range(0, 100) < 80)
        {
            creatureName += namePresets2[Random.Range(0, namePresets2.Length)];
        }
        if (Random.Range(0, 100) < 60)
        {
            creatureName += namePresets3[Random.Range(0, namePresets3.Length)];
        }

        // Chosen variables

        if (firstGeneration)
        {
            chosenColor = creatureColors[Random.Range(0, 8)];
            chosenScale = Random.Range(30, 100);
            chosenStrength = Random.Range(0, 100);
            chosenFriendliness = Random.Range(0, 100);
            chosenSpeed = Random.Range(2, 100);
        }

        gameObject.GetComponent<Renderer>().material = chosenColor;
        gameObject.transform.localScale = new Vector3(transform.localScale.x, chosenScale, transform.localScale.z);
        movementSpeed = chosenSpeed / 25;

        nameTitle.text = creatureName;

        // Start movement

        timeBetweenActions = Random.Range(0.5f, 4.0f);
        currentAngle = transform.eulerAngles;
        InvokeRepeating("MoveCreature", timeBetweenActions, timeBetweenActions);

        // Birth message

        if (!firstGeneration)
        {
            // Debug.Log(father + " and " + mother + " just made " + creatureName);
        }
    }

    public void MoveCreature()
    {
        timeBetweenActions = Random.Range(4.0f, 10.0f);
        movementAngle = Random.Range(0.0f, 360.0f);
        transform.Rotate(0.0f, movementAngle, 0.0f);
        movementVariation = Random.Range(0.0f, 0.2f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Water")
        {
            Destroy(gameObject.transform.parent.gameObject);
            // gameController.totalDeaths++;
        }

        if (col.tag == "CreatureArea")
        {
            chanceOfNothing = Random.Range(0, 100);
            chanceOfKnex = chosenFriendliness + Random.Range(0, 50);
            chanceOfFight = 100 - chosenFriendliness + Random.Range(-50, 0);

            int mainChance = Random.Range(0, 100);

            if (chanceOfNothing < 10)
            {   
                if ((mainChance < 66) && mainChance > 33 && chanceOfKnex > 33)
                {
                    // Apply particles

                    knexParticles.Play();
                    col.GetComponentInParent<CreatureController>().knexParticles.Play();

                    // Instantiate child

                    newChild = Instantiate(gameController.child, new Vector3((col.transform.position.x + transform.position.x) / 2,
                    30.0f,
                    (col.transform.position.z + transform.position.z) / 2
                    ), Quaternion.identity) as GameObject;

                    // Add father or mother attributes
                    newChild.GetComponentInChildren<CreatureController>().firstGeneration = false;
                    newChild.GetComponentInChildren<CreatureController>().chosenColor = chosenColor;
                    newChild.GetComponentInChildren<CreatureController>().chosenScale = col.GetComponentInParent<CreatureController>().chosenScale;

                    // < 50 = mother component, > 50 = father component

                    // Strength

                    if (Random.Range(0, 100) < 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenStrength = chosenStrength;
                    }
                    else if (Random.Range(0, 100) > 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenStrength = col.GetComponentInParent<CreatureController>().chosenStrength;
                    }

                    // Friendliness

                    if (Random.Range(0, 100) < 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenFriendliness = chosenFriendliness;
                    }
                    else if (Random.Range(0, 100) > 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenFriendliness = col.GetComponentInParent<CreatureController>().chosenFriendliness;
                    }

                    // Speed

                    if (Random.Range(0, 100) < 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenSpeed = chosenSpeed;
                    }
                    else if (Random.Range(0, 100) > 50)
                    {
                        newChild.GetComponentInChildren<CreatureController>().chosenSpeed = col.GetComponentInParent<CreatureController>().chosenSpeed;
                    }

                    // Father / mother variables

                    newChild.GetComponentInChildren<CreatureController>().father = creatureName;
                    newChild.GetComponentInChildren<CreatureController>().mother = col.GetComponentInParent<CreatureController>().creatureName;

                    gameController.totalBirths++;
                }

                if ((mainChance < 33) && mainChance > 0 && chanceOfFight > 33)
                {
                    // Apply particles
                    fightParticles.Play();
                    col.GetComponentInParent<CreatureController>().fightParticles.Play();

                    if (chosenStrength > col.GetComponentInParent<CreatureController>().chosenStrength)
                    {
                        Destroy(col.GetComponentInParent<CreatureController>().gameObject.transform.parent.gameObject);
                        // Debug.Log(creatureName + " just killed " + col.GetComponentInParent<CreatureController>().creatureName);
                    }

                    if (col.GetComponentInParent<CreatureController>().chosenStrength > chosenStrength)
                    {
                        Destroy(gameObject.transform.parent.gameObject);
                        // Debug.Log(col.GetComponentInParent<CreatureController>().creatureName + " just killed " + creatureName);
                    }
                    
                    gameController.totalDeaths++;
                } 
            }
        }
    }

    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * (movementSpeed + movementVariation);
        currentAngle = new Vector3
        (
            0.0f,
            Mathf.LerpAngle(currentAngle.y, movementAngle, Time.deltaTime),
            0.0f
        );
        transform.eulerAngles = currentAngle;
    }
}
