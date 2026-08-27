using UnityEngine;
using UnityEngine.Events;

public class StairGatesMain : MonoBehaviour
{
    public  Animator anim;
    private bool Dynamic;
    private int randomDoorIndex;
    
    public void ReciveDoorIndex(int i)
    {
                Dynamic = Random.Range(0.0f,1.0f) > 0.5f? true:false;
                randomDoorIndex = i;

        if(!Dynamic)OpenDoor();
    }
    private void OpenDoor()
    {
        switch (randomDoorIndex)
        {
            case 0:
            anim.CrossFade("left Opened",1);
            break;
            case 1:
            anim.CrossFade("Middle Opened",1);
            break;
            case 2:
            anim.CrossFade("Right Opened",1);
            break;
        }
    }

    private void CloseDoor()
    {
        anim.CrossFade("Default",1);
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player") && Dynamic)
        {
            Debug.Log("OpenDoor");
            OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }
}
