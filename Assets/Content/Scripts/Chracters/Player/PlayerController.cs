using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
     CharacterController CC { get;set; }
    [Range(0, 20)]
    public float VerticalSpeed = 5;
    [Range(0, 20)]
    public float HorizontalSpeed = 5;

    public static PlayerController playerController_Instance;
    private int DirX = 0;

    //-----------------------------------Swiping-----------------------------------------------
    [Space(20)]
    [Header("Swiping")]

    public float maxSwipeDistance = 500f;
    public float SwipeMoveDistance = 0.5f;
    event Action<Vector2> onSwipe ;
    public int currentLane = 2;
    [Range(0.1f,1f)]
    public float LaneSwitchSpeed = 0.5f;
    Vector2 startpos = Vector2.zero;
    Vector2 endpos = Vector2.zero;
    //--------------------------------------- Movement Mode Enum --------------------------------------------------------
    public enum EMovementMode
    {
        tap,
        swipe

    }public EMovementMode movementMode = EMovementMode.swipe;

    //--------------------------------------- Movement Mode Enum --------------------------------------------------------
    public enum ESwipe
    {
        none,
        right,
        left,
        up,
        down
    }ESwipe swipe = ESwipe.none;
    private void OnEnable()
    {
        onSwipe += HandleSwipe;
    }
    private void OnDisable()
    {
        onSwipe -= HandleSwipe;
        
    }
    private void Start()
    {
        CC = this.transform.GetComponent<CharacterController>();
        if(playerController_Instance == null)
        {
            playerController_Instance = this;
        }
    }

    private void Update()
    {

        PCmovement();
        Androidmovement();
    }
    public void HorizontalMove(int dir)
    {
        DirX = dir;
    }
    void PCmovement()
    {
        float fwd_Dir = 1;
        float right = Input.GetAxis("Horizontal") * HorizontalSpeed;
        float forward = fwd_Dir * VerticalSpeed ;
        Vector3 DesiredMoveDir = new Vector3(right, 0, forward);
        CC?.Move(DesiredMoveDir * Time.deltaTime);
    }
    void Androidmovement()
    {
        switch (movementMode) {
        case EMovementMode.tap:
                float fwd_Dir = 1;
                float right = DirX * HorizontalSpeed;
                float forward = fwd_Dir * VerticalSpeed;
                Vector3 DesiredMoveDir = new Vector3(right, 0, forward);
                CC?.Move(DesiredMoveDir * Time.deltaTime);
        break;


        case EMovementMode.swipe:

            

                if(Input.touchCount >= 1)
                {
                    Touch touch = Input.GetTouch(0);
                    if(touch.phase == TouchPhase.Began)
                    {
                        startpos = touch.position;
                    }
                    if(touch.phase == TouchPhase.Ended)
                    {
                        endpos = touch.position;
                       Vector2 delta = (endpos - startpos);
                 
                        onSwipe?.Invoke(delta);

                    }
                }

         break;
        }
        

    }

    void HandleSwipe(Vector2 delta)
    {
        if(Mathf.Abs(delta.x) < Mathf.Abs(delta.y))
        {
            return;
        }
        if(delta.magnitude < maxSwipeDistance)
        {
            return;
        }

        if(delta.x < -maxSwipeDistance)
        {
            swipe = ESwipe.left;
      
            Debug.Log(swipe);
        }
        else if (delta.x > maxSwipeDistance)
        {
            swipe = ESwipe.right;
            Debug.Log(swipe);
        }

        switch (swipe) {
            case ESwipe.right:
                SwitchLaneRight();
                break;
            case ESwipe.left:
                SwitchLaneLeft();
                break;
        }
        
    }

    private void SwitchLaneRight()
    {
        if(currentLane == 1 || currentLane == 2)
        {

            float TargetLocation =transform.position.x + SwipeMoveDistance * 1;
            transform.DOMoveX(TargetLocation, LaneSwitchSpeed);
            currentLane += 1;
        }
    }
    private void SwitchLaneLeft()
    {
        if (currentLane == 2 || currentLane ==3)
        {
            float TargetLocation =transform.position.x + SwipeMoveDistance * -1;
            transform.DOMoveX(TargetLocation, LaneSwitchSpeed);
            currentLane -= 1;
        }
    }

}


