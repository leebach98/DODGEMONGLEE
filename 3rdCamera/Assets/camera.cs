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
        //�ؿ� �ڵ�� Ű������ �Է°��� �޴´�.
        Vector2 moveinput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Debug.Log(moveinput.magnitude);
        //maggnitude == �Ÿ��� ��� �Ѵ� �ٵ� �Ÿ��� ����� ���� 0���� ũ�� ismove�� Ʈ��� ��ȯ�Ѵ�.
        bool isMove = moveinput.magnitude != 0; //moveinput !=false
        animator.SetBool("isMove", isMove);// ismove�� Ʈ���̸� ���ϸ������� ismove�� �ȴ� �ִϸ��̼��� �����Ų��.

        //���� ������� ������ ����ȴٸ�
        if (isMove)
        {
            //������ �����ذŴ� ������ �����ִµ�, ī�޶��� ���� �Ʒ�
            Vector3 lookForward = new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized;
            //�����Ŷ� �Ȱ����� ���� �� ����
            Vector3 lookRight = new Vector3(CameraArm.right.x, 0f, CameraArm.right.z).normalized;
            //������ ī�޶��� �տ��ٰ� �� Ű���� �Է��� ���� y�� ��� �����̸鼭(�̸鼭�� �ڵ� ���� +�� �ǹ��Ѵ�),
            //x���� Ű���� �Էµ� ���� �����ϰŴ�
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
        //rotation�� vector���� �����ϰ� ������ eulerAngles�� ����.
        Vector3 camAngle = CameraArm.rotation.eulerAngles;


        //�츮�� ���� 180���� ���ư��� �����ϱ� ������ �Ұž�
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }


        //ī�޶��� ȸ���� �ϰ�ʹ�. �ٵ� ȸ���� ���� Ÿ���� quaternion�ε� euler�� ����Ѵ�.
        CameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);

    }

}
