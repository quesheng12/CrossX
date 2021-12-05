using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Master script that will handle reading some input on the controller and trigger special events like Teleport or
/// activating the MagicTractorBeam
/// </summary>
public class MasterController : MonoBehaviour
{
    static MasterController s_Instance = null;
    public static MasterController Instance => s_Instance;

    public XRRig Rig => m_Rig;

    // [Header("Setup")]
    // public bool DisableSetupForDebug = false;
    public Transform StartingPosition;
    // public GameObject TeleporterParent;

    public Transform camara;

    public float walkSpeed = 3;

    public bool gameOver = false;

    [Header("Reference")]
    // public XRRayInteractor RightTeleportInteractor;
    // public XRRayInteractor LeftTeleportInteractor;

    // public XRDirectInteractor RightDirectInteractor;
    // public XRDirectInteractor LeftDirectInteractor;

    // public MagicTractorBeam RightTractorBeam;
    // public MagicTractorBeam LeftTractorBeam;

    XRRig m_Rig;

    InputDevice m_LeftInputDevice;
    InputDevice m_RightInputDevice;

    // XRInteractorLineVisual m_RightLineVisual;
    // XRInteractorLineVisual m_LeftLineVisual;

    HandPrefab m_RightHandPrefab;
    HandPrefab m_LeftHandPrefab;

    XRReleaseController m_RightController;
    XRReleaseController m_LeftController;

    bool m_PreviousRightClicked = false;
    bool m_PreviousLeftClicked = false;

    bool m_LastFrameRightEnable = false;
    bool m_LastFrameLeftEnable = false;

    LayerMask m_OriginalRightMask;
    LayerMask m_OriginalLeftMask;

    List<XRBaseInteractable> m_InteractableCache = new List<XRBaseInteractable>(16);

    void Awake()
    {
        s_Instance = this;
        m_Rig = GetComponent<XRRig>();

    }

    void OnEnable()
    {
        InputDevices.deviceConnected += RegisterDevices;
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= RegisterDevices;
    }

    void Start()
    {
        transform.position = StartingPosition.position;
        transform.rotation = StartingPosition.rotation;
        Debug.Log(transform.position);


        InputDeviceCharacteristics leftTrackedControllerFilter = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left;
        List<InputDevice> foundControllers = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, foundControllers);

        if (foundControllers.Count > 0)
            m_LeftInputDevice = foundControllers[0];


        InputDeviceCharacteristics rightTrackedControllerFilter = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right;

        InputDevices.GetDevicesWithCharacteristics(rightTrackedControllerFilter, foundControllers);

        if (foundControllers.Count > 0)
            m_RightInputDevice = foundControllers[0];

        if (m_Rig.TrackingOriginMode != TrackingOriginModeFlags.Floor)
            m_Rig.cameraYOffset = 1.8f;
    }

    void RegisterDevices(InputDevice connectedDevice)
    {
        if (connectedDevice.isValid)
        {
            if ((connectedDevice.characteristics & InputDeviceCharacteristics.HeldInHand) == InputDeviceCharacteristics.HeldInHand)
            {
                if ((connectedDevice.characteristics & InputDeviceCharacteristics.Left) == InputDeviceCharacteristics.Left)
                {
                    m_LeftInputDevice = connectedDevice;
                }
                else if ((connectedDevice.characteristics & InputDeviceCharacteristics.Right) == InputDeviceCharacteristics.Right)
                {
                    m_RightInputDevice = connectedDevice;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        RightTeleportUpdate();
        LeftTeleportUpdate();
    }

    void RightTeleportUpdate()
    {
        if (!gameOver)
        {
            Vector2 axisInput;
            m_RightInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisInput);

            //人物移动
            transform.position += new Vector3(axisInput.y * camara.forward.x, 0, axisInput.y * camara.forward.z
            ) * Time.deltaTime * walkSpeed;

            transform.position += new Vector3(axisInput.x * camara.right.x, 0, axisInput.x * camara.right.z
            ) * Time.deltaTime * walkSpeed;

            // m_RightLineVisual.enabled = axisInput.y > 0.5f;

            // RightTeleportInteractor.InteractionLayerMask = m_LastFrameRightEnable ? m_OriginalRightMask : new LayerMask();

            // if (axisInput.y <= 0.5f && m_PreviousRightClicked)
            // {
            //     m_RightController.Select();
            // }


            // if (axisInput.y <= -0.5f)
            // {
            //     if (!RightTractorBeam.IsTracting)
            //         RightTractorBeam.StartTracting();
            // }
            // else if (RightTractorBeam.IsTracting)
            // {
            //     RightTractorBeam.StopTracting();
            // }

            //if the right animator is null, we try to get it. It's not the best performance wise but no other way as setup
            //of the model by the Interaction Toolkit is done on the first update.
            // if (m_RightHandPrefab == null)
            // {
            //     m_RightHandPrefab = RightDirectInteractor.GetComponentInChildren<HandPrefab>();
            // }

            m_PreviousRightClicked = axisInput.y > 0.5f;

            if (m_RightHandPrefab != null)
            {
                m_RightHandPrefab.Animator.SetBool("Pointing", m_PreviousRightClicked);
            }
        }
    }

    void LeftTeleportUpdate()
    {
        Vector2 axisInput;
        m_LeftInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out axisInput);

        // m_LeftLineVisual.enabled = axisInput.y > 0.5f;

        // LeftTeleportInteractor.InteractionLayerMask = m_LastFrameLeftEnable ? m_OriginalLeftMask : new LayerMask();

        // if (axisInput.y <= 0.5f && m_PreviousLeftClicked)
        // {
        //     m_LeftController.Select();
        // }

        // if (axisInput.y <= -0.5f)
        // {
        //     if (!LeftTractorBeam.IsTracting)
        //         LeftTractorBeam.StartTracting();
        // }
        // else if (LeftTractorBeam.IsTracting)
        // {
        //     LeftTractorBeam.StopTracting();
        // }


        transform.Rotate(new Vector3(0, axisInput.x * 5, 0));


        // if (axisInput.x < -0.95)
        // {
        //     transform.Rotate(new Vector3(0, , 0));
        // }

        //if the left animator is null, we try to get it. It's not the best performance wise but no other way as setup
        //of the model by the Interaction Toolkit is done on the first update.
        // if (m_LeftHandPrefab == null)
        // {
        //     m_LeftHandPrefab = LeftDirectInteractor.GetComponentInChildren<HandPrefab>();
        // }

        m_PreviousLeftClicked = axisInput.y > 0.5f;

        if (m_LeftHandPrefab != null)
            m_LeftHandPrefab.Animator.SetBool("Pointing", m_PreviousLeftClicked);

        // m_LastFrameLeftEnable = m_LeftLineVisual.enabled;
    }
}
