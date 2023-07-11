using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //Bu class oyun i�erisindeki g��lendirme ve d��manlar� olu�turmak i�in yaz�lm��t�r.
    public GameObject powerPrefab;
    public Transform spawnParent;//nesneleri bir tane parent nesne i�erisinde
    //olu�turuyorum. tamam�n� silme gibi bir i�leme ihtiya� duyarsam direkt parenti silmek i�in
    
    //platformun uzunlu�ununa g�re olu�turma i�lemini ger�ekle�tiriyorum
    public float platformWidth;
    public float platformLength;
    [SerializeField] float spawnInterval = 10f;// olu�turma s�resi
    [SerializeField] GameObject Enemy;//olu�turmak istedi�im d��man
    public int EnemyCount { get; set; } = 7;//olu�turca��m d��man say�s�
    private void Start()
    {
        //her 10 saniyede bir g��lendirme olu�mas�
        InvokeRepeating("SpawnPower", 0f, spawnInterval);
        for (int i = 0; i < EnemyCount; i++)
        {  //sahne ba�lay�nca d��manlar� olu�turuyorum.
  
            float spawnPosX = Random.Range(-platformWidth / 2f, platformWidth / 2f);
            float spawnPosZ = Random.Range(-platformLength / 2f, platformLength / 2f);
            Vector3 spawnPosition = new Vector3(spawnPosX, 1.5f, spawnPosZ);
            Instantiate(Enemy, spawnPosition, Quaternion.identity, spawnParent);
        }
    }

    void SpawnPower()
    {
        //platform �zerinde random noktalarda g��lendirme olu�turuyorum
        float spawnPosX = Random.Range(-platformWidth / 2f, platformWidth / 2f);
        float spawnPosZ = Random.Range(-platformLength / 2f, platformLength / 2f);
        Vector3 spawnPosition = new Vector3(spawnPosX, 1.5f, spawnPosZ);
        Instantiate(powerPrefab, spawnPosition, Quaternion.identity, spawnParent);
    }
}
