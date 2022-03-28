using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum SIDE
{
    Left, Mid , Right
}
// public enum HitX{
//     Left, Mid, Right, None
// }
// public enum HitY{
//     Up, Mid, Down, None
// }
// public enum HitZ{
//     Forward, Mid, Backward, None
// }
public class Character : MonoBehaviour
{
    public SIDE m_side = SIDE.Mid;
    float NewXPos = 0f;
    public bool SwipeLeft;
    public bool SwipeRight;
    public bool SwipeUp, SwipeDown;
    public float XValue;
    private CharacterController m_char;    
    public float forwardSpeed = 5f;
private float jumpForce = 5f;
private float y;
public TrollFollow troll;
private float curDistance;
private Animator m_animator;
private float ColHeight;
private float ColCenterY;
public bool StopAllState = false;
public Image GameOver;
public Text scoreText;
public Text HighScore;
public float score = 0;
public bool Dead = false;
public Rigidbody rbody;
private float normalSpeed;
private float boostedSpeed = 15f;
private float slowSpeed = 2.5f;
// private float LastScore;
// public Text bestScoreText;
 

    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = forwardSpeed;
        m_char = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        // myCollider = GetComponent<CapsuleCollider>();
        rbody = GetComponent<Rigidbody>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        // LastScore = PlayerPrefs. GetFloat("MyScore");
                // transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Score
        scoreText.text = score.ToString();
      
    //   if(score > LastScore){
    //       bestScoreText.text = "Best Score : " + score.ToString();
    //   }
    //   else{
    //       bestScoreText.text = "Your Score : " + score.ToString();
    //   }
        if(Dead == true){
GameOver.gameObject.SetActive(true);
  HighScore.text = "Score : " + score.ToString();

        }



        // troll.curDis = curDistance;
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
 SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
        if(SwipeLeft){
            if(m_side == SIDE.Mid){
                NewXPos = -XValue;
                m_side = SIDE.Left;
            }else if( m_side == SIDE.Right){
                NewXPos = 0;
                m_side = SIDE.Mid;
            }
        }
        else if(SwipeRight){
            if(m_side == SIDE.Mid){
                NewXPos = XValue;
                m_side= SIDE.Right;
            } else if( m_side == SIDE.Left){
                NewXPos = 0;
                m_side = SIDE.Mid;
            }
        }
        // troll.Follow(transform.position,forwardSpeed);    
     Vector3 moveVector = new Vector3(NewXPos - transform.position.x,y * Time.deltaTime, forwardSpeed* Time.deltaTime);    
     m_char.Move(moveVector);
        Jump();
        Slide();
    }

public IEnumerator DeathPlayer(string anim){
Dead = true;
m_animator.SetLayerWeight(1,0);
m_animator.Play(anim);
yield return new WaitForSeconds(0.2f);
}

private void Jump(){
    if(m_char.isGrounded){
        if(SwipeUp){
y = jumpForce;
m_animator.CrossFadeInFixedTime("Jump", 0.1f);
// inJump = true;
        }
        
    }else{
        y-= jumpForce * 2 * Time.deltaTime;
        if(m_char.velocity.y < -0.1f){
            m_animator.Play("Running");
        }
        // inJump = false;
    }
}
internal float SlideCounter;

public void Slide(){
SlideCounter -= Time.deltaTime;
if(SlideCounter <= 0f){
SlideCounter = 0f;
m_char.center = new Vector3(0, ColCenterY, 0);
m_char.height = ColHeight;

}
if(SwipeDown){
    SlideCounter = 0.2f;
    y -= 10f;
    m_char.center = new Vector3(0, ColCenterY/2f, 0);
m_char.height = ColHeight/2f;
m_animator.CrossFadeInFixedTime("Slide", 0.1f);
}
}

private void OnTriggerEnter(Collider other) {
    if(other.tag == "Enemy" || other.tag == "End"){
      StartCoroutine(DeathPlayer("Death"));
       forwardSpeed = 0;
      m_animator.Play("Death");
    //   if(score > LastScore){
    //       PlayerPrefs.SetFloat("MyScore", score);
    //   }
         }
    if(other.tag == "Coin"){
        Destroy(other.gameObject, 0.5f);
        score += 2f;
    }
    if(other.tag == "Boost"){
        forwardSpeed = boostedSpeed;
        Destroy(other.gameObject);
       StartCoroutine("BoostController");
    }
       if(other.tag == "Slow"){
        forwardSpeed = slowSpeed;
        Destroy(other.gameObject);
       StartCoroutine("SlowController");
    }
     if(other.tag == "Win"){
     StartCoroutine(Victory("Win"));
       forwardSpeed = 0;
      m_animator.Play("Win");
    }
    //  if(other.tag == "End"){
    //       StartCoroutine(DeathPlayer("Death"));
    //    forwardSpeed = 0;
    //   m_animator.Play("Death");
    // }
}

public IEnumerator Victory(string anim){
StopAllState = true;
m_animator.SetLayerWeight(1,0);
m_animator.Play(anim);
yield return new WaitForSeconds(3);
SceneManager.LoadScene(2);
}

IEnumerator SlowController(){
    yield return new WaitForSeconds(1);
    forwardSpeed = normalSpeed;
}

IEnumerator BoostController(){
yield return new WaitForSeconds(2);
forwardSpeed = normalSpeed;
}

public void OnCharacterColliderHit(Collider col){
    // hitX = GetHitX(col);
}

public void GotoMenu(){
    SceneManager.LoadScene(0);
}
public void PlayAgain(){
    SceneManager.LoadScene(1);
}


// public HitX GetHitX(Collider col){
// Bounds char_bound = m_char.bounds;
// Bounds col_bound = col.bounds;
// float min_x = Mathf.Max(col_bound.min.x, char_bound.min.x);
// float max_x = Mathf.Min(col_bound.max.x, char_bound.max.x);
// float average = (min_x + max_x) / 2f - col_bound.min.x;
// HitX hit;
// if(average > col_bound.size.x - 0.33f)
// hit = HitX.Right;
// else if(average < 0.33f)
// hit = HitX. Left;
// else
//  hit = HitX.Mid;
//  return hit;
// }

}


