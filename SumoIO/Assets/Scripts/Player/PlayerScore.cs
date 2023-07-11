using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    //oyuncunun, oyunda geçen her bir süreye paun almasýný saðlýyorum 
    private float puan;
    private float timer;
    [SerializeField] float scoreTime;
    [SerializeField] TextMeshProUGUI scoreText;

    

    private void FixedUpdate()
    {        
            timer += Time.deltaTime;
            if (timer > scoreTime)
            {
                puan += 1.2f;
                scoreText.text = "Score : " + puan.ToString("F2");
                timer = 0;
            }

    }


    public void AddScore(float amount)
    {
        puan+= amount;
        scoreText.text = "Score : " + puan.ToString("F2");
    }



}
