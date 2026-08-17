using System.Collections;
using UnityEngine;

public class ObstacleHit : MonoBehaviour,IObstacle
{
    private Vector3 initialPos;
    private Vector3 EndPos;
    private Quaternion initialAngle;
    private float EndAngle;
    private float animSpeed = 8f; 
    public void OnHit(Vector3 hitPos)
    {
        StartCoroutine(StartAnim());
    }
    IEnumerator StartAnim()
    {
        float time = 0;
        initialPos = transform.position;
        float sign = Random.value < 0.5 ? 1:-1;
        EndPos = transform.position + Vector3.left * 0.5f * sign;

        initialAngle = transform.rotation;
        EndAngle = initialAngle.y - 70 * sign;

        while (time < 1)
        {
            time += Time.deltaTime * animSpeed;
            transform.position =Vector3.Lerp(initialPos,EndPos,time);

            transform.rotation = Quaternion.Lerp(initialAngle,Quaternion.Euler(0,EndAngle,0),time);
            yield return null;
        }
    }

}
interface IObstacle
{
    void OnHit(Vector3 hitPos);
}