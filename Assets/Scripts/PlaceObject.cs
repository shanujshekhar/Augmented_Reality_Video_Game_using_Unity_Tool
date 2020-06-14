using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float speed = 0.2f;
    private float sliderspeed;
    private string name = "";
    private GameObject objectToPlace = null;
    private GameObject plane;
    private GameObject marker;
    private GameObject cargomarker;
    private GameObject activeOptions = null;
    private GameObject Buildui = null;
    private bool collided = false;
    private bool lt = true;
    private bool rt = true;
    private bool u = true;
    private bool d = true;
    private bool cargo = false;
    private string prev = "empty";
    private Slider sliderSpaceship;
    private Slider sliderPlanet;
    private float height = 0;
    private Text instructions;

    public void OnPointerDown(PointerEventData eventData)
    {
        name = eventData.selectedObject.name;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        name = "";
    }


    public void ObjectNull()
    {
        objectToPlace = null;
        height = 0;
        if(sliderSpaceship!=null)
            sliderSpaceship.value = 0;
        if(sliderPlanet!=null)
            sliderPlanet.value = 0;

        instructions.text = "Tap an Icon To Place";

        if(marker!=null)
            marker.SetActive(false);
        if(activeOptions!=null)
            activeOptions.SetActive(false);

        if (cargomarker != null)
        {
            cargomarker.SetActive(false);
            cargo = false;
        }
        
        lt = rt = u = d = true;
    }

    void Start()
    {
        marker = GameObject.Find("Marker").transform.GetChild(0).gameObject;
        cargomarker = GameObject.Find("Marker").transform.GetChild(1).gameObject;
        plane = GameObject.Find("ImageTarget").transform.GetChild(0).gameObject;
        Buildui = GameObject.Find("BuildUIParent").transform.GetChild(0).gameObject;
        instructions = GameObject.Find("BuildUIParent").transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
            bool hit = Physics.Raycast(ray, out hitInfo);

            if (hit)
            {
                string objectName = hitInfo.transform.gameObject.name;
                if (objectName.Contains("Model") || objectName.Contains("cargo(Clone)"))
                {

                    objectToPlace = hitInfo.transform.gameObject;

                    if (objectName.Contains("cargo"))
                    {
                        cargo = true;

                        cargomarker.SetActive(true);
                        cargomarker.transform.position = objectToPlace.transform.position;

                        cargomarker.transform.position = new Vector3(cargomarker.transform.position.x, cargomarker.transform.position.y + 0.5f, cargomarker.transform.position.z);
                        cargomarker.transform.RotateAround(cargomarker.transform.up, 0.5f * Time.deltaTime);

                    }
                    else if(objectName.Contains("Model") && Buildui.activeSelf == true)
                    {
                        instructions.text = "Use Screen Controls to place object" + "\n" + "Click Done to Confirm";
                        marker.SetActive(true);
                        marker.transform.position = objectToPlace.transform.position;

                        marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y + 1.5f, marker.transform.position.z);
                        marker.transform.RotateAround(marker.transform.up, 0.5f * Time.deltaTime);
                    }

                    if (objectName.Contains("GreenHouseModel") && Buildui.activeSelf==true)
                    {
                        activeOptions = GameObject.Find("GreenHouseUI").transform.GetChild(1).gameObject;
                        speed = 5;
                        if (activeOptions != null)
                            activeOptions.SetActive(true);

                        if (GameObject.Find("SpaceshipUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("SpaceshipUI").transform.GetChild(1).gameObject.SetActive(false);

                        if (GameObject.Find("PlanetUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("PlanetUI").transform.GetChild(1).gameObject.SetActive(false);
                    }
                    else if (objectName.Contains("SpaceshipModel") && Buildui.activeSelf == true)
                    {
                        if (sliderPlanet != null)
                            sliderPlanet.onValueChanged.RemoveListener(delegate { Debug.Log("Removed"); });

                        if (GameObject.Find("GreenHouseUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("GreenHouseUI").transform.GetChild(1).gameObject.SetActive(false);

                        if (GameObject.Find("PlanetUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("PlanetUI").transform.GetChild(1).gameObject.SetActive(false);

                        activeOptions = GameObject.Find("SpaceshipUI").transform.GetChild(1).gameObject;
                        if (activeOptions != null)
                            activeOptions.SetActive(true);
                        sliderSpaceship = GameObject.Find("SpaceshipUI").transform.GetChild(1).GetChild(1).gameObject.GetComponent<Slider>();

                        sliderSpaceship.onValueChanged.AddListener(delegate { changeHeight(sliderSpaceship); });
                        sliderspeed = 0.015f;
                        speed = 0.1f;
                    }
                    else if (objectName.Contains("PlanetModel") && Buildui.activeSelf == true)
                    {
                        if (sliderSpaceship != null)
                            sliderSpaceship.onValueChanged.RemoveListener(delegate { Debug.Log("Removed"); });

                        if (GameObject.Find("GreenHouseUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("GreenHouseUI").transform.GetChild(1).gameObject.SetActive(false);

                        if (GameObject.Find("SpaceshipUI").transform.GetChild(1).gameObject.activeSelf)
                            GameObject.Find("SpaceshipUI").transform.GetChild(1).gameObject.SetActive(false);

                        activeOptions = GameObject.Find("PlanetUI").transform.GetChild(1).gameObject;
                        if (activeOptions != null)
                            activeOptions.SetActive(true);
                        sliderPlanet = GameObject.Find("PlanetUI").transform.GetChild(1).GetChild(2).gameObject.GetComponent<Slider>();

                        sliderPlanet.onValueChanged.AddListener(delegate { changeHeight(sliderPlanet); });
                        sliderspeed = 1;
                        speed = 2;
                    }
                    else if (objectName.Contains("cargo"))
                    {
                        activeOptions = GameObject.Find("GameUI").transform.GetChild(0).GetChild(3).gameObject;
                        speed = 5;
                        if (activeOptions != null)
                            activeOptions.SetActive(true);
                    }
                }
            }
            
        }
        if (objectToPlace != null)
        {
            if (objectToPlace.name.Contains("GreenHouseModel"))
            {
                collided = objectToPlace.GetComponent<CollisionCheck>().collided;

                if (collided && prev.Equals("empty"))
                    prev = name;

                if (collided && prev.Equals(name))
                {
                    switch (prev)
                    {
                        case "Left":
                            lt = false;
                            break;
                        case "Right":
                            rt = false;
                            break;
                        case "Up":
                            u = false;
                            break;
                        case "Down":
                            d = false;
                            break;
                    }
                }
                else
                {
                    prev = "empty";
                    lt = rt = u = d = true;
                }
            }
        }

        if (objectToPlace != null)
        {
            if (cargo)
            {
                cargomarker.transform.position = objectToPlace.transform.position;

                cargomarker.transform.position = new Vector3(cargomarker.transform.position.x, cargomarker.transform.position.y + 0.5f, cargomarker.transform.position.z);
                cargomarker.transform.RotateAround(cargomarker.transform.up, 0.5f * Time.deltaTime);
            }
            else if(Buildui.activeSelf == true)
            {
                marker.transform.position = objectToPlace.transform.position;

                marker.transform.position = new Vector3(marker.transform.position.x, marker.transform.position.y + 1.5f, marker.transform.position.z);
                marker.transform.RotateAround(marker.transform.up, 0.5f * Time.deltaTime);
            }
        }


        switch (name)
        {
            case "Left":
                left();
                break;
            case "Right":
                right();
                break;
            case "Up":
                up();
                break;
            case "Down":
                down();
                break;
            case "LeftRotate":
                leftrot();
                break;
            case "RightRotate":
                rightrot();
                break;
        }
    }
   
    public void left()
    {
        if (objectToPlace != null)
        {
            if (objectToPlace.name.Contains("GreenHouseModel") && !lt)
                objectToPlace.transform.localPosition = objectToPlace.transform.localPosition;
            else
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x - 0.5f, objectToPlace.transform.localPosition.y, objectToPlace.transform.localPosition.z), speed * Time.deltaTime);
            }
        }
    }

    public void right()
    {
        if (objectToPlace != null)
        {
            if (objectToPlace.name.Contains("GreenHouseModel") && !rt)
                objectToPlace.transform.localPosition = objectToPlace.transform.localPosition;
            else
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x + 0.5f, objectToPlace.transform.localPosition.y, objectToPlace.transform.localPosition.z), speed * Time.deltaTime);
            }
        }
    }

    public void up()
    {
        if (objectToPlace != null)
        {
            if (objectToPlace.name.Contains("GreenHouseModel") && !u)
                objectToPlace.transform.localPosition = objectToPlace.transform.localPosition;
            else
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x, objectToPlace.transform.localPosition.y, objectToPlace.transform.localPosition.z + 0.5f), speed * Time.deltaTime);
            }
        }
    }

    public void down()
    {
        if (objectToPlace != null)
        {
            if (objectToPlace.name.Contains("GreenHouseModel") && !d)
                objectToPlace.transform.localPosition = objectToPlace.transform.localPosition;
            else
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x, objectToPlace.transform.localPosition.y, objectToPlace.transform.localPosition.z - 0.5f), speed * Time.deltaTime);
            }
        }
    }

    public void leftrot()
    {
        if (objectToPlace != null)
        {
            objectToPlace.transform.RotateAround(-plane.transform.up, 1 * Time.deltaTime);
        }
    }

    public void rightrot()
    {
        if (objectToPlace != null)
        {
            objectToPlace.transform.RotateAround(plane.transform.up, 1 * Time.deltaTime);
        }
    }

    public void changeHeight(Slider slider)
    {
        Debug.Log("Speed: " + sliderspeed);
        if (slider != null && objectToPlace!=null)
        {
            Debug.Log("Slider" + slider.transform.parent.name);
            if (height < slider.value)
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x, objectToPlace.transform.localPosition.y + slider.value, objectToPlace.transform.localPosition.z), sliderspeed * Time.deltaTime);

                height = slider.value;
            }
            else if (height > slider.value)
            {
                objectToPlace.transform.localPosition = Vector3.MoveTowards(objectToPlace.transform.localPosition, new Vector3(objectToPlace.transform.localPosition.x, objectToPlace.transform.localPosition.y - slider.value, objectToPlace.transform.localPosition.z), sliderspeed * Time.deltaTime);

                height = slider.value;
            }
        }
    }

}
