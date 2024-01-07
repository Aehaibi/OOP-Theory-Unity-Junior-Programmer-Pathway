using UnityEngine;

public class Bumpers : MonoBehaviour
{

    public float bumperStrength;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromBumper = (collision.gameObject.transform.position - transform.position);

            playerRigidbody.AddForce(awayFromBumper * bumperStrength, ForceMode.Impulse);
        }
    }
}
