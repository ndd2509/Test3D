using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public Image settingImage;
     public Image bestScoreImg;
    public Text scoreText;
    public float bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreText = PlayerPrefs.GetFloat("MyScore");
        scoreText.text = bestScoreText.ToString();
    }
    
    public void Setting(){
settingImage.gameObject.SetActive(true);
    }
    public void Exit(){
settingImage.gameObject.SetActive(false);
    }

     public void BestScore(){
bestScoreImg.gameObject.SetActive(true);
    }
     public void ExitBestScore(){
bestScoreImg.gameObject.SetActive(false);
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void PlayGame(){
        SceneManager.LoadScene(1);
    }
}
