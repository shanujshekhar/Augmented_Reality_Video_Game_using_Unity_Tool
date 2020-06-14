using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuplicateObjects : MonoBehaviour
{
    public GameObject Duplicate;
    private GameObject plane;
    private GameObject day;
    private GameObject GreenHouseSpawn;
    private GameObject SpaceshipSpawn;
    private GameObject PlanetSpawn;
    private int count = 0;

    void Start()
    {
        GreenHouseSpawn = GameObject.Find("GreenHouseSpawn").gameObject;
        SpaceshipSpawn = GameObject.Find("SpaceshipSpawn").gameObject;
        PlanetSpawn = GameObject.Find("PlanetSpawn").gameObject;
        day = GameObject.Find("Directional Light").gameObject;

        Button btn = transform.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(AddObject);
            plane = GameObject.Find("ImageTarget").transform.GetChild(0).gameObject;
        }
        
    }

    public void AddObject()
    {

        if (Duplicate.name.Equals("GreenHouseModel"))
        {
            GameObject c = Instantiate(Duplicate, GreenHouseSpawn.transform.position, GreenHouseSpawn.transform.rotation, plane.transform);
            c.gameObject.tag = "GreenHouse";
            c.transform.name = "GreenHouseModel" + (count + 1);
            c.transform.localScale = Duplicate.transform.lossyScale;

            if (!day.GetComponent<Light>().enabled)
                c.transform.GetChild(1).gameObject.SetActive(true);

            c.SetActive(true);
        }
        else if (Duplicate.name.Equals("SpaceshipModel"))
        {
            GameObject c = Instantiate(Duplicate, SpaceshipSpawn.transform.position, SpaceshipSpawn.transform.rotation, SpaceshipSpawn.transform);
            c.gameObject.tag = "Spaceship";
            c.transform.name = "SpaceshipModel" + (count + 1);
            c.transform.localScale = new Vector3(0.001491574f, 0.001491574f, 0.001491574f);

            if (!day.GetComponent<Light>().enabled)
                c.transform.Find("Light").GetChild(0).GetComponent<Light>().enabled = true;

            c.SetActive(true);
        }
        else if (Duplicate.name.Equals("PlanetModel"))
        {
            GameObject c = Instantiate(Duplicate, PlanetSpawn.transform.position, PlanetSpawn.transform.rotation, PlanetSpawn.transform);
            c.gameObject.tag = "Planet";
            c.transform.name = "PlanetModel" + (count + 1);
            c.transform.localScale = new Vector3(-1.631558f, -1.631558f, -1.631558f);
            c.SetActive(true);
        }
        

        
        count++;
    }
}
