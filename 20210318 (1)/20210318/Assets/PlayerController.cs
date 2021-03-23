using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TeacherMove))]  //무조건 포함
public class PlayerController : MonoBehaviour
{

    TeacherMove myMovement;

    private void Awake()
    {
        myMovement = this.GetComponent<TeacherMove>();
    }

    //피킹 처리  마우스 왼쪽처리했을떄 해당지역으로 움직이는 기법
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //월드로 무한히 뻗어나가는 직사광선

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                myMovement.MovePosition(hit.point);
            }

        }
    }
}
