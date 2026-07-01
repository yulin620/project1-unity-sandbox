using UnityEngine;

// Attach this to the "follower" GameObject. Assign the Target in the Inspector.
// Moves the object to maintain a set distance from the target, and logs a
// numeric "goodness" score each frame: 0 = perfect distance, more negative = worse.
public class DistanceFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float desiredDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float tolerance = 0.1f; // dead zone so it doesn't jitter at the target distance

    void Update()
    {
        if (target == null) return;

        Vector3 toTarget = target.position - transform.position;
        float currentDistance = toTarget.magnitude;
        float error = currentDistance - desiredDistance; // >0 too far, <0 too close

        // "Goodness" score: 0 is ideal, gets more negative the further off we are.
        float goodness = -Mathf.Abs(error);
        Debug.Log($"Distance: {currentDistance:F2} | Desired: {desiredDistance:F2} | Goodness: {goodness:F2}");

        if (Mathf.Abs(error) > tolerance)
        {
            Vector3 direction = toTarget.normalized;
            // If too close, move away (negative); if too far, move closer (positive)
            float moveDir = Mathf.Sign(error);
            transform.position += direction * moveDir * moveSpeed * Time.deltaTime;
        }
    }
}
