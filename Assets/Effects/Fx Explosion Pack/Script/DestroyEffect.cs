using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

    void Start()
    {
        // Destroy the effect after a few seconds
        Destroy(gameObject,2);
    }
}
