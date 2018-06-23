using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ParticleSystemSpawner : MonoBehaviour {

    public GameObject particleSystemPrefab;
    public int numParticleSystems;
    public Vector2 positionJitter;

    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start () {
        // figure out the rect params
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        float minX = corners[0].x;
        float maxX = minX;
        float minY = corners[0].y;
        float maxY = minY;
        for (int i = 1; i < 4; i++) {
            minX = Mathf.Min(minX, corners[i].x);
            minY = Mathf.Min(minY, corners[i].y);
            maxX = Mathf.Max(maxX, corners[i].x);
            maxY = Mathf.Max(maxY, corners[i].y);
        }
        float width = maxX - minX;
        float height = maxY - minY;

        // evenly space along width with some jitter
        float sectionWidth = width / numParticleSystems;
        float midHeight = (minY + maxY) / 2f;
        for (int i = 0; i < numParticleSystems; i++) {
            Vector2 pos = new Vector2((i + 0.5f) * sectionWidth, midHeight);
            pos.x += Random.Range(-1f, 1f) * positionJitter.x;
            pos.y += Random.Range(-1f, 1f) * positionJitter.y;
            SpawnSystemAt(pos);
        }
	}

    void SpawnSystemAt(Vector2 pos) {
        GameObject system = Instantiate(particleSystemPrefab, pos, Quaternion.Euler(-90f, 0f, 0f));
        system.transform.SetParent(transform);
    }
}
