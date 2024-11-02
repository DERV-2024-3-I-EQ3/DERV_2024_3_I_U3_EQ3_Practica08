using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textError;
    [SerializeField] TextMeshProUGUI textCorrecto;
    [SerializeField] TextMeshProUGUI textColor;
    [SerializeField] Transform posicion;
    Renderer rend;
    private int index;
    [System.Serializable]
    public struct Colores
    {
        public string nombre;
        public Color color;
        public GameObject objeto;
        public Material material;

    }

    [SerializeField] private List<Colores> colores;

    void Start()
    {
        rend = GetComponent<Renderer>();
        CambiarColor();

    }

    public void CambiarColor()
    {
        if (colores.Count > 0)
        {
            index = Random.Range(0, colores.Count);
            rend.material = colores[index].material;
            textColor.text = colores[index].nombre;
            textColor.color = colores[index].color;
        }
        else
        {
            Debug.Log("No hay más colores");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objecto"))
        {
            if (collision.gameObject == colores[index].objeto)
            {
                Debug.Log("Correcto");
                StartCoroutine(MensajeCorrecto());
                Destroy(collision.gameObject);
                colores.RemoveAt(index);
                CambiarColor();
            }
            else
            {
                StartCoroutine(MensajeIncorrecto());
                collision.gameObject.transform.position = posicion.position;
            }
        }
    }
    private IEnumerator MensajeCorrecto()
    {
        textCorrecto.text = "Correcto";
        yield return new WaitForSeconds(1);
        textCorrecto.text = "";
    }

    private IEnumerator MensajeIncorrecto()
    {
        textError.text = "Error";
        yield return new WaitForSeconds(1);
        textError.text = "";
    }

}