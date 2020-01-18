using Cinemachine;
using UnityEngine;
using UnityEngine.U2D;
using System.Reflection;

[RequireComponent(typeof(PixelPerfectCamera), typeof(CinemachineBrain))]
public class OrthographicOverride : MonoBehaviour
{
    private CinemachineBrain CB;
    private object Internal;
    private FieldInfo OrthoInfo;

    private void Start()
    {
        CB = GetComponent<CinemachineBrain>();

        Internal = typeof(PixelPerfectCamera).GetField("m_Internal", BindingFlags.NonPublic | BindingFlags.Instance)
                                             .GetValue(GetComponent<PixelPerfectCamera>());
        
        OrthoInfo = Internal.GetType().GetField("orthoSize", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    private void LateUpdate()
    {
        CinemachineVirtualCamera cam = CB.ActiveVirtualCamera as CinemachineVirtualCamera;

        if (cam)
        {
            cam.m_Lens.OrthographicSize = (float) OrthoInfo.GetValue(Internal);
        }
    }
}
