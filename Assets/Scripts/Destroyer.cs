﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("destroyer"))
        {
            Destroy(coll.gameObject);
        }
    }
}