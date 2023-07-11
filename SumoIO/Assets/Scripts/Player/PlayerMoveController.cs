using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : PlayerControl
{
   //oyuncunun hareket class�
    public float movementSpeed = 3f;     // Hareket h�z�
    [SerializeField] Joystick joystick; //mobil i�in joystick eklentisi
   
 
    private void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        if (Application.isMobilePlatform) // mobil platformda ise 
        {
             moveInput = joystick.Vertical;
             rotateInput = joystick.Horizontal;
        }
        

        Vector3 moveDirection = new Vector3(rotateInput, 0f, moveInput).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Y�r�me i�lemi
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
            anim.SetBool("Walk", true);

            // Y�n�n� de�i�tirme i�lemi
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);
        }
        else { 
            anim.SetBool("Walk", false);
        }
       

    }
   
   

  
   



    
    

}
