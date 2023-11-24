using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public void Mainmenu()
    {
        SceneManager.LoadScene(1);
    }
    public void StageSelect()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Stage1()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void ChallengePlay()
    {
        SceneManager.LoadSceneAsync(4);
    }

    
}
