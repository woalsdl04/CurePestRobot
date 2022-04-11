using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    MeshRenderer mr = null;

    public float  ScrollSpeed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mr.material.SetTextureOffset("_MainTex", new Vector2(0, -(ScrollSpeed * V.WorldTIme)));
    }
}
