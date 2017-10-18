using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    #region variables
    public Shooting shooting;
    FiringType firingType;
    #endregion

    void Start()
    {
        shooting = FindObjectOfType<Shooting>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            shooting.Fire();

        }
    }

       


}
