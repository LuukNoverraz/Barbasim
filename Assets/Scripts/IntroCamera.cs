using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public GameController gameController;
    void Start()
    {
        if (!gameController.skipIntro)
        {
            Time.timeScale = 0;

            GetComponentInChildren<Camera>().farClipPlane = 1.0f;
        }
    }
}
