using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerManager : MonoBehaviour
{
    [SerializeField]
    GameObject person;
    [SerializeField]
    GameObject bird;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MovePerson();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveBird();
        }
    }

    public void MovePerson()
    {
        bird.GetComponent<ObjectController>().enabled = false;
        person.GetComponent<PersonController>().enabled = true;
    }

    public void MoveBird()
    {
        bird.GetComponent<ObjectController>().enabled = true;
        person.GetComponent<PersonController>().enabled = false;
    }
}
