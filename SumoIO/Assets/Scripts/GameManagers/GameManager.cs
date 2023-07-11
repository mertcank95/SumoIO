using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //oyunda geçen kronometreyi ve düþman sayýsýný tutuðum ve iþlemleri 
    //gerçekleþtirdiðim class
    public float countdownDuration = 45f;
    private float countdownTimer;
    [SerializeField] TextMeshProUGUI timerText;
    GameObject[] enemyies;
    public bool GameComplate { get; set; }
    public int EnemyCount { get; set; }
    GameSceneManager gameSceneManager;

    void Start()
    {
        
        countdownTimer = countdownDuration;
        enemyies = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCount= enemyies.Length;
        gameSceneManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GameSceneManager>();    
    }

    void Update()
    {
        countdownTimer -= Time.deltaTime;
        if (countdownTimer <= 0f)
        {
            countdownTimer = 0f;
            GameComplate = true;
            gameSceneManager.GameComplatePanel();
            if (EnemyCount>0)
            {
                foreach (var item in enemyies)
                {
                    Destroy(item);
                }
            }
        }

        int minutes = Mathf.FloorToInt(countdownTimer / 60f);
        int seconds = Mathf.FloorToInt(countdownTimer % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void EnemyCountControl()
    {
        EnemyCount -= 1;
        Debug.Log(EnemyCount);
        if(EnemyCount<=0)
        {
            //oyunu bitir;
            GameComplate = true;
            gameSceneManager.GameComplatePanel();
        }
    }


}
