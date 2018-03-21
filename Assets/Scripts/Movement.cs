using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 0.5f;
    public readonly float minX = -25f;
    public readonly float maxX = 25f;

    public float smooth = 2.0F;
    public float tiltAngle = 30.0F;
    //public GameObject ballPrefab;

    private void RotateCamera() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            print("Rotating left");
            transform.Rotate(0f, -10f, 0f, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            print("Rotating left");
            transform.Rotate(0f, 10f, 0f, Space.World);
        }
    }

    private void DropBall() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //GameObject ball = Instantiate(ballPrefab, this.transform.position, Quaternion.identity) as GameObject;
            print("Dropped ball");
        }
    }

	private void MoveCamera() {
        if (Input.GetKey(KeyCode.A)) {
            print("Move left");
            this.transform.position += new Vector3(-speed, 0f, 0f);
        } if (Input.GetKey(KeyCode.D)) {
            print("Move right");
            this.transform.position += new Vector3(speed, 0f, 0f);
        } if (Input.GetKey(KeyCode.W)) {
            print("Move right");
            this.transform.position += new Vector3(0f, 0f, speed);
        } if (Input.GetKey(KeyCode.S)) {
            print("Move right");
            this.transform.position += new Vector3(0f, 0f, -speed);
        }

        //Restrict player to stay inside the map
        float newX = Mathf.Clamp(this.transform.position.x, minX, maxX);
        float newZ = Mathf.Clamp(this.transform.position.z, minX, maxX);
        this.transform.position = new Vector3(newX, this.transform.position.y, newZ);
    }
	
	// Update is called once per frame
	void Update () {
        MoveCamera();
        DropBall();
        RotateCamera();
    }
}
