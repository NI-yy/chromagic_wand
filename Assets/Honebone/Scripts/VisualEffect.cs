using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffect : MonoBehaviour
{
    [SerializeField]
    Vector2 translate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(translate);
    }
    public void Disable()
    {
        Destroy(gameObject);
    }
}
