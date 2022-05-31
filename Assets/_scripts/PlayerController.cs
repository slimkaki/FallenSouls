using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool alive;
    public int deaths;
    private Rigidbody rb;
    private GameObject TpObj;
    private SlackLineController slackLine;
    
    void Start() {
        alive = true;
        deaths = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        TpObj = GameObject.FindGameObjectWithTag("TpObj");
        slackLine = GameObject.FindGameObjectWithTag("slackline").GetComponent<SlackLineController>();
    }

    // Update is called once per frame
    void Update() {
        if(this.transform.position.y < -7f && alive){
            alive = false;
            deaths++;
            StartCoroutine("Reset");
        }
    }

    public bool isAlive() {
        return this.alive;
    }

    public int numberOfDeaths() {
        return this.deaths;
    }

    void respawn(){
        this.transform.position = new Vector3(0, 0.7f, 0);
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<TrackPadWalk>().enabled = false;
        TpObj.SetActive(true);
        slackLine.resetStartedSlackFlag();
        alive = true;
    }

    IEnumerator Reset() {
        yield return new WaitForSeconds(0.5f);
        respawn();
    }
}
