using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusControl : MonoBehaviour
{
    //Enum yap�lar� ile d��manlar�n durumlar�n� kontrol ediyoruz.
    internal Transform target { get; set; }
    //Enum de�i�kenimizin propertysi
    public EnemyStatus Status { get; set; }
    //Hedef ile Ajan aras� mesafe
    float distanceToPlayer;
    //bu metotler� classlar� kal�t�m ile verdi�im classda kullanca��m
    internal virtual void Awake()
    {
        //Sahne �zerinden hedefi bulma i�lemi
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Baslangi� De�eri
        Status = EnemyStatus.WALK;
    }

    internal virtual void Update()
    {
        SetState();
    }
    void SetState()
    {
        //Hedefimiz ile Ajan aras�ndaki mesafe'nin hesaplanmas�
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //Mesafeye gore verecegimiz tepki aral�klar�
        if (distanceToPlayer >= 0f && distanceToPlayer <= 3.5f)
            Status = EnemyStatus.FOLLOW;
        else 
            Status = EnemyStatus.WALK;
      
    }
}
