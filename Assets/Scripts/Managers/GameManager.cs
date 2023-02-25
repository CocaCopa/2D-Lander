using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool PlayerLanded { get; set; }
    public bool PlayerDied { get; set; }

    [SerializeField] Text restartText;

    private void Awake() {

        instance = this;
        PlayerLanded = false;
        PlayerDied = false;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.R)) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (PlayerDied) {

            restartText.gameObject.SetActive(true);
        }
    }
}
