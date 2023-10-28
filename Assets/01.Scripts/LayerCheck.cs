using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private string _whatIsLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag(_whatIsLayer))
        {

        }
    }
}