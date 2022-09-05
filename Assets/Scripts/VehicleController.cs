/*****************************************************************
 * 코 드 명 : VehicleController.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.27
 * 설    명 : 차량을 컨트롤 한다.
******************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************************************************
 * 설명 : 차량을 컨트롤 하도록 구현한다.
*****************************************************************/
public class VehicleController : MonoBehaviour
{
    [Header("바퀴 Collider")]
    public WheelCollider frontRightWhellCollider;           //오른쪽 앞
    public WheelCollider frontLeftWhellCollider;            //왼쪽 앞
    public WheelCollider BackRightWhellCollider;            //오른쪽 뒤
    public WheelCollider BackLeftWhellCollider;             //왼쪽 뒤

    [Header("바퀴 Transform")]
    public Transform frontRightWhellTransform;
    public Transform frontLeftWhellTransform;
    public Transform BackRightWhellTransform;
    public Transform BackLeftWhellTransform;

    [Header("차 문 Transform")]
    public Transform vehicleDoor;                           //차 문

    [Header("자동차 정보")]
    public float accelForce = 100f;                         //엑셀 힘 (기석도)
    private float currentForce = 0f;                         //현재 엑셀 힘
    public float brakeForce = 200f;                         //브레이크 힘
    private float currentBrakeForce = 0f;                   //현재 브레이크
    public Vector3 centerOfMass;                            //무게중심



    [Header("자동차 회전")]
    public float wheelsTorque = 20f;                        //바퀴 회전력
    private float currentTurnAngle = 0f;                    //현재 회전률

    [Header("자동차 내부")]
    public PlayerScript player;                             //플레이어
    private float radius = 5f;                              //범위
    private bool isOpen = false;                            //문 열렸는지?

    [Header("SetActive가 False가 되어야하는 것")]
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject TPSCamera;
    public GameObject TPSCanvas;
    public GameObject playerCharacter;

    [Header("자동차 Hit")]
    public Camera cam;
    public float hitRange = 2f;          //공격범위
    public float damage = 200f;
    public GameObject bloodEffect;
    //public ParticleSystem hitSpark;          //피격스파크

    public Rigidbody carRigidbody;
    private void Start()
    {
        carRigidbody.centerOfMass = centerOfMass;
    }

    private void FixedUpdate()
    {
        //거리확인
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                isOpen = true;
                radius = 5000f;
                //퀘스트 클리어
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
 * 설명 : 차를 움직이는 로직을 구현한다.
*****************************************************************/
    private void MoveVehicle()
    {
        //4륜 구동..? 2륜구동..? 고민...
        //motorTorque : 바퀴를 회전시키는 힘
        frontLeftWhellCollider.motorTorque = currentForce;
        frontLeftWhellCollider.motorTorque = currentForce;
        BackRightWhellCollider.motorTorque = currentForce;
        BackLeftWhellCollider.motorTorque = currentForce;

        currentForce = accelForce * Input.GetAxis("Vertical");
    }

/****************************************************************
 * 설명 : 차의 핸들을 조종하는 로직을 구현한다.
*****************************************************************/
    private void VehicleSteering()
    {
        //steerAngle 핸들을 좌우로 회전시킴
        currentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");
        frontRightWhellCollider.steerAngle = currentTurnAngle;
        frontLeftWhellCollider.steerAngle = currentTurnAngle;

        //애니메이션
        SteeringWheels(frontRightWhellCollider, frontRightWhellTransform);
        SteeringWheels(frontLeftWhellCollider, frontLeftWhellTransform);
        SteeringWheels(BackRightWhellCollider, BackRightWhellTransform);
        SteeringWheels(BackLeftWhellCollider, BackLeftWhellTransform);

    }


/****************************************************************
 * 설명 : 자동차의 바퀴가 회전하도록 구현한다.
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
 * 설명 : Space Bar를 누르면 브레이크 동작을 하도록 구현한다.
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
        RaycastHit hitInfo;     //맞은놈
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange)) //공격범위 내에 레이케스트 hit되는 것들의 정보
        {
            Debug.Log(hitInfo.transform.name);          //위치는 position.

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
