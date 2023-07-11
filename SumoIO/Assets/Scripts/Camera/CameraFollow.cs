using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followHeight { get; set; } = 9;
    [SerializeField] float followDistance = 6f;
    [SerializeField] float followHeightSpeed = 0.9f;

    Transform player;

    float targetHeight;
    float currentHeight;
    float currentRotation;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        targetHeight = player.position.y + followHeight;
        currentRotation = transform.eulerAngles.y;
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, followHeightSpeed * Time.deltaTime);
        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);
        Vector3 targetPosition = player.position - (euler * Vector3.forward) * followDistance;
        targetPosition.y = currentHeight;
        transform.position = targetPosition;
        transform.LookAt(player);

    }
}
