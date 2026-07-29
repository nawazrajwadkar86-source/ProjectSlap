using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     Animator animator;
    private GameObject Current_Enemy;
    public GameObject PlayerMesh;
    [Range(0, 10)]
    public float SlapRange = 1;
    public List<GameObject> totatWorldTargets;

    //Mobile Inputs
    Touch touch;
    
    public enum ESlapType { 
    
        none,
        front_slap,
        right_slap,
        left_slap,
    
    }
    public ESlapType ESlap_type = ESlapType.right_slap;
    void Start()
    {
        totatWorldTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("npc"));
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    Debug.Log("Touch on UI");
                    return;
                }
            slap(ESlap_type);
            }

        }
#endif

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Touch on UI");
                return;
            }
            slap(ESlap_type);
        }

#endif
    }
    private void OnDrawGizmos()
    {
        if (getCurrentTarget() != null)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(getCurrentTarget().transform.position , 1f);
   
        }
    }
    void slap(ESlapType e_slapType)
    {
        
        animator.SetTrigger(e_slapType.ToString());
        ChooseSlapType(ref e_slapType);

        
    }
    void ChooseSlapType(ref ESlapType slapType)
    {
        if (getCurrentTarget() == null)
        {
            return;
        }
        Vector3 RelativeLocation = transform.InverseTransformPoint(getCurrentTarget().transform.position);
        if(RelativeLocation.x > 0)
        {
            Flip_animation_temp(1);
            slapType = ESlapType.right_slap;
        }
        else if(RelativeLocation.x < 0)
        {
            Flip_animation_temp(-1);
            slapType = ESlapType.left_slap;
        }
        else if(RelativeLocation.z > 0)
        {
            slapType = ESlapType.front_slap;
        }
        else
        {
            slapType = ESlapType.right_slap;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       /* if(other.transform.CompareTag("npc")){
            Current_Enemy = other.gameObject;
        }*/
    }
    GameObject getCurrentTarget()
    {
  
       GameObject bestTarget = null;
        float Bestcost = Mathf.Infinity;
        float MaxDistance = SlapRange;
        foreach(var target in totatWorldTargets)
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist > MaxDistance)
            {
                continue;
            }
            if(dist < Bestcost)
            {
                Bestcost = dist;
                bestTarget = target;
            }
        }

        return bestTarget;
    }
    void Flip_animation_temp(float flipdir)
    {
        
        PlayerMesh.transform.localScale = new Vector3(flipdir,1,1);

    }
  
}
