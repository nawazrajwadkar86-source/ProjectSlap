using UnityEngine;

public class EventSlapped : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Target target;
    public void EnableOnslapped()
    {
        target.bisSlapped = true;
    }
}
