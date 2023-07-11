using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Bu class oyun içerisindeki güçlendirme ve düþmanlarý oluþturmak için yazýlmýþtýr.
    public GameObject powerPrefab;
    public Transform spawnParent;//nesneleri bir tane parent nesne içerisinde
    //oluþturuyorum. tamamýný silme gibi bir iþleme ihtiyaç duyarsam direkt parenti silmek için
    
    //platformun uzunluðununa göre oluþturma iþlemini gerçekleþtiriyorum
    public float platformWidth;
    public float platformLength;
    [SerializeField] float spawnInterval = 10f;// oluþturma süresi
    [SerializeField] GameObject Enemy;//oluþturmak istediðim düþman
    public int EnemyCount { get; set; } = 7;//oluþturcaðým düþman sayýsý
    private void Start()
    {
        //her 10 saniyede bir güçlendirme oluþmasý
        InvokeRepeating("SpawnPower", 0f, spawnInterval);
        for (int i = 0; i < EnemyCount; i++)
        {  //sahne baþlayýnca düþmanlarý oluþturuyorum.
  
            float spawnPosX = Random.Range(-platformWidth / 2f, platformWidth / 2f);
            float spawnPosZ = Random.Range(-platformLength / 2f, platformLength / 2f);
            Vector3 spawnPosition = new Vector3(spawnPosX, 1.5f, spawnPosZ);
            Instantiate(Enemy, spawnPosition, Quaternion.identity, spawnParent);
        }
    }

    void SpawnPower()
    {
        //platform üzerinde random noktalarda güçlendirme oluþturuyorum
        float spawnPosX = Random.Range(-platformWidth / 2f, platformWidth / 2f);
        float spawnPosZ = Random.Range(-platformLength / 2f, platformLength / 2f);
        Vector3 spawnPosition = new Vector3(spawnPosX, 1.5f, spawnPosZ);
        Instantiate(powerPrefab, spawnPosition, Quaternion.identity, spawnParent);
    }
}
