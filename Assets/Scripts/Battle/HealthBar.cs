using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject health;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void UpdateHealth(float hp)
    {
        health.transform.localScale = new Vector3(hp, 1f);
    }
}
