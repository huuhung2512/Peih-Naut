using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //sử dụng camera theo dõi người chơi
    Transform target;
    Vector3 velocity = Vector3.zero;
    // tạo độ trễ khi lia camera
    [Range(0, 1)]
    public float smoothTime;
    public Vector3 possitionOffset;
    
    // giới hạn camera
    public Vector2 xLimit;
    public Vector2 yLimit;

    public Animation anim;
    private void Awake(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animation>();
    }

    private void  LateUpdate()
    { 
        Vector3 targetPosition = target.position + possitionOffset;
        targetPosition = new Vector3(Mathf.Clamp(targetPosition.x,xLimit.x,xLimit.y),Mathf.Clamp(targetPosition.y,yLimit.x,yLimit.y),-10);
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition,ref velocity,smoothTime);

    }
  
}
