using System;
using UnityEngine;

public class BlinkingColor : MonoBehaviour
{
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = Color.white;

    [Range(0, 10)] [SerializeField] private float speed = 1;
    
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        _renderer.material.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
