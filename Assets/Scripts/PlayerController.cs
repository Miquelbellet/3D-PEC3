using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxVelocityX;
    public float maxVelocityY;
    public GameObject bulletQuadPrefab;

    private Rigidbody rgPlayer;
    private Animator animatorPlayer;

    void Start()
    {
        rgPlayer = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMovement();
        CharacterShoot();
    }

    private void CharacterMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //rgPlayer.velocity += Vector3.forward * 0.2f;
                animatorPlayer.SetBool("walking", false);
                animatorPlayer.SetBool("running", true);
            }
            else
            {
                //rgPlayer.velocity += Vector3.forward * 0.1f;
                animatorPlayer.SetBool("walking", true);
                animatorPlayer.SetBool("running", false);
            }
        }
        if (Input.GetKey(KeyCode.S) && rgPlayer.velocity.z > -maxVelocityX)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rgPlayer.velocity += Vector3.back * 0.2f;
            }
            else
            {
                rgPlayer.velocity += Vector3.back * 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.D) && rgPlayer.velocity.x < maxVelocityX)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rgPlayer.velocity += Vector3.right * 0.2f;
            }
            else
            {
                rgPlayer.velocity += Vector3.right * 0.1f;
            }
        }
        if (Input.GetKey(KeyCode.A) && rgPlayer.velocity.x > -maxVelocityX)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rgPlayer.velocity += Vector3.left * 0.2f;
            }
            else
            {
                rgPlayer.velocity += Vector3.left * 0.1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rgPlayer.velocity += Vector3.up * 8f;
            animatorPlayer.SetBool("jumping", true);
        }
        else
        {
            var currentAnim = animatorPlayer.GetCurrentAnimatorClipInfo(0);
            if (currentAnim[0].clip.name != "jump")
            {
                animatorPlayer.SetBool("jumping", false);
            }
        }
    }

    private void CharacterShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd * 50, Color.green);
            RaycastHit objectHit;
            // Shoot raycast
            if (Physics.Raycast(transform.position, transform.forward, out objectHit, 50))
            {
                GameObject bullet = Instantiate(bulletQuadPrefab,
                            objectHit.point + objectHit.normal * 0.01f,
                            Quaternion.FromToRotation(Vector3.forward, -objectHit.normal)
                        );
                Destroy(bullet, 8f);

                //GameObject ps1 = Instantiate(AK_particleShotPrefab, objectHit.point + objectHit.normal * 0.01f, Quaternion.Euler(0, 0, 0));
                //Destroy(ps1, 1f);
                //GameObject ps2 = Instantiate(AK_particleShotPrefab, gunPoint.transform.position, Quaternion.Euler(0, 0, 0));
                //Destroy(ps2, 1f);
            }
        }
    }
}
