using UnityEngine;

public class LaserTest : MonoBehaviour
{
    public float laserLength;
    public float laserNormalLength;

    // Update is called once per frame
    void Update()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        RaycastHit2D[] hits = new RaycastHit2D[1];
        if (Physics2D.Raycast(transform.position, transform.right, contactFilter, hits, laserLength) > 0)
        {
            Debug.DrawRay(transform.position, hits[0].point - (Vector2)transform.position, Color.red);
            Debug.DrawRay(hits[0].point, hits[0].normal * laserNormalLength, Color.cyan);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.right * laserLength, Color.blue);
        }
    }
}
