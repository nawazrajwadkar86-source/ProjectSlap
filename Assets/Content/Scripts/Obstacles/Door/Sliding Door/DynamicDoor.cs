using Unity.VisualScripting;
using UnityEngine;

public class DynamicDoor : MonoBehaviour
{
    int openedDoorIndex;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CloseDoor();
    }

    private void openDoor()
    {
        openedDoorIndex = Random.Range(0,3);

        switch (openedDoorIndex)
        {
            case 0:
            anim.CrossFade("Door 1",1);
            break;
            case 1:
            anim.CrossFade("Door 2",1);
            break;
            case 2:
            anim.CrossFade("Door 3",1);
            break;
        }
    }
    private void CloseDoor()
    {
        anim.CrossFade("Door Default",1);
    }
}
