using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody thisRigidbody;
    public float jumpPower = 10;
    public float jumpInterval = 0.5f;
    private float jumpCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        // update cooldown
        jumpCooldown -= Time.deltaTime;
        bool IsGameActive = GameManager.Instance.IsGameActive();
        bool canJump = jumpCooldown <= 0 && IsGameActive;

        // Jump!
        if (canJump) {
        bool jumpInput = Input.GetKey(KeyCode.Space);
        if(jumpInput) {
                Jump();
            }
         }

         // Toggle gravity
         thisRigidbody.useGravity = IsGameActive;
    }
    void OnCollisionEnter(Collision other) {
        OnCustomCollisionEnter(other.gameObject);
        
    }
    void OnTriggerEnter(Collider other) {
        OnCustomCollisionEnter(other.gameObject);
    }

    private void OnCustomCollisionEnter(GameObject other) {
        bool isSensor = other.CompareTag("Sensor");
        if(isSensor) {
            // Score!
           GameManager.Instance.score++;
            Debug.Log("Score: "+ GameManager.Instance.score);
        } else{
            // Game Over!
            GameManager.Instance.Endgame();
        }


    }

    private void Jump(){
        // Reset cooldown
            jumpCooldown = jumpInterval;

            // Apply force
            thisRigidbody.velocity = new Vector3(0, 0, 0);
            thisRigidbody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
    }

}
