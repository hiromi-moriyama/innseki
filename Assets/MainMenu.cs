using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void AdventurePlay()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ChallengePlay()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(0);
    }
}
