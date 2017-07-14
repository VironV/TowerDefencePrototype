using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour {

    [Header("Techical")]
    public GameObject particles;
    public GameObject additionMesh;

    public void Explode()
    {
        GameObject ptl = Instantiate(particles, transform.position, transform.rotation);
        Destroy(ptl, 6f);

        Destroy(gameObject.GetComponent<Renderer>());
        Destroy(additionMesh);
    }
}
