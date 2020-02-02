using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    ParticleSystem particleSystem;

    bool enabled = false;
    public float poweredFor = 0;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.E)) {
            enabled = true;
        }

        if ((Input.GetKey(KeyCode.Space) && enabled) || poweredFor > 0)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(1, 0), 5);
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
            particleSystem.Stop();
        }

        if (poweredFor > 0) {
            poweredFor -= Time.deltaTime;
        }
    }
}
