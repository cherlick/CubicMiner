namespace CastMyWay
{
    using UnityEngine;
    public class RayCastSystem <T>
    {
        public bool _rayCastDebugMode = false;
        

        public T GetObjectDetection(Vector3 startPoint, Vector3 direction, float distance , LayerMask layerMask = default)
        {
            RaycastHit2D hit2D;
            if (_rayCastDebugMode)
            {
                Debug.Log("Debug "+_rayCastDebugMode );
                Debug.DrawRay(startPoint, direction, Color.red, 1);
            }

            hit2D = Physics2D.Raycast(startPoint, direction, distance, layerMask);
            
            if (hit2D) return hit2D.collider.gameObject.GetComponent<T>();

            return default;
        }
    }
}
