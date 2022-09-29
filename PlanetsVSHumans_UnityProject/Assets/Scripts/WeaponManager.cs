using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    NONE = 0,
    ALIEN = 1,
    METEOR = 2,
    LASER = 3
}

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon = Weapon.NONE;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = Weapon.ALIEN;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = Weapon.METEOR;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = Weapon.LASER;
        }
    }

    public void OnAlienClick()
    {
        weapon = Weapon.ALIEN;
    }

    public void OnMeteorClick()
    {
        weapon = Weapon.METEOR;
    }

    public void OnLaserClick()
    {
        weapon = Weapon.LASER;
    }
}