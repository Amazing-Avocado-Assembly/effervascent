using UnityEngine;

public class Player : MonoBehaviour
{
    public Bubble Bubble { get; private set; }
    public Transform projectilePrefab;
    public float pushForce = 10;
    public float volumePerSecond = 10f;

    private Projectile projectile = null;

    void OnEnable()
    {
        Bubble = GetComponentInChildren<Bubble>();
    }

    void Update()
    {
        // If the player presses the left mouse button, spawn a projectile as a child of the player
        if (Input.GetMouseButtonDown(0))
        {
            if (projectile == null)
            {
                GameObject projectileObject = Instantiate(projectilePrefab.gameObject);
                projectileObject.transform.parent = transform;
                
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition - transform.position;
                direction.z = 0;
                direction.Normalize();

                projectileObject.transform.localPosition = (Bubble.GetRadius() + projectileObject.GetComponent<Bubble>().GetRadius() + 0.05f) * direction;
                
                projectile = projectileObject.GetComponent<Projectile>();
            }
        }
        // If the player holds the left mouse button, transfer volume from self to the bubble
        else if (Input.GetMouseButton(0))
        {
            if (projectile != null)
            {
                Bubble.volume -= Time.deltaTime * volumePerSecond;
                projectile.Bubble.volume += Time.deltaTime * volumePerSecond;

                Vector3 direction = projectile.transform.position - transform.position;
                direction.z = 0;
                direction.Normalize();

                projectile.transform.localPosition = (Bubble.GetRadius() + projectile.Bubble.GetRadius() + 0.05f) * direction;
            }
        }
        // If the player let's go of the mouse button, release the projectile
        else if (Input.GetMouseButtonUp(0))
        {
            if (projectile != null)
            {
                projectile.transform.parent = null;
                projectile.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                projectile.Bubble.ApplyForce(Bubble, pushForce);
                Bubble.ApplyForce(projectile.Bubble, pushForce);

                projectile = null;
            }
        }
    }
}
