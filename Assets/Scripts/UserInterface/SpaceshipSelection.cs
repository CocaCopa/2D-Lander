using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipSelection : MonoBehaviour
{
    public GameObject shipHolder;
    public float shipSpacing = 6;
    [SerializeField] float UISwipeSpeed = 8;

    Vector3 offset;
    Vector3 targetPosition;

    #region Public:
    public void NextSpaceship() {

        SwipeRight(true);
    }

    public void PreviousSpaceship() {

        SwipeRight(false);
    }

    public void SelectSpaceship() {

        Collider2D ship = Physics2D.OverlapCircle(Vector2.zero, 0.1f);        
        SpaceshipData.m_data = ship.GetComponent<MyData>().SpaceshipData;
        ManageScenes.instance.StartGame();
    }
    #endregion

    #region Private:
    private void Awake() {

        offset = new Vector3(shipSpacing, 0, 0);
    }

    private void Update() {

        shipHolder.transform.position = NextUiPosition();
    }

    private Vector3 NextUiPosition() {

        Vector3 currentPosition = shipHolder.transform.position;
        float lerpTime = UISwipeSpeed * Time.deltaTime;
        return Vector2.Lerp(currentPosition, targetPosition, lerpTime);
    }

    private void SwipeRight(bool swipeRight) {

        float direction = swipeRight ? 1 : -1;
        targetPosition -= direction * offset;

        float minValue = (-shipHolder.transform.childCount + 1) * offset.x;
        float maxValue = 0;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minValue, maxValue);
    }
    #endregion
}
