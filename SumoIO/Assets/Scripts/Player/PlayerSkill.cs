using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerSkill : MonoBehaviour
{
    //oyuncunun skilleri (skil süreleri ve bekleme) gibi yapýlarýn yazýldýðý class
    [SerializeField] private GameObject skill1Explosion;
    [SerializeField] private GameObject skill2Explosion;
    [SerializeField] private Image skill1Fill;
    [SerializeField] private Image skill2Fill;

    [SerializeField] private Text skill1text;
    [SerializeField] private Text skill2text;
    private bool[] cooldowns = new bool[2];
    void Start()
    {
        for (int i = 0; i < cooldowns.Length; i++)
        {
            cooldowns[i] = true;
        }
    }

    
    public void Skill1()
    {
        if (cooldowns[0])
        {
          StartCoroutine(SkillTime(skill1Fill, skill1text, 12, 0));
          Instantiate(skill1Explosion, transform.position, Quaternion.identity);
        }

    }


    public void Skill2()
    {
        if (cooldowns[1])
        {
            StartCoroutine(SkillTime(skill2Fill, skill2text, 15, 1));
            Instantiate(skill2Explosion, transform.position, Quaternion.identity);

        }

    }












    private IEnumerator SkillTime(Image skillFill, Text textTime, float skillTime, int index)
    {
        float elapsedTime = 0;
        float fillValue = 1;
        skillFill.fillAmount = 1;
        textTime.gameObject.SetActive(true);
        cooldowns[index] = false;
        while (elapsedTime < skillTime)
        {
            elapsedTime += Time.deltaTime;
            fillValue = Mathf.Lerp(1, 0, (elapsedTime / skillTime));
            var timeValue = (float)System.MathF.Round((skillTime - elapsedTime), 1);
            textTime.text = timeValue.ToString();
            skillFill.fillAmount = fillValue;
            yield return null;
        }
        skillFill.fillAmount = 0;
        textTime.gameObject.SetActive(false);
        cooldowns[index] = true;

    }



}
