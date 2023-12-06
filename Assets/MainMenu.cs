using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string Stage_1;
    public string Stage_2;
    public string Stage_3;
    public string Challenge;
    public string StageSelect;


    public void Mainmenu()
    {
        SceneManager.LoadScene(1);
    }
    public void Stage()
    {
        SceneManager.LoadSceneAsync(StageSelect);
    }

    public void Stage1()
    {
        SceneManager.LoadScene(Stage_1);
    }

    public void Stage2()
    {
        if (PlayerAttributes.stage1 == true)
        {
            SceneManager.LoadScene(Stage_2);
        }
    }

    public void Stage3()
    {
        if (PlayerAttributes.stage2 == true)
        {
            SceneManager.LoadScene(Stage_3);
        }
    }
    public void ChallengePlay()
    {
        if(PlayerAttributes.stage1 == true & PlayerAttributes.stage2 == true & PlayerAttributes.stage3 == true)
        SceneManager.LoadScene(Challenge);
    }

    
}
