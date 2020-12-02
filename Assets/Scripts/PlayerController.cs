using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject pistolObj;
    public GameObject bulletQuadPrefab;

    private Animator animPLayer;

    void Start()
    {
        animPLayer = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterShoot();
    }

    private void CharacterShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(transform.position, fwd * 50, Color.green);
            RaycastHit objectHit;
            // Shoot raycast
            if (Physics.Raycast(pistolObj.transform.position, transform.forward, out objectHit, 50))
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
