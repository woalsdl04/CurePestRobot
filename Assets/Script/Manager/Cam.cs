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

public class Cam : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineImpulseSource source;

    public GameObject[] Target;
    // Start is called before the first frame update
    private void Awake()
    {
        V.Cam = this;
    }

    public void ShakeRepeat(float time)
    {
        InvokeRepeating("Shake", 0, time);
    }

    public void Shake() 
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

    public void ChangeTarget(CAMERA_TARGET target) 
    {
        VirtualCamera.Follow = Target[(int)target].transform;
    }

}
