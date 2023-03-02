using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool LevelCompleted { get; private set; }
    public bool PlayerDied { get; set; }

    [SerializeField] Text restartText;
    [SerializeField] LandingTrigger landingTrigger;

    private void Awake() {

        instance = this;
        LevelCompleted = false;
        PlayerDied = false;
    }

    private void Update() {

        LevelCompleted = landingTrigger.LandingAccepted;

        if (Input.GetKeyDown(KeyCode.R)) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (PlayerDied) {

            restartText.gameObject.SetActive(true);
        }
    }
}
