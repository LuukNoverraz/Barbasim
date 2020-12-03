using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameController gameController;

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

    private float movementAngle;
    private Vector3 currentAngle;

    private float movementSpeed = 0.5f;
    private float movementVariation = 0.2f;

    // Interaction

    private float chanceOfNothing;
    private float chanceOfKnex;
    private float chanceOfFight;

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

        chosenColor = creatureColors[Random.Range(0, 8)];
        chosenScale = Random.Range(30, 100);
        chosenStrength = Random.Range(0, 100);
        chosenFriendliness = Random.Range(0, 100);
        chosenSpeed = Random.Range(0, 100);

        gameObject.GetComponent<Renderer>().material = chosenColor;
        gameObject.transform.localScale = new Vector3(transform.localScale.x, chosenScale, transform.localScale.z);
        movementSpeed = chosenSpeed / 25;

        nameTitle.text = creatureName;

        // Start movement

        timeBetweenActions = Random.Range(0.5f, 4.0f);
        currentAngle = transform.eulerAngles;
        InvokeRepeating("MoveCreature", timeBetweenActions, timeBetweenActions);
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
            Destroy(gameObject);
            // gameController.totalDeaths++;
        }

        if (col.tag == "CreatureArea")
        {
            chanceOfNothing = Random.Range(0, 100);
            chanceOfKnex = Random.Range(0, 100);
            chanceOfFight = Random.Range(0, 100);

            if (chanceOfNothing < 3)
            {
                if ((Random.Range(0, 100) < 100) && Random.Range(0, 100) > 66)
                {
                    Debug.Log("NONE");
                } 
                if ((Random.Range(0, 100) < 66) && Random.Range(0, 100) > 33)
                {
                    Debug.Log("KNEX");
                    gameController.totalBirths++;
                }
                if ((Random.Range(0, 100) < 33) && Random.Range(0, 100) > 0)
                {
                    Debug.Log("FIGHT");
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
