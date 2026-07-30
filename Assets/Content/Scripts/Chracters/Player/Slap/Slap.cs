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
                target?.CallOnHitTargetEvent(target.type);
            Debug.LogWarning("Trigger SLAPPEDDDDDDDDD !");
        }
    }
}
