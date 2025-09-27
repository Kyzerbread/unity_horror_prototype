using UnityEngine;

public class FOVAdjuster : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public FirstPersonMovement movement; // needs IsSprinting exposed
    

    [Header("Settings")]
    public float sprintFOVIncrease = 6f; // extra degrees while sprinting
    public float fovLerpSpeed = 10f;      // how quickly FOV adjusts

    private float defaultFOV;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = GetComponent<Camera>();

        defaultFOV = mainCamera.fieldOfView;
    }

    void Update()
    {
        // How fast are we moving horizontally (ignore vertical velocity for jumping/falling)
        Vector3 horizontalVel = new Vector3(movement.Velocity.x, 0f, movement.Velocity.z);
        float speed = horizontalVel.magnitude;

        // Set a threshold so tiny jitters don’t trigger sprint FOV
        bool isMoving = speed > 0.1f;

        float targetFOV = (movement.IsSprinting && isMoving)
            ? defaultFOV + sprintFOVIncrease
            : defaultFOV;

        mainCamera.fieldOfView = Mathf.Lerp(
            mainCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * fovLerpSpeed
        );
    }

}
