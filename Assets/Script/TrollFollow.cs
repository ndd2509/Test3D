using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollFollow : MonoBehaviour
{
    public Animator trollAnimator;
    public Transform Troll;
    public float curDis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Follow(Vector3 pos, float speed)
    {
        Vector3 position = pos - Vector3.forward * curDis;
        Troll.position = Vector3.Lerp(Troll.position , position, Time.deltaTime * speed / curDis);
    }
}
