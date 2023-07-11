using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusControl : MonoBehaviour
{
    //Enum yapýlarý ile düþmanlarýn durumlarýný kontrol ediyoruz.
    internal Transform target { get; set; }
    //Enum deðiþkenimizin propertysi
    public EnemyStatus Status { get; set; }
    //Hedef ile Ajan arasý mesafe
    float distanceToPlayer;
    //bu metotlerý classlarý kalýtým ile verdiðim classda kullancaðým
    internal virtual void Awake()
    {
        //Sahne üzerinden hedefi bulma iþlemi
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //Baslangiç Deðeri
        Status = EnemyStatus.WALK;
    }

    internal virtual void Update()
    {
        SetState();
    }
    void SetState()
    {
        //Hedefimiz ile Ajan arasýndaki mesafe'nin hesaplanmasý
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        //Mesafeye gore verecegimiz tepki aralýklarý
        if (distanceToPlayer >= 0f && distanceToPlayer <= 3.5f)
            Status = EnemyStatus.FOLLOW;
        else 
            Status = EnemyStatus.WALK;
      
    }
}
