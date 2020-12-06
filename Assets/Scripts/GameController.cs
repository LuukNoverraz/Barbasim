using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCamera;
    public bool skipIntro;
    private RaycastHit hit;
    public GameObject startMenu;
    public GameObject creatureInfo;
    public GameObject followCanvas;
    public GameObject creatureImage;
    public Text[] creatureTexts;
    public GameObject child;
    public CreatureController creatureController;
    public int totalDeaths;
    public int totalBirths;
    public TextMesh[] debugText;
    public GameObject debugTower;


    void Start()
    {
        creatureInfo.SetActive(false);
        followCanvas.SetActive(false);

        if (!skipIntro)
        {
            startMenu.SetActive(true);
        }
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
                    creatureController = hit.collider.GetComponent<CreatureController>();
                    creatureImage.GetComponent<Image>().color = (creatureController.chosenColor.color);
                    creatureImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, ((creatureController.chosenScale) * 1.42857142857f));
                    creatureTexts[0].text = (creatureController.creatureName);
                    creatureTexts[1].text = (creatureController.chosenColor.name);
                    creatureTexts[2].text = ((creatureController.chosenScale) / 10).ToString() + " cm";
                    creatureTexts[3].text = (creatureController.chosenStrength).ToString() + "%";
                    creatureTexts[4].text = (creatureController.chosenFriendliness).ToString() + "%";
                    creatureTexts[5].text = (creatureController.chosenSpeed).ToString() + "%";
                    creatureTexts[6].text = (creatureController.father);
                    creatureTexts[7].text = (creatureController.mother);
                    creatureInfo.SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && creatureInfo.activeSelf)
        {
            FollowCreature();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && creatureInfo.activeSelf)
        {
            DisableInfo();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && mainCamera.GetComponent<CameraFollow>().target)
        {
            StopFollowing();
        }
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1.0f;
        mainCamera.farClipPlane = 1000.0f;
    }

    public void FollowCreature()
    {
        DisableInfo();
        followCanvas.SetActive(true);
        mainCamera.GetComponent<CameraFollow>().LockOn();
    }

    public void StopFollowing()
    {
        followCanvas.SetActive(false);
        mainCamera.GetComponent<CameraFollow>().LockOff();
    }

    public void DisableInfo()
    {
        creatureInfo.SetActive(false);
    }
}
