using System;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<float> onStageTrigger;
    private BoxCollider2D _collider;
    
    private void OnEnable() {
        _collider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Stage");
        if (other.gameObject.CompareTag("Player")){
            _collider.enabled = false;
            Invoke("EnableCollider", 10f);
            onStageTrigger?.Invoke(transform.position.y);
        }
    
    }

    private void EnableCollider() => _collider.enabled = true;
}
