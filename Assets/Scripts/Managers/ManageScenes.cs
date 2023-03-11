using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    [SerializeField] string tempScene;
    [SerializeField] string selectSpaceship;

    public void StartGame() {

        SceneManager.LoadScene(tempScene);
    }

    public void SpaceshipSelection() {

        SceneManager.LoadScene(selectSpaceship);
    }
}