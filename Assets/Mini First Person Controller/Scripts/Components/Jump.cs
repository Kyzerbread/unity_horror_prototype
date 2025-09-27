using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(CharacterController))]
public class Jump : MonoBehaviour
{
    

    private CharacterController controller;
    private InputHandler inputHandler;
    private Vector3 velocity; // vertical velocity stored here

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputHandler = GetComponent<InputHandler>();
    }

 

    void Update()
    {
      
    }

}
