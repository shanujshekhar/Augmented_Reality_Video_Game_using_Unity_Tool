using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDayNight : MonoBehaviour
{
    private GameObject day;
    private GameObject[] spaceships;
    private GameObject[] greenhouses;
    private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        
        day = GameObject.Find("Directional Light").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        greenhouses = GameObject.FindGameObjectsWithTag("GreenHouse");
        spaceships = GameObject.FindGameObjectsWithTag("Spaceship");
    }

    public void Toggle()
    {
        day.GetComponent<Light>().enabled = !day.GetComponent<Light>().enabled;

        on = !on;
        if (spaceships!=null && spaceships.Length != 0)
        {
            foreach(GameObject s in spaceships)
                s.transform.Find("Light").GetChild(0).GetComponent<Light>().enabled = on;
        }

        if (greenhouses!=null && greenhouses.Length != 0)
        {
            foreach (GameObject g in greenhouses)
                g.transform.GetChild(1).gameObject.SetActive(on);
        }
    }

}
