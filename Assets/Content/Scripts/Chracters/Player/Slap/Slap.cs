using UnityEngine;

public class Slap: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Hand_Bone;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MultiSlap()
    {
        Collider[] cols = Physics.OverlapSphere(Hand_Bone.transform.position, 2);
        foreach (var col in cols)
        {
            col.GetComponent<Target>().CallOnHitTargetEvent(col.GetComponent<Target>().type);
            col.GetComponent<Target>().SetSpeed(PlayerController.playerController_Instance.VerticalSpeed - 1);
            Debug.LogWarning("MultiSlap SLAPPEDDDDDDDDD !");
        }
    }
    void SingeleSlap(Collider other)
    {

       Target target =  other.transform.GetComponent<Target>();
        if (!target.bisSlapped)
        {
           // SlapCounter.Instance.EventOnSlap();
          //  target?.CallOnHitTargetEvent(target.type);
           
        }
    

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("npc"))
        {
            if (!SlapManager.slapManager_instance.bMultiSlap)
            {

                SingeleSlap(other);
            }
            else
            {

                MultiSlap();
            }
        }
    }


}
