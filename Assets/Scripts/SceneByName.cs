using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneByName : MonoBehaviour {

    public string sceneName = "";

    //This is Main Camera in the Scene    
    //public Camera m_MainCamera;
    //public GameObject myCamera;
    //public Camera myCamera;
    bool m_SceneLoaded = false;

    // Use this for initialization
    void Start () {

        
        // This gets the Main Camera from the Scene
        //myCamera =  Camera.main;
        //myCamera = m_MainCamera.GetComponent<Camera>();
        //myCamera = GameObject.FindWithTag("CameraSkene1");
        //m_MainCamera = myCamera.GetComponent<Camera>();
        // m_MainCamera = GameObject.FindWithTag("CameraSkene1").GetComponent<Camera>();

    }
	
	public void ReloadScene()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene()
    {
        // lataa skene
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSingle(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        m_SceneLoaded = true;

        Invoke("SetSceneActive", 1);
    }

    public void LoadAsync(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));

        SetSceneActive();
    }

    public void LoadAdditive(string sceneName)
    {

        return;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        m_SceneLoaded = true;

       Invoke("SetSceneActive", 1);
    }

    public void LoadAsyncAdditive(string name)
    {
        //This disables Main Camera
        //Debug.Log("DISABLOIDAAN KAMERA!");
        //m_MainCamera.enabled = false;
        //m_MainCamera.targetDisplay = 4;
        //myCamera.enabled = false;

        m_SceneLoaded = false;

        // lataa skene
        StartCoroutine(LoadYourAsyncScene(name));

        SetSceneActive();
    }



    public void SetSceneActive()
    {
        sceneName =  SceneManager.GetActiveScene().name;
      //  Debug.Log("m_SceneLoaded: " + m_SceneLoaded);

        if (m_SceneLoaded == true)
        {
            Debug.Log("m_SceneLoaded: " + m_SceneLoaded);
            // Set Scene2 as the active Scene
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            // Ouput the same of the active Scene
            // See now that the name is updated
           // Debug.Log("NO NYT ON!!! : " + SceneManager.GetActiveScene().name);
        }
        //Debug.Log("Active Scene : " + SceneManager.GetActiveScene().name);
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    IEnumerator LoadYourAsyncScene(string name)
    {
       
       
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        //Debug.Log("DISABLOIDAAN KAMERA!");
        //m_MainCamera.enabled = false;
        //m_MainCamera.targetDisplay = 4;
        //myCamera.enabled = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        m_SceneLoaded = true;
        Debug.Log("LATAUS SUORITETTU! " + m_SceneLoaded);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }

    IEnumerator LoadAsyncScene(string name)
    {


        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, UnityEngine.SceneManagement.LoadSceneMode.Single);

        //Debug.Log("DISABLOIDAAN KAMERA!");
        //m_MainCamera.enabled = false;
        //m_MainCamera.targetDisplay = 4;
        //myCamera.enabled = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        m_SceneLoaded = true;
        Debug.Log("LATAUS SUORITETTU! " + m_SceneLoaded);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }



}
