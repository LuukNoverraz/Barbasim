using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool skipIntro = false;
    public GameController gameController;
    public Transform target;
    public Transform origin;
    private RaycastHit mapHit;
    
    // FOLLOW INFO
    public float distance = 2.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float yMinLimit = 0f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;

    // ISLAND INFO
    public float distanceIsland = 2.0f;
    public float xSpeedIsland = 20.0f;
    public float ySpeedIsland = 20.0f;
    public float yMinLimitIsland = 0f;
    public float yMaxLimitIsland = 90f;
    public float distanceMinIsland = 10f;
    public float distanceMaxIsland = 10f;
    public float smoothTimeIsland = 2f;
    float rotationYAxisIsland = 0.0f;
    float rotationXAxisIsland = 0.0f;
    float velocityXIsland = 0.0f;
    float velocityYIsland = 0.0f;

    void Start()
    {        
        if (!gameController.skipIntro)
        {
            Time.timeScale = 0;

            GetComponent<Camera>().farClipPlane = 1.0f;
        }
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
    }
    public void LockOn()
    {
        target = gameController.creatureController.gameObject.transform;
    }
    public void LockOff()
    {
        target = null;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray mapRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mapRay, out mapHit))
            {
                origin.transform.position = mapHit.point;
            }
        }
    }
    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }
            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
            // RaycastHit hit;
            // if (Physics.Linecast(target.position, transform.position, out hit))
            // {
            //     distance -= hit.distance;
            // }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
            velocityX = Mathf.Lerp(velocityX, 0, Time.unscaledDeltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.unscaledDeltaTime * smoothTime);
        }

        else if (!target)
        {
            gameController.StopFollowing();

            if (Input.GetMouseButton(0) && !gameController.startMenu.activeSelf)
            {
                velocityXIsland += xSpeedIsland * Input.GetAxis("Mouse X") * distanceIsland * 0.02f;
                velocityYIsland += ySpeedIsland * Input.GetAxis("Mouse Y") * 0.02f;
            }
            rotationYAxisIsland += velocityXIsland;
            rotationXAxisIsland -= velocityYIsland;
            rotationXAxis = ClampAngle(rotationXAxisIsland, yMinLimitIsland, yMaxLimitIsland);
            Quaternion fromRotationIsland = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotationIsland = Quaternion.Euler(rotationXAxis, rotationYAxisIsland, 0);
            Quaternion rotationIsland = toRotationIsland;

            distanceIsland = Mathf.Clamp(distanceIsland - Input.GetAxis("Mouse ScrollWheel") * 45, distanceMinIsland, distanceMaxIsland);
            // RaycastHit hit;
            // if (Physics.Linecast(origin.position, transform.position, out hit))
            // {
            //     distanceIsland += hit.distance;
            // }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distanceIsland);
            Vector3 position = rotationIsland * negDistance + origin.position;

            transform.rotation = rotationIsland;
            transform.position = position;
            velocityXIsland = Mathf.Lerp(velocityXIsland, 0, Time.unscaledDeltaTime * smoothTimeIsland);
            velocityYIsland = Mathf.Lerp(velocityYIsland, 0, Time.unscaledDeltaTime * smoothTimeIsland);
        }
    }
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
