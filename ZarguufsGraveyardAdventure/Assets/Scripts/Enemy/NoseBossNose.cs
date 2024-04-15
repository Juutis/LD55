using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseBossNose : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer noseRenderer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FlipNose(bool flipX)
    {
        noseRenderer.flipX = flipX;
    }

    public void Hide()
    {
        noseRenderer.enabled = false;
    }

    public void Show()
    {
        noseRenderer.enabled = true;
    }
}
