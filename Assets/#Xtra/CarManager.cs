using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
public class CarManager : MonoBehaviour
{
    public GameObject MainCam;
    public GameObject Player;

    public GameObject CarCamera;
    public GameObject Car;
    public Transform ExitPos;

    public float CarState;

    public GameObject FalsePlayer;

    public CarRadio Radio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MainCam.SetActive(false);
            Player.SetActive(false);
            CarCamera.SetActive(true);
            Car.GetComponent<CarController>().enabled = true;
            Car.GetComponent<CarUserControl>().enabled = true;
            Car.GetComponent<CarAudio>().enabled = true;
            FalsePlayer.SetActive(true);

        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CarCamera.SetActive(false);
            Car.GetComponent<CarController>().enabled = false;
            Car.GetComponent<CarUserControl>().enabled = false;
            Car.GetComponent<CarAudio>().enabled = false;
            Player.transform.position = ExitPos.position;
            Player.transform.rotation = ExitPos.rotation; 
            MainCam.SetActive(true);
            Player.SetActive(true);
            FalsePlayer.SetActive(false);
        }
    }
}
