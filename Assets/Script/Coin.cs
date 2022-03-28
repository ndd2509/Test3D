using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float rotationSpeed = 20f;
    public bool goUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if(goUp == true){
            transform.Rotate(0,0,0);
            transform.Translate(0,0.04f,0);
        }
        else{
            transform.Rotate(0,0,1f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            goUp = true;
        }
    }
}
