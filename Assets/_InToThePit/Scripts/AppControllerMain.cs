using System;
using UnityEngine;

[DefaultExecutionOrder(-100)] // Ensures this script runs before most other scripts
public class AppControllerMain : MonoBehaviour
{
    private bool _initialized;
    
    private GameControllerCore  GameControllerCore { get; set; }
    private UI_Manager UI_Manager { get; set; }
    private GameStateManager GameStateManager { get; set; }
    

    private void Awake()
    {
        if(_initialized) return;
        _initialized = true;
        
        //Aqui hacemos la gerarquia de orden de inicialisacion de los proyectos.

        //Gameplay Controller (Game Logic) 
        GameControllerCore = FindAnyObjectByType<GameControllerCore>();
        GameControllerCore.Init();
        
        //UI Manager (References of canvas screens)
        UI_Manager = FindAnyObjectByType<UI_Manager>();
        UI_Manager.Init();
        
        GameStateManager = FindAnyObjectByType<GameStateManager>();
        GameStateManager.Init();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
