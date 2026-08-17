using UnityEngine;

public class ObstacleBreak : MonoBehaviour,IObstacle
{
    public GameObject BrokenObj;
    public Rigidbody[] BrokenGlasses;
    private void OnEnable()
    {
        foreach(Rigidbody t in BrokenGlasses)
        {
            t.transform.localPosition    = Vector3.zero;
            t.transform.localRotation = Quaternion.identity;
        }
        BrokenObj.SetActive(false);
    }
    public void resetDamage()
    {
        foreach(Rigidbody t in BrokenGlasses)
        {
            t.transform.localPosition    = Vector3.zero;
            t.transform.localRotation = Quaternion.identity;
        }
        BrokenObj.SetActive(false);
    }
    public void OnHit(Vector3 hitPos)
    {
        BrokenObj.transform.position = transform.position;
        BrokenObj.SetActive(true);
        gameObject.SetActive(false);
    }

}
