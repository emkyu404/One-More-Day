using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    [Header("Camera threshold")]
    [SerializeField] Transform player;
    [SerializeField] float threshold;
    [SerializeField] PlayerController playerController;
    [SerializeField] float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = (player.position + playerController.GetMouseToWorldPosition()) / 2f;
        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold + player.position.x);
        targetPos.z = Mathf.Clamp(targetPos.z, -threshold + player.position.z, threshold + player.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
