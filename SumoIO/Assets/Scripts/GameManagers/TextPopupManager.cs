using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPopupManager : MonoBehaviour
{
    /*oyuncunun veya isteðe göre farklý yerlede 
     (puan alýnca veya hasar alýnca) çýkacak yazýlar
     */
    
    private TextMeshPro textMesh;
    private Vector3 target;
    float timer;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        float direction = -0.1f;
        target = transform.position + Quaternion.Euler(0, 0, direction) * new Vector3(-1, 1, 0);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 0.6f) Destroy(gameObject);
        transform.position = Vector3.Lerp(transform.position, target, Mathf.Sin(timer / 0.3f));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / 0.3f));
    }
    public void Setup(float amount)
    {
       
            textMesh.color = Color.yellow;
            textMesh.SetText(amount.ToString());
    }
}
