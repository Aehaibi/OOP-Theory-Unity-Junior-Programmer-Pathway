using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	//physics
	public float speed;
	public float jump;

	private float m_PushStrength = 15f;
	//ENCAPSULATION
	public float pushStrength
	{
		get { return m_PushStrength; }
		set 
		{
			if (value < 0.0f)
			{
				Debug.LogError("Application of negative strength not allowed.");
			}
			else 
			{
				m_PushStrength = value;
			}
        }
	}
	public bool isOnGround = true;

	public ParticleSystem boostFX;
	public ParticleSystem pickUpFX;
	public ParticleSystem hitVFX;

	public Text countText;
	public int PickUpCount;
	public Text winText;
	public Vector3 scaleChange;
	public bool sizeIncreased = false;
	public int count;

	//powerUp
	public GameObject powerupIndicator;
	public int powerupTimer;
	private bool hasPowerup;
	public float speedBoost;

	private Rigidbody rb;

	//Portal
	public GameObject portalOneSpawn;
	public GameObject portalTwoSpawn;
	public GameObject portalOne;
	public GameObject portalTwo;
	public AudioClip warpSound;

	public AudioSource playerAudio;

	// At the start of the game..
	void Start ()
	{
		sizeIncreased = false;
		PickUpCount = FindObjectsOfType<PickUp>().Length;
		Debug.Log(PickUpCount);
		rb = GetComponent<Rigidbody>();
		count = 0;
		transform.localScale = scaleChange;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";
	}

	// Each physics step..
	void FixedUpdate ()
	{
		Movement();

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Debug.Log("Loading level...");
		}
	}

    void LateUpdate()
    {
		SetCountText();
    }

	//ABSTRACTION
	void Movement() 
	{
		// Set some local float variables equal to the value of our Horizontal and Vertical Inputs
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		if (hasPowerup)
		{
			moveHorizontal *= speedBoost;
			moveVertical *= speedBoost;
		}

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		rb.AddForce(movement * speed);

		if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
		{
			rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
			isOnGround = false;
			// playerAudio.PlayOneShot(jumpSound, 1.0f);
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("objRb"))
		{
			Rigidbody objRb = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

			Debug.Log("Player has collided with " + collision.gameObject.name); //to double check that the collision is working
			objRb.AddForce(awayFromPlayer * pushStrength, ForceMode.Impulse);
			hitVFX.Play();
		}

        if (collision.gameObject.tag == "ScaleIncrease")
        {
            transform.localScale += scaleChange;
            collision.gameObject.SetActive(false);
			sizeIncreased = true;
			pickUpFX.Play();
        }

		//ENCAPSULATION
		if (sizeIncreased)
		{
			if (collision.gameObject.tag == "ScaleDecrease")
			{
				transform.localScale -= scaleChange;
				collision.gameObject.SetActive(false);
				sizeIncreased = false;
				pickUpFX.Play();
                //count = count + 1;
                //SetCountText();
            }
		}
        if (collision.gameObject.tag == "Destructible")
        {
            boostFX.Play();
			hitVFX.Play();
        }

        if (collision.gameObject.tag == "Bumpers")
		{
			boostFX.Play();
		}


        if (collision.gameObject.CompareTag("PortalOne"))
		{
			transform.position = portalTwoSpawn.transform.position;
			Debug.Log("Player stepped through Portal One");
			playerAudio.PlayOneShot(warpSound, 0.75f);
			pickUpFX.Play();
		}

		if (collision.gameObject.CompareTag("PortalTwo"))
		{
			transform.position = portalOneSpawn.transform.position;
			Debug.Log("Player stepped through Portal Two");
			playerAudio.PlayOneShot(warpSound, 0.75f);
		}

		if (collision.gameObject.tag == "death")
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			Debug.Log("Loading level...");
		}

	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
            pickUpFX.Play();
            SetCountText();
		}

		//when player collides with powerup set hasPowerup to true, destroy powerup, powerupIndicator obj is set active
		if (other.CompareTag("Powerup"))
		{
			hasPowerup = true;
			Destroy(other.gameObject);
			powerupIndicator.gameObject.SetActive(true);
			speed *= speedBoost;
			StartCoroutine(PowerupCountdownRoutine());
		}
	}

	//ABSTRACTION
	IEnumerator PowerupCountdownRoutine()
	{
		yield return new WaitForSeconds(powerupTimer);
		hasPowerup = false;
		powerupIndicator.gameObject.SetActive(false);
	}

	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	public void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString();
		Debug.Log(count);

		// Check if our 'count' is equal to or exceeded 12
		if (count >= PickUpCount) 
		{
			// Set the text value of our 'winText'
			winText.text = "LEVEL COMPLETE!";
			Debug.Log("Level Complete");
			winText.color = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, 1));
		}
	}
}