using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform centerTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private PlatesCounter counter;

    private List<Transform> plates;
    private const float PLATE_OFFSET_Y = 0.1f;

    private void Awake()
    {
        plates = new List<Transform>();
    }

    private void Start()
    {
        counter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        counter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateSpawned()
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, centerTopPoint);

        plateVisualTransform.localPosition = new Vector3(0, PLATE_OFFSET_Y * plates.Count, 0);

        plates.Add(plateVisualTransform);
    }

    private void PlatesCounter_OnPlateRemoved()
    {
        Transform plateVisual = plates[plates.Count - 1];
        plates.Remove(plateVisual);
        Destroy(plateVisual.gameObject);
    }
}
