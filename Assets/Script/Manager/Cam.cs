using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CAMERA_TARGET 
{
    PLAYER,
    BOSS,
    BOSS_AND_PALYER,
}

public class Cam : MonoBehaviour //카메라 매니저
{
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineImpulseSource source;

    public GameObject[] Target;
    // Start is called before the first frame update
    private void Awake()
    {
        V.Cam = this;
    }

    public void ShakeRepeat(float time) //카메라 쉐이크 반복
    {
        InvokeRepeating("Shake", 0, time);
    }

    public void Shake() //카메라 쉐이크
    {
        source.GenerateImpulse();
        if (V.UI.WarningText.gameObject.activeSelf) V.Sound.SFX_Play(SFX_SOUND.WARNING);
    }

    void Start()
    {
        VirtualCamera = GameObject.Find("Virtual").GetComponent<CinemachineVirtualCamera>();
        source = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTarget(CAMERA_TARGET target) //카메라 타겟 변경
    {
        VirtualCamera.Follow = Target[(int)target].transform;
    }

}
