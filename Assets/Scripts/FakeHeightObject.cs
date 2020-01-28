using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FakeHeightObject : MonoBehaviour {

    public UnityEvent onGroundHitEvent;

    public Transform trnsObject;
    public Transform trnsBody;
    public Transform trnsShadow;

    public float gravity = -10;
    public Vector2 groundVelocity;
    public float verticalVelocity;
    private float lastIntialVerticalVelocity;

    public bool isGrounded;

    void Update(){
        UpdatePosition();
        CheckGroundHit();   
    }

    public void Initialize(Vector2 groundVelocity, float verticalVelocity){

        isGrounded = false;
        this.groundVelocity = groundVelocity;
        this.verticalVelocity = verticalVelocity;
        lastIntialVerticalVelocity = verticalVelocity;

    }

    void UpdatePosition(){

        if(!isGrounded){
            verticalVelocity += gravity * Time.deltaTime;
            trnsBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        }

        trnsObject.position += (Vector3)groundVelocity * Time.deltaTime;

    }

    void CheckGroundHit(){

        if(trnsBody.position.y < trnsObject.position.y && !isGrounded){

            trnsBody.position = trnsObject.position;
            isGrounded = true;
            GroundHit();
        }

    }

    void GroundHit(){
        onGroundHitEvent.Invoke();
    }

    public void Stick(){
        groundVelocity = Vector2.zero;
    }

    public void Bounce(float divisionFactor){
        Initialize(groundVelocity, lastIntialVerticalVelocity / divisionFactor);
    }
    
    public void SlowDownGroundVelocity(float divisionFactor){
        groundVelocity = groundVelocity / divisionFactor;
    }

    public void Destroy(float timeToDestruction){

        Destroy(gameObject, timeToDestruction);

    }



}