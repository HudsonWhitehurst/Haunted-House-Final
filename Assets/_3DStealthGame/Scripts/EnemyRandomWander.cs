using UnityEngine;

public class EnemyRoomWander : MonoBehaviour
{
    public float speed = 3f;
    public BoxCollider roomArea;

    private Vector3 targetPoint;

    void Start()
    {
        PickNewPoint();
    }

    void Update()
    {
        // Move toward target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            speed * Time.deltaTime
        );

        // Rotate toward movement direction
        Vector3 dir = targetPoint - transform.position;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2f);
        }

        // If close, pick a new point
        if (Vector3.Distance(transform.position, targetPoint) < 1f)
        {
            PickNewPoint();
        }
    }

    void PickNewPoint()
    {
        Bounds b = roomArea.bounds;

        float x = Random.Range(b.min.x, b.max.x);
        float z = Random.Range(b.min.z, b.max.z);

        targetPoint = new Vector3(x, transform.position.y, z);
    }
}