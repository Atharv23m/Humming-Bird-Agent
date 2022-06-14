using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// forr a specific flower and its nectar
/// </summary>
public class Flower : MonoBehaviour
{
    [Tooltip("Original color")]
    public Color fullflower = new Color(1f , 0f, .3f);
    [Tooltip("Used up color")]
    public Color emptyflower = new Color(0.5f, 0f, 1f);
    /// <summary>
    /// Trigger collider for nectar
    /// </summary>
    [HideInInspector]
    public Collider nectarCollider;

    // normal collider for flower petals
    private Collider flowerCollider;

    // flowers material
    private Material flowerMaterial;
    
    /// <summary>
    /// Vector pointing straight out of flower
    /// </summary>
    public Vector3 FlowerUpVector
    {
        get
        {
            return nectarCollider.transform.up;
        }
    }

    public Vector3 FlowerCenterPosition
    {
        get
        {
            return nectarCollider.transform.position;
        }
    }
    /// <summary>
    /// amount of nectar in flower
    /// </summary>
    public float nectarAmount { get; private set; }

    /// <summary>
    /// whether nectar remaining 
    /// </summary>
    public bool HasNectar
    {
        get
        {
            return nectarAmount > 0;
        }
    }

    /// <summary>
    /// Attempts to remove nectar from the flower
    /// </summary>
    /// <param name="amount"> nectar removed </param>
    /// <returns> The actual amount removed </returns>
    public float Feed( float amount)
    {
        float nectarTaken = Mathf.Clamp(amount, 0f, nectarAmount);

        // subtract nectar
        nectarAmount -= amount;
        if(nectarAmount <=0)
        {
            nectarAmount = 0;

            //disable flower and nectar collider
            flowerCollider.gameObject.SetActive(false);
            nectarCollider.gameObject.SetActive(false);

            //change flower color
            flowerMaterial.SetColor("_BaseColor", emptyflower);
        }
        return nectarTaken;
    }
    /// <summary>
    /// Resets the flower
    /// </summary>
    public void ResetFlower()
    {
        // refill nectar
        nectarAmount = 1f;

        // enable the flower 
        flowerCollider.gameObject.SetActive(true);
        nectarCollider.gameObject.SetActive(true);

        // change the flower color
        flowerMaterial.SetColor("_BaseColor", fullflower);
    }

    private void Awake()
    {
        MeshRenderer meshrenderer = GetComponent<MeshRenderer>();
        flowerMaterial = meshrenderer.material;
        flowerCollider = transform.Find("FlowerCollider").GetComponent<Collider>();
        nectarCollider = transform.Find("FlowerNectarCollider").GetComponent<Collider>();


    }
}