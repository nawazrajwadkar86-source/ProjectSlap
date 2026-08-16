using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is
    public List<Image> Heart_img;
    public float fill_amount;
    public float reduction_amount = 1.0f;
    public static Player_Health Instance;
    public bool bDead;
    void Start()
    {
        Instance = this;
  
    }

    // Update is called once per frame
    void Update()
    {
        bDead = deathCheck();
        if (bDead)
        {
            GameOver();
        }
    }

    public void Hurt()
    {
        
        fill_amount = reduction_amount;

        foreach (var item in Heart_img)
        {
            if (item.fillAmount == 0)
            {
                
                continue;
            }

            //float Remaining_amt = 1 - fill_amount;

            float sub = Mathf.Min(item.fillAmount, fill_amount);

            item.fillAmount -= sub;
            fill_amount -= sub;
        }
        //reduction_amount = 2;
    }

    private bool deathCheck()
    {
        return Heart_img[Heart_img.Count - 1].fillAmount == 0;

    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
