using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /*OYUNCU controlleri
      oyuncu yumruk atarak di�er d��manlar� ittirdi�i bir mekanik d���nd�m
      bunun i�in ileriye do�ru bir ray g�ndererek �arpan d��mananlara kuvvet uygulad�m.*/
    [SerializeField] Transform rayOrigin;
    [SerializeField]
    [Range(0, 50)] float rayDistance;
    internal Animator anim;
    float forceMultiplier = 4f; // �tme kuvveti �arpan�, ba�lang�� de�eri olarak 1
    [SerializeField]
    GameObject punchEffect;
    private GameObject enemy;

    [SerializeField] GameObject textPopup;
    [SerializeField] GameObject powerEffect;//g�� toplama effeckti

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
    public void Punch()//Bu k�sm� animasyon event olarak tam yumru�u att��� saniyede �a��rd�m 
    {//daha g�zel bir g�r�nt� a��s�ndan ve s�rekli rayi �al��t�rmas�n� engelmek i�in
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Enemy")) // K�p etiketine sahip nesneyle �arp���ld� m� kontrol ediliyor
            {
                Vector3 hitPoint = hit.point;
                AudioManager.instance.Play("punch");
                Vector3 reverseDirection = (transform.position - hitPoint).normalized;
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                Instantiate(punchEffect, hitPoint, Quaternion.identity);
                if (rb != null)
                {
                    rb.AddForce(-reverseDirection * forceMultiplier, ForceMode.Impulse); // �tme kuvvetini uygula
                    hit.collider.GetComponent<Enemy>().pushControl = true;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);

        }
        else
        {
            // Ray hi�bir �eye �arpmad�ysa, s�n�rl� bir mesafeye kadar �izgi �izme i�lemi
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 1f);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Power"))
        {
            TextPopupManager damagePopup = Instantiate(textPopup, transform.position, Quaternion.identity).GetComponent<TextPopupManager>();
            damagePopup.Setup(1000);//yaz� olarak ��kmas�n� istedi�im de�er
            Instantiate(powerEffect, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            forceMultiplier += 1.5f;
            transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
            Camera.main.GetComponent<CameraFollow>().followHeight += 1f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //d��mana yak�n bir a��daysa
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject;
            //d��manlar�n savunma g�c�n� oyuncunun itme g���nden ��kararak 
            //d��mana savunma kazand�r�yorum
            forceMultiplier += other.gameObject.GetComponent<Enemy>().Durability;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = null;
            //bu de�eri daha sonra oyuncunun iteme g���nden ��kartarak 
            //oyuncunun normal g�ce d�nemsini sa�l�yorum
            forceMultiplier -= other.gameObject.GetComponent<Enemy>().Durability;

        }
    }

   


}
