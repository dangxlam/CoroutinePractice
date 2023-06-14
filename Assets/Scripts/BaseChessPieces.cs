using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChessPieces : MonoBehaviour
{
    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    Vector2 location;

    public Vector2 Location { 
        get
        {
            return location;
        } 
        private set
        {
            location = value;
        } 
    }

    private void Start()
    {
        location = transform.position;
        transform.position +=  offsetPosition;
    }
}
