using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MouseController1 : MonoBehaviour {

    private GameObject[] circleObjects;
    private readonly float offSet = 100f;
    public GameObject closestPointer;

    void Start() {
        circleObjects = GameObject.FindGameObjectsWithTag("circleObject");
    }

    private float[] ClosestObject() {
        float[] lowestDists = new float[3];
        lowestDists[0] = 0;
        lowestDists[1] = 0;
        lowestDists[2] = 0;
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
    }

    /*private float ClosestObject() { //Manhattan Distance
        float lowestDist = 0;
        int lowestValue = 0;
        for (int i=0; i<circleObjects.Length; i++) {
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
            print("distance:" + dist + " | object:"+i);
        }
        print("Lowest Value:" + lowestValue);
        return lowestDist;
    }*/

    // Update is called once per frame
    void Update() {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //closestPointer.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //closestPointer.transform.position = new Vector3(this.transform.position.x- GetComponent<RectTransform>().sizeDelta.y, this.transform.position.x - GetComponent<RectTransform>().sizeDelta.y, Input.mousePosition.z);
        //this.transform.position.Scale(new Vector3(1f, 1f));
        float[] lowestDists = ClosestObject();
        //print("LOWEST:" + lowestDists[0]);
        //print("2nd LOWEST:" + lowestDists[1]);
        //float radius = Math.Min(lowestDists[0], lowestDists[1]);
        float anotheradius = lowestDists[0] + lowestDists[1];
        int index = (int)lowestDists[2];
        //print("RADIUS:"+ radius);
        print("CLOSEST:"+lowestDists[0]);
        print("2nd CLOSEST:" + lowestDists[1]);
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(lowestDists[0] + offSet, lowestDists[0] + offSet);
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(anotheradius + offSet, anotheradius + offSet);
        this.transform.localScale = new Vector3((anotheradius / 100f) - 0.5f, (anotheradius / 100f) - 0.5f);
        /*Vector3 vectorToTarget = circleObjects[index].transform.position - closestPointer.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 100f);
        closestPointer.GetComponent<RectTransform>().sizeDelta = new Vector2(lowestDists[0] + offSet, 10f);*/
        //print("pointer size:" + closestPointer.GetComponent<RectTransform>().sizeDelta.x);
        if (Input.GetMouseButtonDown(0)) {
            Vector3 vectorToTarget = circleObjects[index].transform.position - closestPointer.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime*1f);
            closestPointer.transform.LookAt(circleObjects[index].transform);
            //circleObjects[ClosestObject()].GetComponent<Image>().color = Color.red;
        }

    }
}
