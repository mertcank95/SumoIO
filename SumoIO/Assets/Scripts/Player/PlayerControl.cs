using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /*OYUNCU controlleri
      oyuncu yumruk atarak diðer düþmanlarý ittirdiði bir mekanik düþündüm
      bunun için ileriye doðru bir ray göndererek çarpan düþmananlara kuvvet uyguladým.*/
    [SerializeField] Transform rayOrigin;
    [SerializeField]
    [Range(0, 50)] float rayDistance;
    internal Animator anim;
    float forceMultiplier = 4f; // Ýtme kuvveti çarpaný, baþlangýç deðeri olarak 1
    [SerializeField]
    GameObject punchEffect;
    private GameObject enemy;

    [SerializeField] GameObject textPopup;
    [SerializeField] GameObject powerEffect;//güç toplama effeckti

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {

        if (enemy is not null)
        {
            anim.SetTrigger("Punch");
        }
    }
    public void Punch()//Bu kýsmý animasyon event olarak tam yumruðu attýðý saniyede çaðýrdým 
    {//daha güzel bir görüntü açýsýndan ve sürekli rayi çalýþtýrmasýný engelmek için
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Enemy")) // Küp etiketine sahip nesneyle çarpýþýldý mý kontrol ediliyor
            {
                Vector3 hitPoint = hit.point;
                AudioManager.instance.Play("punch");
                Vector3 reverseDirection = (transform.position - hitPoint).normalized;
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                Instantiate(punchEffect, hitPoint, Quaternion.identity);
                if (rb != null)
                {
                    rb.AddForce(-reverseDirection * forceMultiplier, ForceMode.Impulse); // Ýtme kuvvetini uygula
                    hit.collider.GetComponent<Enemy>().pushControl = true;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);

        }
        else
        {
            // Ray hiçbir þeye çarpmadýysa, sýnýrlý bir mesafeye kadar çizgi çizme iþlemi
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 1f);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power"))
        {
            TextPopupManager damagePopup = Instantiate(textPopup, transform.position, Quaternion.identity).GetComponent<TextPopupManager>();
            damagePopup.Setup(1000);//yazý olarak çýkmasýný istediðim deðer
            Instantiate(powerEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            forceMultiplier += 1.5f;
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            Camera.main.GetComponent<CameraFollow>().followHeight += 1f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //düþmana yakýn bir açýdaysa
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject;
            //düþmanlarýn savunma gücünü oyuncunun itme güçünden çýkararak 
            //düþmana savunma kazandýrýyorum
            forceMultiplier += other.gameObject.GetComponent<Enemy>().Durability;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = null;
            //bu deðeri daha sonra oyuncunun iteme güçünden çýkartarak 
            //oyuncunun normal güce dönemsini saðlýyorum
            forceMultiplier -= other.gameObject.GetComponent<Enemy>().Durability;

        }
    }

   


}
