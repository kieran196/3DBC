using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class BubbleCursorTEMP : MonoBehaviour {

    private GameObject[] circleObjects;
    private float startRadius = 0f;
	public GameObject closestPointer;
    public Text cursorRadius;

    // Use this for initialization
    void Start () {
        circleObjects = GameObject.FindGameObjectsWithTag("circleObject");
        startRadius = GetComponent<CircleCollider2D>().radius;
        this.transform.GetComponent<Renderer>().material.color = Color.blue;


    }


    // Update is called once per frame
    void Update () {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		closestPointer.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    }
}
