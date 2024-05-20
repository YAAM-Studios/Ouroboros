using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera eniaVirtualCamera;
    public CinemachineVirtualCamera hyettaVirtualCamera;

    [Header("Camera Settings")] 
    public float orthoSize = 10f;

    public float nearClippingPlane;
    public float farClippingPlane = 1000f;

    public Vector3 trackedObjectOffset = new (0f, 0.67f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        ChangeCameraSettings(eniaVirtualCamera);
        ChangeCameraSettings(hyettaVirtualCamera);
    }

    void ChangeCameraSettings(CinemachineVirtualCamera camera)
    {
        camera.m_Lens.Orthographic = true;
        camera.m_Lens.OrthographicSize = orthoSize;
        camera.m_Lens.NearClipPlane = nearClippingPlane;
        camera.m_Lens.FarClipPlane = farClippingPlane;
        var transposer = camera.AddCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_TrackedObjectOffset = trackedObjectOffset;
    }
}
