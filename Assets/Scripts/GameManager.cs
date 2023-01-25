using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform PlayerTransform { get; private set; }
    public bool PlayerLanded { get; set; }
    public bool PlayerDied { get; set; }

    [SerializeField] Text restartText;

    private void Awake() {

        instance = this;

        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerLanded = false;
        PlayerDied = false;
    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.R)) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (PlayerDied) {

            PlayerTransform.gameObject.SetActive(false);
            restartText.gameObject.SetActive(true);
        }
    }
}
