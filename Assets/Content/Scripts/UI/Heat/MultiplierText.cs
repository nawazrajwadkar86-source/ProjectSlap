using UnityEngine;

public class MultiplierText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Invoke(nameof(DisableAnimator), 2);
    }
    private void OnDisable()
    {
        animator.enabled = true;
    }
    public void DisableAnimator()
    {
        Debug.LogWarning("Disabled Animator");
        animator.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
