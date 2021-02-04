using System;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<float> onStageTrigger;

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Stage");
        if (other.gameObject.CompareTag("Player"))
            onStageTrigger?.Invoke(transform.position.y);
    }
}
