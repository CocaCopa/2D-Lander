using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(SpaceshipSelection))]
public class SpaceshipSelectionEditor : Editor {

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        GUILayout.Space(20);

        if (GUILayout.Button("Apply Ship Spacing")) {

            SpaceOutShips();
        }

        if (GUILayout.Button("Apply Stats to sliders")) {

            SetStatSliders();
        }

        GUILayout.Space(8);

        if (GUILayout.Button("Save Scene")) {

            SaveScene();
        }
    }

    #region GUI Buttons:
    private void SaveScene() {

        string currentScenePath = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), currentScenePath);
    }

    private void SpaceOutShips() {

        SpaceshipSelection spaceshipSelection = FindObjectOfType<SpaceshipSelection>();
        GameObject shipHolder = spaceshipSelection.ShipHolder;
        float shipSpacing = spaceshipSelection.ShipSpacing;
        Vector3 offset = new Vector3 (shipSpacing, 0, 0);
        Vector3 shipPosition = Vector3.zero;

        for (int i = 0; i < shipHolder.transform.childCount; i++) {

            GameObject spaceship = shipHolder.transform.GetChild(i).gameObject;
            spaceship.transform.position = shipPosition;

            shipPosition += offset;
        }
    }

    private void SetStatSliders() {

        SpaceshipSelection spaceshipSelection = FindObjectOfType<SpaceshipSelection>();
        GameObject shipHolder = spaceshipSelection.ShipHolder;

        for (int i = 0; i < shipHolder.transform.childCount; i++) {

            SpaceshipData shipData = shipHolder.transform.GetChild(i).GetComponent<MyData>().GetSpaceshipData;
            GameObject canvas = shipHolder.transform.GetChild(i).GetChild(2).gameObject;

            float[] stats = SetShipStats(shipData);

            for (int j = 0; j < canvas.transform.childCount; j++) {

                float value = stats[j];
                value = Mathf.Clamp01(value);
                Slider slider = canvas.transform.GetChild(j).GetChild(1).GetComponent<Slider>();
                slider.value = value;
            }
        }
    }

    private float[] SetShipStats(SpaceshipData shipData) {

        float[] stats = {
            shipData.healthPoints / shipData.MaxHealthPoints(),
            1 - shipData.fuelConsumptionRate / shipData.fuelCapacity,
            shipData.rotationSpeed / shipData.MaxRotationSpeed(),
            shipData.steerAngle / shipData.MaxSteerAngle(),
            shipData.maximumVeclocity / shipData.MaxVeclocity(),
            shipData.force / shipData.MaxForce(),
        };

        return stats;
    }
    #endregion
}
