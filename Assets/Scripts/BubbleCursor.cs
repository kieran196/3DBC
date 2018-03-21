using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BubbleCursor : MonoBehaviour {

    private GameObject[] circleObjects;
    private float startRadius = 0f;
	public GameObject closestPointer;
    public GameObject closestPointerb;
    public Text cursorRadius;

    // Use this for initialization
    void Start () {
        circleObjects = GameObject.FindGameObjectsWithTag("circleObject");
        startRadius = GetComponent<CircleCollider2D>().radius;
        this.transform.GetComponent<Renderer>().material.color = Color.blue;


    }

    /*private float[] ClosestObject() { //Manhattan Distance
        float[] lowestDists = new float[2];
        float lowestDist = 0;
        int lowestValue = 0;
        for (int i = 0; i < circleObjects.Length; i++) {
            float getX = circleObjects[i].transform.position.x;
            float getY = circleObjects[i].transform.position.y;
            float dist = Mathf.Abs(getX - Input.mousePosition.x) + Mathf.Abs(getY - Input.mousePosition.y);
            if (i == 0) {
                lowestDist = dist;
                lowestValue = 0;
            } else {
                if (dist < lowestDist) {
                    lowestDist = dist;
                    lowestValue = i;
                }
            }
            print("distance:" + dist + " | object:" + i);
        }
        print("Lowest Value:" + lowestValue);
        lowestDists[0] = lowestDist;
        lowestDists[1] = lowestValue;
        return lowestDists;
    }*/

	private float[][] ClosestObject() {
		float[] lowestDists = new float[4];
		lowestDists[0] = 0; // 1ST Lowest Distance
		lowestDists[1] = 0; // 2ND Lowest Distance
		lowestDists[2] = 0; // 1ST Lowest Index
		lowestDists [3] = 0; // 2ND Lowest Index
		float lowestDist = 0;
		float[][] allDists = new float[circleObjects.Length][];
        for (int i=0; i<circleObjects.Length; i++) {
            allDists[i] = new float[2];
        }

		int lowestValue = 0;
		for (int i = 0; i < circleObjects.Length; i++) {
			//float getX = circleObjects[i].transform.position.x;
			//float getY = circleObjects[i].transform.position.y;
			//float dist = Mathf.Abs(getX - Input.mousePosition.x) + Mathf.Abs(getY - Input.mousePosition.y);
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

        //allDists.OrderBy(l => (float)l[0]);
        //allDists.OrderBy(y => y[0]);
        /*print("~~~~~~~");
        //Array.Sort(allDists[0]);
        for (int i=0; i< circleObjects.Length; i++)
        {
            print(arraytest[i][0] + " | " + arraytest[i][1]);
        }
        print("~~~~~~~");*/
        /*lowestDists[0] = allDists[0][0];
		if (circleObjects.Length > 1) {
			lowestDists[1] = allDists[1][0];
		}
		lowestDists[2] = allDists[0][1];
		lowestDists [3] = allDists [1][1];*/
		return arraytest;
	}


	/*private float[] ClosestObject() {
		float[] lowestDists = new float[3];
		lowestDists[0] = 0; // 1ST Lowest Distance
		lowestDists[1] = 0; // 2ND Lowest Distance
		lowestDists[2] = 0; // 1ST Lowest Index
		lowestDists [3] = 0; // 2ND Lowest Index
		float lowestDist = 0;
		float[] allDists = new float[circleObjects.Length];

		int lowestValue = 0;
		for (int i = 0; i < circleObjects.Length; i++) {
			float getX = circleObjects[i].transform.position.x;
			float getY = circleObjects[i].transform.position.y;
			float dist = Mathf.Abs(getX - Input.mousePosition.x) + Mathf.Abs(getY - Input.mousePosition.y);
			if (i == 0) {
				lowestDist = dist;
				lowestValue = 0;
			} else {
				if (dist < lowestDist) {
					lowestDist = dist;
					lowestValue = i;
				}
			}
			allDists[i] = dist;
		}
		Array.Sort(allDists);
		lowestDists[0] = allDists[0];
		if (allDists.Length > 1) {
			lowestDists[1] = allDists[1];
		}
		lowestDists[2] = lowestValue;
		return lowestDists;
	}*/

    // Update is called once per frame
    void Update () {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        closestPointer.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        closestPointerb.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        float[][] lowestDistances = ClosestObject();
        //Camera.main.
        //float dist = Vector3.Distance(Input.mousePosition, circleObjects[0].transform.position) * 2 + circleObjects[0].GetComponent<CircleCollider2D>().radius;
        //float dist = Vector3.Distance(Input.mousePosition, circleObjects[(int)lowestDistances[0][1]].transform.position) * 2;
        //print(dist);
        //print(lowestDistances[0][0]);
        float ClosestCircleRadius = lowestDistances[0][0] + circleObjects[(int)lowestDistances[0][1]].GetComponent<CircleCollider2D>().radius;
        float SecondClosestCircleRadius = lowestDistances[1][0] - circleObjects[(int)lowestDistances[1][1]].GetComponent<CircleCollider2D>().radius;
        float closestValue = Mathf.Min(ClosestCircleRadius, SecondClosestCircleRadius);
        //print("updating");
        print("FIRST:" + ClosestCircleRadius*2);
        print("SECOND:" + SecondClosestCircleRadius*2);
        if (ClosestCircleRadius * 2 < SecondClosestCircleRadius * 2) {
            this.GetComponent<CircleCollider2D>().radius = closestValue + ClosestCircleRadius;
            print("TARGET:"+lowestDistances[0][1]);
        } else {
            this.GetComponent<CircleCollider2D>().radius = closestValue + SecondClosestCircleRadius;
            print("TARGET:" + lowestDistances[1][1]);
        }
        //this.transform.localScale = new Vector3(dist / 100f, dist / 100f);

        /*this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		closestPointer.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        float[][] lowestDistances = ClosestObject();
		//Min between lowestDist[0] & lowestDist[1]
		//this.GetComponent<CircleCollider2D>().radius = lowestDistances[0];
        //circleObjects[(int)lowestDistances[1]].GetComponent<Image>().color = Color.yellow;

		print("CLOSEST:"+lowestDistances[0][0] + " | index: " + lowestDistances[0][1]);
		print("2nd CLOSEST:" + lowestDistances[1][0] + " | index: " + lowestDistances[1][1]);
        float ClosestCircleRadius = circleObjects[(int)lowestDistances[0][1]].GetComponent<CircleCollider2D>().radius;
        float SecondClosestCircleRadius = circleObjects[(int)lowestDistances[1][1]].GetComponent<CircleCollider2D>().radius;
        float closestValue = Mathf.Min(lowestDistances[0][0]+ ClosestCircleRadius, lowestDistances[1][0]- SecondClosestCircleRadius);
        this.GetComponent<CircleCollider2D>().radius = closestValue + ClosestCircleRadius;
        cursorRadius.text = "Cursor Radius:" + this.GetComponent<CircleCollider2D>().radius;*/
    }
}
