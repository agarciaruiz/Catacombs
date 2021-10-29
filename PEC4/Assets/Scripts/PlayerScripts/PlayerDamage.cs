using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : GenericDamage
{
    [SerializeField] private FloatValue playerDamage;

    // Start is called before the first frame update
    void Start()
    {
        damage = playerDamage.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
