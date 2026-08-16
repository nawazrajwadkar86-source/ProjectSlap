using UnityEngine;

public class ObstacleBreak : MonoBehaviour,IObstacle
{
    public GameObject BrokenObj;
    public void OnHit(Vector3 hitPos)
    {
        BrokenObj.SetActive(true);
        gameObject.SetActive(false);
    }

}
