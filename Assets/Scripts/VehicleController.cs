/*****************************************************************
 * �� �� �� : VehicleController.cs
 * �� �� �� : �� �� ��
 * �� �� �� : 2022.06.27
 * ��    �� : ������ ��Ʈ�� �Ѵ�.
******************************************************************/
/*include�� ���*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * ���� : ������ ��Ʈ�� �ϵ��� �����Ѵ�.
*****************************************************************/
public class VehicleController : MonoBehaviour
{
    [Header("���� Collider")]
    public WheelCollider frontRightWhellCollider;           //������ ��
    public WheelCollider frontLeftWhellCollider;            //���� ��
    public WheelCollider BackRightWhellCollider;            //������ ��
    public WheelCollider BackLeftWhellCollider;             //���� ��

    [Header("���� Transform")]
    public Transform frontRightWhellTransform;
    public Transform frontLeftWhellTransform;
    public Transform BackRightWhellTransform;
    public Transform BackLeftWhellTransform;

    [Header("�� �� Transform")]
    public Transform vehicleDoor;                           //�� ��

    [Header("�ڵ��� ����")]
    public float accelForce = 100f;                         //���� �� (�⼮��)
    private float currentForce = 0f;                         //���� ���� ��
    public float brakeForce = 200f;                         //�극��ũ ��
    private float currentBrakeForce = 0f;                   //���� �극��ũ
    public Vector3 centerOfMass;                            //�����߽�



    [Header("�ڵ��� ȸ��")]
    public float wheelsTorque = 20f;                        //���� ȸ����
    private float currentTurnAngle = 0f;                    //���� ȸ����

    [Header("�ڵ��� ����")]
    public PlayerScript player;                             //�÷��̾�
    private float radius = 5f;                              //����
    private bool isOpen = false;                            //�� ���ȴ���?

    [Header("SetActive�� False�� �Ǿ���ϴ� ��")]
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject TPSCamera;
    public GameObject TPSCanvas;
    public GameObject playerCharacter;

    [Header("�ڵ��� Hit")]
    public Camera cam;
    public float hitRange = 2f;          //���ݹ���
    public float damage = 200f;
    public GameObject bloodEffect;
    //public ParticleSystem hitSpark;          //�ǰݽ���ũ

    public Rigidbody carRigidbody;
    private void Start()
    {
        carRigidbody.centerOfMass = centerOfMass;
    }

    private void FixedUpdate()
    {
        //�Ÿ�Ȯ��
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
                radius = 5000f;
                //����Ʈ Ŭ����
                ObjectivesComplete.occurrence.GetObjectives(true, false, false, false, false);


                aimCam.SetActive(false);
                aimCanvas.SetActive(false);
                TPSCamera.SetActive(false);
                TPSCanvas.SetActive(false);

            }
            else if(Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpen = false;
                radius = 5f;
            }
        }

        if(isOpen)
        {
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
            TPSCamera.SetActive(false);
            TPSCanvas.SetActive(false);
            playerCharacter.SetActive(false);

            MoveVehicle();
            VehicleSteering();
            VehicleBrake();
            HitZombies();
        }
        else if(!isOpen)
        {
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
            TPSCamera.SetActive(true);
            TPSCanvas.SetActive(true);
            playerCharacter.SetActive(true);
        }
        
    }

/****************************************************************
 * ���� : ���� �����̴� ������ �����Ѵ�.
*****************************************************************/
    private void MoveVehicle()
    {
        //4�� ����..? 2������..? ���...
        //motorTorque : ������ ȸ����Ű�� ��
        frontLeftWhellCollider.motorTorque = currentForce;
        frontLeftWhellCollider.motorTorque = currentForce;
        BackRightWhellCollider.motorTorque = currentForce;
        BackLeftWhellCollider.motorTorque = currentForce;

        currentForce = accelForce * Input.GetAxis("Vertical");
    }

/****************************************************************
 * ���� : ���� �ڵ��� �����ϴ� ������ �����Ѵ�.
*****************************************************************/
    private void VehicleSteering()
    {
        //steerAngle �ڵ��� �¿�� ȸ����Ŵ
        currentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWhellCollider.steerAngle = currentTurnAngle;
        frontLeftWhellCollider.steerAngle = currentTurnAngle;

        //�ִϸ��̼�
        SteeringWheels(frontRightWhellCollider, frontRightWhellTransform);
        SteeringWheels(frontLeftWhellCollider, frontLeftWhellTransform);
        SteeringWheels(BackRightWhellCollider, BackRightWhellTransform);
        SteeringWheels(BackLeftWhellCollider, BackLeftWhellTransform);

    }


/****************************************************************
 * ���� : �ڵ����� ������ ȸ���ϵ��� �����Ѵ�.
*****************************************************************/
    private void SteeringWheels(WheelCollider wc, Transform wt)
    {
        Vector3 position;
        Quaternion rotation;

        wc.GetWorldPose(out position, out rotation);

        wt.position = position;
        wt.rotation = rotation;
    }

/****************************************************************
 * ���� : Space Bar�� ������ �극��ũ ������ �ϵ��� �����Ѵ�.
*****************************************************************/
    void VehicleBrake()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakeForce;
        }
        else
        {
            currentBrakeForce = 0f;
        }
        frontRightWhellCollider.brakeTorque = currentBrakeForce;
        frontLeftWhellCollider.brakeTorque = currentBrakeForce;
        BackRightWhellCollider.brakeTorque = currentBrakeForce;
        BackLeftWhellCollider.brakeTorque = currentBrakeForce;
    }

    void HitZombies()
    {
        RaycastHit hitInfo;     //������
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange)) //���ݹ��� ���� �����ɽ�Ʈ hit�Ǵ� �͵��� ����
        {
            Debug.Log(hitInfo.transform.name);          //��ġ�� position.

            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (zombie1 != null)
            {
                zombie1.ZombieHitDamage(damage);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject bloodEffectGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.ZombieHitDamage(damage);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                GameObject bloodEffectGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodEffectGo, 1f);
            }
        }
    }
}
