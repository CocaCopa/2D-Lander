using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    private bool hitWall = false;

    private void Update() {

        Debug.Log("Message: 'PlayerCollisionCheck' script is not being used");
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Wall")) {

            hitWall = true;
        }
    }

    public bool PlayerDied {
        get {
            hitWall = false;
            return hitWall;
        }
    }
}
