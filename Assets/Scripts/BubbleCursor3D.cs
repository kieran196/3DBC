using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BubbleCursor3D : MonoBehaviour {

    private GameObject[] circleObjects;
    private float startRadius = 0f;
	public GameObject closestPointer;
    public GameObject closestPointerb;
    public Text cursorRadius;

    // Use this for initialization
    void Start () {
        circleObjects = GameObject.FindGameObjectsWithTag("circleObject");
        startRadius = GetComponent<SphereCollider>().radius;
        this.transform.GetComponent<Renderer>().material.color = Color.blue;


    }

	private float[][] ClosestObject() {
		float lowestDist = 0;
		float[][] allDists = new float[circleObjects.Length][];
        for (int i=0; i<circleObjects.Length; i++) {
            allDists[i] = new float[2];
        }

		int lowestValue = 0;
		for (int i = 0; i < circleObjects.Length; i++) {
            float dist = Vector3.Distance(Input.mousePosition, circleObjects[i].transform.position);
            if (i == 0) {
				lowestDist = dist;
				lowestValue = 0;
			} else {
				if (dist < lowestDist) {
					lowestDist = dist;
					lowestValue = i;
				}
			}
			allDists [i][0] = dist;
			allDists [i][1] = i;
		}
        float[][] arraytest = allDists.OrderBy(row => row[0]).ToArray();
		return arraytest;
	}

    float distance = 10;

    // Update is called once per frame
    void Update () {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        closestPointer.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        closestPointerb.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        float[][] lowestDistances = ClosestObject();
        float ClosestCircleRadius = lowestDistances[0][0] + circleObjects[(int)lowestDistances[0][1]].GetComponent<SphereCollider>().radius;
        float SecondClosestCircleRadius = lowestDistances[1][0] - circleObjects[(int)lowestDistances[1][1]].GetComponent<SphereCollider>().radius;
        float closestValue = Mathf.Min(ClosestCircleRadius, SecondClosestCircleRadius);
        //print("updating");
        print("FIRST:" + ClosestCircleRadius*2);
        print("SECOND:" + SecondClosestCircleRadius*2);
        if (ClosestCircleRadius * 2 < SecondClosestCircleRadius * 2) {
            this.GetComponent<SphereCollider>().radius = closestValue + ClosestCircleRadius;
            print("TARGET:"+lowestDistances[0][1]);
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePos);
                circleObjects[(int)lowestDistances[0][1]].transform.position = objPosition;
            }
        } else {
            this.GetComponent<SphereCollider>().radius = closestValue + SecondClosestCircleRadius;
            print("TARGET:" + lowestDistances[1][1]);
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePos);
                circleObjects[(int)lowestDistances[1][1]].transform.position = objPosition;
            }
        }
    }
}
