using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public int sceneNo;

    void Awake()
    {
       
    }

    public void LoadSceneNr()
    {
        SceneManager.LoadScene(sceneNo);
    }

        public void LoadSceneOnClick(int sceneNo)
    {
        
        SceneManager.LoadScene(sceneNo);
    }

}