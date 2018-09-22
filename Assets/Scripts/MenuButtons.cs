using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    public void NGB(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void NewGameBtn(string newGameLevel)
    {
        // find the scene name
        Scene scene = SceneManager.GetActiveScene();
        string sceneName1 = scene.name+"u1";
        string sceneName2 = scene.name+"u2";
        string sceneName3 = scene.name+"u3";
        string sceneName4 = scene.name+"u4";
        string sceneName5 = scene.name+"u5";
        string sceneName6 = scene.name+"u6";

        Debug.Log("The scene name in buttons is " + sceneName1);
        Debug.Log("The scene name in buttons is " + sceneName2);
        Debug.Log("The scene name in buttons is " + sceneName3);
        Debug.Log("The scene name in buttons is " + sceneName4);
        Debug.Log("The scene name in buttons is " + sceneName5);
        Debug.Log("The scene name in buttons is " + sceneName6);

        // get the customization manager and grab the t1 from it
        string t1 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T1;
        Debug.Log("The t1 in buttons is " + t1);

        string t2 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T2;
        Debug.Log("The t1 in buttons is " + t2);

        string t3 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T3;
        Debug.Log("The t1 in buttons is " + t3);

        string t4 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T4;
        Debug.Log("The t1 in buttons is " + t4);

        string t5 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T5;
        Debug.Log("The t1 in buttons is " + t5);

        string t6 = GameObject.Find("CustomizationManager").GetComponent<Customization>().T6;
        Debug.Log("The t1 in buttons is " + t6);

        // save the data in preferences for this scene
        PlayerPrefs.SetString(sceneName1, t1);
        PlayerPrefs.SetString(sceneName2, t2);
        PlayerPrefs.SetString(sceneName3, t3);
        PlayerPrefs.SetString(sceneName4, t4);
        PlayerPrefs.SetString(sceneName5, t5);
        PlayerPrefs.SetString(sceneName6, t6);
        PlayerPrefs.Save();

        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

}
