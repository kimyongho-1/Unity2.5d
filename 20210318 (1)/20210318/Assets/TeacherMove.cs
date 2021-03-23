using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherMove : MonoBehaviour
{

   [SerializeField] float speed = 2.0f;
   [SerializeField] float rotspeed = 360.0f;
    Coroutine move = null;  //코루틴 초기화
    Animator anim;

    Vector3 Dir = Vector3.zero;// 이동해야하는 방향
    float dist = 0.0f;  //이동해야할 거리

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    
    void Update()
    {
        
    }

    public void MovePosition(Vector3 Pos)
    {
        if (move != null)
            StopCoroutine(move);
        move =   StartCoroutine(Moving(Pos));

    }

    IEnumerator Moving(Vector3 point)
    {
        Dir = point - this.transform.position;
        dist = Dir.magnitude;
        Dir.Normalize();

        float rot = Vector3.Dot(Dir, this.transform.forward); //내적값을 알아야함
        //이동 방향 벡터랑 자신의 앞을 바라보는 벡터의 내적

        rot = Mathf.Acos(rot);  //나오는 값이 라디안값 이걸 다시 오일러 값으로 변경시켜줄거
        rot = (rot * 180.0f) / Mathf.PI;
        float rdir = 1.0f;

        if (Vector3.Dot(this.transform.right, Dir) < 0.0f) //오른쪽 벡터랑 이동방향 내적 계산
        {
            //음수면 왼쪽 반대로 돌아야함
            rdir = -1f;  
        }

        anim.SetBool("Walk",false);
        anim.SetBool("Idle", true);

        //float 근처의 값을 저장함 0.0f = -Mathf.Epsilon ~ +Mathf.Epsilon 가장 작은 오차의 수치
        while (dist > Mathf.Epsilon || rot > Mathf.Epsilon) //0보다는 크단 이야기
        {
            float delta = 0.0f;

            if (dist > Mathf.Epsilon)
            {
                #region move
                //이동해야할 거리가 0보다 크다면
                 delta = speed * Time.deltaTime;

                if (dist - delta > dist)
                {
                    delta = dist;
                }

                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);

                dist -= delta;
                this.transform.Translate(Dir * delta, Space.World);
                #endregion
                if (dist < 0.1f)
                {
                    anim.SetBool("Walk", false);
                    anim.SetBool("Idle", true);

                }
            }


            if (rot > Mathf.Epsilon)
            {
                #region Rotate

                delta = rotspeed * Time.smoothDeltaTime;

                if (rot - delta > rot)
                {
                    delta = rot;
                }

                rot -= delta;
                this.transform.Rotate(this.transform.up * delta * rdir);
                #endregion

            }



            yield return null;
                 
        }
    }

}
//acos(y)  => x라디안값을 알수있따