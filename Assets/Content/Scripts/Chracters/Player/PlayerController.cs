using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
     CharacterController CC { get;set; }
    [Range(0, 20)]
    public float VerticalSpeed = 5;
    [Range(0, 20)]
    public float HorizontalSpeed = 5;

    public static PlayerController playerController_Instance;
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
        move();
    }

    void move()
    {
        float fwd_Dir = 1;
        float right = Input.GetAxis("Horizontal") * HorizontalSpeed;
        float forward = fwd_Dir * VerticalSpeed ;
        Vector3 DesiredMoveDir = new Vector3(right, 0, forward);
        CC?.Move(DesiredMoveDir * Time.deltaTime);
    }
}


