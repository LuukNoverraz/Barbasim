using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameRotate : MonoBehaviour
{
    public Transform cameraTransform;
    
    void Start()
    {
        cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    void Update()
    {
        gameObject.transform.eulerAngles = new Vector3(
            cameraTransform.eulerAngles.x,
            cameraTransform.eulerAngles.y,
            cameraTransform.eulerAngles.z
        );
    }
}
