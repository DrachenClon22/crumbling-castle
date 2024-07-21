using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static bool CameraEnabled = true;

    public enum mode { LOCKED = 0, HIDDEN, LOCKED_HIDDEN };
    public static mode CursorMode = mode.LOCKED_HIDDEN;

    public static Vector2 CalculatedOffsetForUI { get; private set; } = Vector2.zero;

    [Header("General")]
    public float sensitivity = 3f;
    public CharacterController characterController;

    [Header("Advanced")]
    public float minY = -90;
    public float maxY = 90;
    [Range(0, 4)] public int VSYNCMode = 1;
    [Range(1, 300)] public int maxFPS = 60;

    [Header("Camera")]
    [Tooltip("Leave empty to use current object")]
    public Transform cameraObject;

    private Camera cam;

    private float posX;
    private float posY;
    private float tempFOV = 75f;

    private Quaternion startCameraRotation;
    private Quaternion playerRotation;
    private Quaternion xaxis;
    private Quaternion yaxis;

    private void Start()
    {
        tempFOV = Camera.main.fieldOfView;

        playerRotation = characterController.transform.rotation;
        startCameraRotation = transform.localRotation;

        if (cameraObject == null)
        {
            cameraObject = transform;
        }

        cam = cameraObject.GetComponent<Camera>();

        DefineSettings();
    }

    private void DefineSettings()
    {
        QualitySettings.vSyncCount = VSYNCMode;
        Application.targetFrameRate = maxFPS;

        switch (CursorMode)
        {
            case mode.HIDDEN:
                {
                    Cursor.visible = false;
                    break;
                }

            case mode.LOCKED:
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                }
                
            case mode.LOCKED_HIDDEN:
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                }
        }
    }

    private void Update()
    {
        cam.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, tempFOV + characterController.velocity.magnitude, 0.1f);
    }

    /// <summary>
    /// <para>
    /// Toggle camera on or off.
    /// </para>
    /// <para>
    /// Use <b>true</b> for enabling camera and <b>false</b> for disabling
    /// </para>
    /// </summary>
    /// <param name="state">Target camera state</param>
    public static void ToggleCamera(bool state)
    {
        CameraEnabled = state;
        Cursor.visible = !state;
        Cursor.lockState = !state ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (CameraEnabled)
        {
            posX += Input.GetAxis("Mouse X") * sensitivity;
            posY += Input.GetAxis("Mouse Y") * sensitivity;

            posY = Mathf.Clamp(posY, minY, maxY);

            xaxis = Quaternion.AngleAxis(posX, Vector3.up);
            yaxis = Quaternion.AngleAxis(posY, Vector3.left);

            cameraObject.localRotation = startCameraRotation * yaxis;
            characterController.transform.localRotation = playerRotation * xaxis;
        }
    }
}