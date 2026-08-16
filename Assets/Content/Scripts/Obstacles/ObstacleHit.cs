using System.Collections;
using UnityEngine;

public class ObstacleHit : MonoBehaviour,IObstacle
{
    private Vector3 initialPos;
    private Vector3 EndPos;
    public void OnHit(Vector3 hitPos)
    {
        StartCoroutine(StartAnim());
    }
    IEnumerator StartAnim()
    {
        float time = 0;
        initialPos = transform.position;
        EndPos = transform.position + Vector3.left * 0.5f;

        while (time < 2)
        {
            time += Time.deltaTime;
            transform.position =Vector3.Lerp(initialPos,EndPos,time);
            yield return null;
        }
    }

}
interface IObstacle
{
    void OnHit(Vector3 hitPos);
}