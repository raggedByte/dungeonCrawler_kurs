using System;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    public Transform targetTransform;
    public bool lerping;

    [Range(0f, 100f)] public float speed;
    private void FixedUpdate()
    {
        var targetPosition = targetTransform.position;
        var position = transform.position;
        var convertedTargetPosition = new Vector3(targetPosition.x, targetPosition.y, position.z);
        
        transform.position = lerping
            ? Vector3.Lerp(position, convertedTargetPosition, Time.fixedDeltaTime * speed)
            : convertedTargetPosition;
    }
}