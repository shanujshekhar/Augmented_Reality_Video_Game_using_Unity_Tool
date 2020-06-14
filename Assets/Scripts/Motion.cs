using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private GameObject[] spaceships;
    private GameObject[] planets;
    private GameObject plane;
    private GameObject cargo;
    private GameObject cargodroploc;
    private float a = 3;
    private float b = 1;
    private float alpha = 0;
    private bool reached = false;
    private bool initialpos = false;
    private Vector3[] pos;
    private bool reverse = false;
    private Vector3 direction;
    private bool done = true;
    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.Find("ImageTarget").transform.GetChild(0).gameObject;
        cargo = GameObject.Find("ScriptHolder").transform.GetComponent<DuplicateObjects>().Duplicate;
        direction = plane.transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (reached)
        {
            objectMotion();
        }
    }

    public void StopMotion()
    {
        reached = false;
    }

    public void objectMotion()
    {
        reached = true;

        planets = GameObject.FindGameObjectsWithTag("Planet");
        spaceships = GameObject.FindGameObjectsWithTag("Spaceship");


        if (planets!=null && planets.Length != 0)
        {
            foreach (GameObject p in planets)
            {
                p.transform.RotateAround(plane.transform.up, 1 * Time.deltaTime);
                p.transform.RotateAround(plane.transform.position, plane.transform.up, 5 * Time.deltaTime);
            }
        }

        if (spaceships!=null && spaceships.Length != 0)
        {
            foreach (GameObject s in spaceships)
            {
                s.transform.RotateAround(plane.transform.position, plane.transform.up, 25 * Time.deltaTime);

                if (Time.fixedTime % 13 == 0)
                {
                    GameObject c = Instantiate(cargo) as GameObject;
                    c.gameObject.tag = "cargo";
                    c.transform.localScale = new Vector3(6.318057f, 13.61268f, 13.61268f);
                    c.transform.position = s.transform.position;
                    c.SetActive(true);
                }
            }
        }
    }
}
