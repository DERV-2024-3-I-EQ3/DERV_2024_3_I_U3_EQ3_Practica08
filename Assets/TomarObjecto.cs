using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarObjecto : MonoBehaviour
{
    public GameObject objectoTomado;
    public bool IsTaken = false;
    public bool InObject = false;

    public Transform Padre;

    void Awake()
    {
        Padre = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (IsTaken == false && InObject == true)
            {
                IsTaken = true;
            }
            else
            {
                IsTaken = false;
            }
        }

        if (!IsTaken && objectoTomado != null)
        {
            Rigidbody rb = objectoTomado.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            objectoTomado.transform.SetParent(null);
            objectoTomado = null;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        InObject = true;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("Objecto"))
        {
            if (IsTaken && objectoTomado == null)
            {
                objectoTomado = collider.gameObject;
                objectoTomado.transform.SetParent(Padre);
                objectoTomado.transform.position = transform.position;
                objectoTomado.transform.rotation = transform.rotation;
                Rigidbody rb = objectoTomado.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        InObject = false;
    }
}
