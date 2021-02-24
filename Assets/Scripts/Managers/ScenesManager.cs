using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour {
    public void LoadScene(int index){
        if(index < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index);
    }

    public void LoadScene(string sceneName){
        if (IsValidScene(sceneName))
            SceneManager.LoadScene(sceneName);
    }

    private bool IsValidScene(string sceneName){
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath(sceneName);
        if (sceneIndex>=0) return true;
        return false;
    }

    public void RestartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
