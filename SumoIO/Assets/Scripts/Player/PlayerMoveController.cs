using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : PlayerControl
{
   //oyuncunun hareket classý
    public float movementSpeed = 3f;     // Hareket hýzý
    [SerializeField] Joystick joystick; //mobil için joystick eklentisi
   
 
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
            // Yürüme iþlemi
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
            anim.SetBool("Walk", true);

            // Yönünü deðiþtirme iþlemi
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360f);
        }
        else { 
            anim.SetBool("Walk", false);
        }
       

    }
   
   

  
   



    
    

}
