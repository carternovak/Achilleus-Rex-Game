using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapDinoToGibs : MonoBehaviour
{
    [SerializeField] private Transform dinoGibsPrefab;
    [SerializeField] private Transform explosionPrefab;

    // Debug to test Explosion, uncomment to test on mousedown
    /*
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Explode();
        }
    }
    */

    public void Explode()
    {        
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Transform dinoGibTransform = Instantiate(dinoGibsPrefab, transform.position, transform.rotation);
        foreach(Transform child in dinoGibTransform)
        {
            if(child.TryGetComponent<Rigidbody>(out Rigidbody childRB))
            {
                childRB.AddExplosionForce(500f, dinoGibTransform.position - new Vector3(0, 1, 0), 5f);
            }
        }
        
        Destroy(gameObject);
        Debug.Log("Dino Gibbed!");
    }
}
