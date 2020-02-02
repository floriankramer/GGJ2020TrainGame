using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : MonoBehaviour
{

    ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
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
    }
}
