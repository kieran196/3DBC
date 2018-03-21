using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollider : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collision) {
        print("Ball Collided");
    }

    void OnTriggerEnter2D(Collider2D collider) {
        print("Ball Trigger");
    }

}
