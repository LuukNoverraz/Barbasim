using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private RaycastHit hit;
    public GameObject creatureInfo;
    public GameObject creatureImage;
    public Text[] creatureTexts;
    public GameObject child;
    public int totalDeaths;
    public int totalBirths;
    public TextMesh[] debugText;
    public GameObject debugTower;


    void Start()
    {
        creatureInfo.SetActive(false);
    }

    void Update()
    {
        debugText[0].text = totalDeaths.ToString();
        debugText[1].text = totalBirths.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Creature")
                {
                    creatureImage.GetComponent<Image>().color = (hit.collider.GetComponent<CreatureController>().chosenColor.color);
                    creatureImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, ((hit.collider.GetComponent<CreatureController>().chosenScale) * 1.42857142857f));
                    creatureTexts[0].text = (hit.collider.GetComponent<CreatureController>().creatureName);
                    creatureTexts[1].text = (hit.collider.GetComponent<CreatureController>().chosenColor.name);
                    creatureTexts[2].text = ((hit.collider.GetComponent<CreatureController>().chosenScale) / 10).ToString() + " cm";
                    creatureTexts[3].text = (hit.collider.GetComponent<CreatureController>().chosenStrength).ToString() + "%";
                    creatureTexts[4].text = (hit.collider.GetComponent<CreatureController>().chosenFriendliness).ToString() + "%";
                    creatureTexts[5].text = (hit.collider.GetComponent<CreatureController>().chosenSpeed).ToString() + "%";
                    creatureInfo.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableInfo();
        }  
    }

    public void DisableInfo()
    {
        creatureInfo.SetActive(false);
    }
}
