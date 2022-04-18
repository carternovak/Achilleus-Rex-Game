using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    [SerializeField] Transform _head;
    [SerializeField] Transform _player;
    [SerializeField] Transform _neck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        Vector3 direction = _player.position - transform.position;
        
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       // _head.LookAt(_player);
        _head.transform.rotation = Quaternion.Euler(0f, 0f, angle);    //Transform.Rotate.Vector3(0f, angle, 0f);

        // _head.rotation = Quaternion.Euler(0f, angle, 0f);

    }
}
