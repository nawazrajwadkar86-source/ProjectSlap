using UnityEngine;

public class Slap: MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("npc"))
        {
            Target target =  other.transform.GetComponent<Target>();
            if (target != null)
            {
                Debug.Log("Target Found");
            }
            else
            {
                Debug.Log("Target not found");
            }
                target?.CallOnHitTargetEvent(target.type);
            Debug.LogWarning("Trigger SLAPPED !");
        }
    }
}
