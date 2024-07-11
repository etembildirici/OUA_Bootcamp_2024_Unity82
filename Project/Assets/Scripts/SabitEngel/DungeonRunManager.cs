using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRunManager : MonoBehaviour
{
    public GameObject[] roadSegments; // Farklı yol segmentleri
    public Transform roadParent; // Yol segmentlerinin ekleneceği parent

    private float segmentLength = 10f; // Yol segmentinin uzunluğu

    private void Start()
    {
        for (int i = 0; i < 5; i++) // İlk 5 segmenti ekleyerek başlat
        {
            AddSegment();
        }
    }

    private void AddSegment()
    {
        int randomIndex = Random.Range(0, roadSegments.Length);
        GameObject segment = Instantiate(roadSegments[randomIndex], roadParent);
        segment.transform.position = new Vector3(0, 0, roadParent.childCount * segmentLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddSegment();
        }
    }
}

