using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpaceshipSelection))]
public class SpaceshipSpacing : Editor {

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        if (GUILayout.Button("Confirm 'ShipSpacing' value")) {

            SpaceshipSelection spaceshipSelection = (SpaceshipSelection)target;
            GameObject shipHolder = spaceshipSelection.shipHolder;

            float spacing = spaceshipSelection.shipSpacing;
            Vector3 offset = new Vector3 (spacing, 0, 0);
            Vector3 shipPosition = Vector3.zero;

            for (int i = 0; i < shipHolder.transform.childCount; i++) {

                GameObject spaceship = shipHolder.transform.GetChild(i).gameObject;
                spaceship.transform.position = shipPosition;

                shipPosition += offset;
            }
        }
    }
}
