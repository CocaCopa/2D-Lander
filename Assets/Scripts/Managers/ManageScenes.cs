using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    public void StartGame() {

        SceneManager.LoadScene("SampleScene");
    }
}
