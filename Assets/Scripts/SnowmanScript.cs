using UnityEngine;

public class SnowmanScript : MonoBehaviour
{
    private new Rigidbody rigidbody;

    [SerializeField]
    private float force;
    
    private Animation anim;
    
    private GameManager gameManager;
    
    private void Awake()
    {
        var findManager = GameObject.Find("snow_patches");
        gameManager = findManager.GetComponent<GameManager>();

        // anim = gameObject.GetComponent<Animation>();
        //var canvasLives = GameObject.Find("lives");
        //livesTxt = canvasLives.GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        RabbitJump();
    }

    // When instantiated the rabbit moves up
    private void RabbitJump()
    {
        rigidbody.AddForce(0, force, 0, ForceMode.Impulse);
        
        // Play scream animation
        Debug.Log("Scream!!!");
    }

    // When the rabbit hits the bottom it will be destroyed
    private void OnCollisionEnter(Collision col)
    {
        // Destroy rabbit onCollision
        if (!col.gameObject.CompareTag("hitbox")) return;
        Destroy(gameObject);
        gameManager.lives--;
    }
}
