using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ParticleSystemSpawner : MonoBehaviour {

    public GameObject particleSystemPrefab;
    public int numParticleSystems;

    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start () {
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
        for (int i = 0; i < numParticleSystems; i++) {
            Vector2 pos = ChoosePointInRect(minX, minY, maxX, maxY);
            SpawnSystemAt(pos);
        }
	}

    Vector2 ChoosePointInRect(float minX, float minY, float maxX, float maxY) {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        return new Vector2(x, y);
    }

    void SpawnSystemAt(Vector2 pos) {
        GameObject system = Instantiate(particleSystemPrefab, pos, Quaternion.Euler(-90f, 0f, 0f));
        system.transform.SetParent(transform);
    }
}
