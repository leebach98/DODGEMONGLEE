using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    float moveSpeed = 10f;

    public Transform CharactorBody;
    public Transform User;
    public Transform CameraArm;

    Animator animator;
    void Start()
    {
        animator = CharactorBody.GetComponent<Animator>();
        Debug.Log(animator);
    }

// Update is called once per frame
    void Update()
    {
        Move();
        LookAround();
    }
    void Move()
    {
        //밑에 코드는 키보드의 입력값을 받는다.
        Vector2 moveinput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log(moveinput.magnitude);
        //maggnitude == 거리를 계산 한다 근데 거리를 계산한 값이 0보다 크면 ismove를 트루로 반환한다.
        bool isMove = moveinput.magnitude != 0; //moveinput !=false
        animator.SetBool("isMove", isMove);// ismove는 트루이면 에니메이터의 ismove의 걷는 애니메이션을 실행시킨다.

        //만약 이즈무브라는 변수가 실행된다면
        if (isMove)
        {
            //방향을 정해준거다 방향을 정해주는데, 카메라의 위와 아래
            Vector3 lookForward = new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized;
            //위에거랑 똑같은데 오른 쪽 왼쪽
            Vector3 lookRight = new Vector3(CameraArm.right.x, 0f, CameraArm.right.z).normalized;
            //방향은 카메라의 앞에다가 내 키보드 입력을 받은 y축 대로 움직이면서(이면서는 코드 언어로 +를 의미한다),
            //x축의 키보드 입력도 같이 움직일거다
            Vector3 moveDir = lookForward * moveinput.y + lookRight * moveinput.x;



            User.forward = lookForward;
            //CharactorBody.forward = lookForward;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        //Debug.DrawRay(CameraArm.position, new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized);
    }
    void LookAround()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //rotation을 vector값의 지정하고 싶으면 eulerAngles를 쓴다.
        Vector3 camAngle = CameraArm.rotation.eulerAngles;


        //우리는 목이 180도로 돌아가지 않으니까 제한을 할거야
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }


        //카메라의 회전을 하고싶다. 근데 회전의 변수 타입은 quaternion인데 euler로 계산한다.
        CameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);

    }

}
