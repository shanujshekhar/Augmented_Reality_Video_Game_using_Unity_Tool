using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    private GameObject[] planets;
    private GameObject[] spaceships;
    private GameObject[] greenhouses;
    private GameObject[] cargos;

    public void destroyObjects()
    {
        planets = GameObject.FindGameObjectsWithTag("Planet");
        spaceships = GameObject.FindGameObjectsWithTag("Spaceship");
        greenhouses = GameObject.FindGameObjectsWithTag("GreenHouse");
        cargos = GameObject.FindGameObjectsWithTag("cargo");

        foreach (GameObject p in planets)
            Destroy(p);

        foreach (GameObject p in spaceships)
            Destroy(p);

        foreach (GameObject p in greenhouses)
            Destroy(p);

        foreach (GameObject p in cargos)
            Destroy(p);

    }
}
