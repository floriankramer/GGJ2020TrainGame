using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    ParticleSystem flameParticleSystem;

    bool manualEnabled = false;
    public float poweredFor = 0;

    // Start is called before the first frame update
    void Start()
    {
        flameParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E)) {
            manualEnabled = true;
        }

        if ((Input.GetKey(KeyCode.Space) && manualEnabled) || poweredFor > 0)
        {
            if (!flameParticleSystem.isPlaying)
            {
                flameParticleSystem.Play();
            }

            Vector3 RaycastSrc = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            RaycastHit2D[] hits = Physics2D.RaycastAll(RaycastSrc, new Vector2(1, 0), 5);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.GetComponent<Barricade>() != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }

        }
        else
        {
            flameParticleSystem.Stop();
        }

        if (poweredFor > 0) {
            poweredFor -= Time.deltaTime;
        }
    }
}
