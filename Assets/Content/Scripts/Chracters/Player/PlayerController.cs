using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float runSpeed;
    private float[] laneXPos = { -1f, 0f, 1f };
    private int currentLaneIndex = 1;
    Vector3 moveDir;
    private float moveX;

    private void Update()
    {
        Move();
        Inputs();
    }
    private void Inputs()
    {
        moveX = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        moveDir.z = runSpeed * Time.deltaTime;
        moveDir.x = moveX *runSpeed* Time.deltaTime;

        controller.Move(moveDir);
    }
    
}


