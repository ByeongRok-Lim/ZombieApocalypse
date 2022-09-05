/*******************************************************************************
 * 코 드 명 : CharacterMovement.cs
 * 작 성 자 : 임 병 록
 * 작 성 일 : 2022.06.21
 * 설    명 : 캐릭터 및 카메라의 이동을 담당한다.
 *******************************************************************************/
/*include될 헤더*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/********************************************************************
 * 설명 : 캐릭터의 이동을 구현한다.
*********************************************************************/
public class CharacterMovement : MonoBehaviour {

	public float speed = 10.0f;						//캐릭터 이동속도

	public float sensitivity = 30.0f;				//회전 감도

	CharacterController character;

	public GameObject cam;
	public bool webGLRightClickRotation = true;
	float gravity = -9.8f;


	void Start(){
		//LockCursor ();
		character = GetComponent<CharacterController> ();
		if (Application.isEditor) {
			webGLRightClickRotation = false;
			sensitivity = sensitivity * 1.5f;
		}
	}




	void Update(){
		float moveFB = Input.GetAxisRaw ("Horizontal") * speed;
		float moveLR = Input.GetAxisRaw ("Vertical") * speed;
		
		float rotX = Input.GetAxisRaw ("Mouse X") * sensitivity;
		float rotY = Input.GetAxisRaw ("Mouse Y") * sensitivity;

		//rotX = Input.GetKey (KeyCode.Joystick1Button4);
		//rotY = Input.GetKey (KeyCode.Joystick1Button5);

		Vector3 movement = new Vector3 (moveFB, gravity, moveLR);



		if (webGLRightClickRotation) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				CameraRotation (cam, rotX, rotY);
			}
		} else if (!webGLRightClickRotation) {
			CameraRotation (cam, rotX, rotY);
		}

		movement = transform.rotation * movement;
		character.Move (movement * Time.deltaTime);
	}


	void CameraRotation(GameObject cam, float rotX, float rotY){		
		transform.Rotate (0, rotX * Time.deltaTime, 0);
		cam.transform.Rotate (-rotY * Time.deltaTime, 0, 0);
	}




}
