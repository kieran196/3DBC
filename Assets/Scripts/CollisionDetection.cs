using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

    public static bool cursorHovered = false;


    void OnTriggerEnter2D(Collider2D collision) {
        //if (cursorHovered == false) {
            this.GetComponent<Image>().color = Color.green;
            //print(this.gameObject.name);
            //cursorHovered = true;
        //}
    }

    void OnTriggerExit2D(Collider2D collision) {
        //if (cursorHovered == true) {
            this.GetComponent<Image>().color = Color.red;
            //cursorHovered = false;
        //}
        
    }
}
