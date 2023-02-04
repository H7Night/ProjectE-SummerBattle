using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camera : MonoBehaviour
{
    public static Camera Instance;
    
    private CinemachineConfiner _cinemachineConfiner;
    public PolygonCollider2D area;
    private String currentSceceName = "00";
    private bool changed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        _cinemachineConfiner = GetComponent<CinemachineConfiner>();
        area = GameObject.Find("Area").GetComponent<PolygonCollider2D>();
        GetSize();
    }

    private void Update()
    {
        CheckScence();
        if (changed)
        {
            
            area = GameObject.Find("Area").GetComponent<PolygonCollider2D>();
            GetSize();
        }
    }
    
    private void GetSize()
    {
        _cinemachineConfiner.m_BoundingShape2D = area;
    }
    
    private void CheckScence()
    {
        var activeScene = SceneManager.GetActiveScene();
    
        if (currentSceceName != activeScene.name)
        {
            changed = true;
            currentSceceName = activeScene.name;
            Console.WriteLine(currentSceceName);
        }
    }
    
}