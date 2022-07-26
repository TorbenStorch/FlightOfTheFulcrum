using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLiquid : MonoBehaviour
{
    [SerializeField] private MeshRenderer glassMeshRenderer;
    private Material _material;
    private float _futureValue;

    void Start()
    {
        _material = GetComponent<MeshRenderer>().sharedMaterial;
        _material.SetFloat("_Fill", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        _futureValue = glassMeshRenderer.sharedMaterial.GetFloat("_BlendToFuture");
        _material.SetFloat("_Fill", 1f - _futureValue);
    }
}
