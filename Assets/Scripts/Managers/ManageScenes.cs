using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public static ManageScenes instance;

    [Header("--- Scene Names ---")]
    [SerializeField] string testScene;
    [SerializeField] string selectSpaceship;

    private void Awake() {

        DontDestroy();
    }

    public void StartGame() {

        SceneManager.LoadScene(testScene);
    }

    public void SpaceshipSelection() {

        SceneManager.LoadScene(selectSpaceship);
    }

    public void ReloadLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DontDestroy() {

        if (instance == null) {

            instance = this;
            DontDestroyOnLoad(this);
        }
        else {

            Destroy(this);
        }
    }
}