using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move : MonoBehaviour
{
    [SerializeField][Header("스피드")] float speed;
    [SerializeField][Header("카메라")] Camera cam;

    Rigidbody rb;
    Animator anim;
    Vector3 target; //목표지점
    bool isMove;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        
    }

    
    void Update()
    {

        Mouse();
      
    }
    private void SetDestination(Vector3 des)
    {
        target = des;
        isMove = true;
    }
    void MoveChr()
    {
        if (isMove)//트루라면
        {
            if (Vector3.Distance(target, transform.position) <= 0.1f)
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Idle", true);
                isMove = false;
                return;
                
            }

            var dir = target - transform.position;
            //마우스 위치 방향

            transform.position += dir.normalized * Time.deltaTime * speed;
            anim.SetBool("Idle", false);
            anim.SetBool("Walk", true);

            

        }



    }

    void Mouse()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
            {
                SetDestination(hit.point);
            }

        }

        MoveChr(); 
        Rot();

    }

    void Rot()
    {
        transform.LookAt(target);
        
    }
}
