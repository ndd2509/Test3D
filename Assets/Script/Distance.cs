using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    private GameObject player;
    public Text textDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z);
        textDistance.text = "Distance : " + distance.ToString() + " m ";
    }
}
