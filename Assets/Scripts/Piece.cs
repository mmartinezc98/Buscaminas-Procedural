using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Piece : MonoBehaviour
{
    public int x;
    public int y;
    public bool bomb = false;

    private void OnMouseDown()
    {
        if (bomb)
        {
            // si es una bomba lo pintamos de rojo
            GetComponent<SpriteRenderer>().color = Color.red;
            transform.GetChild(0).gameObject.SetActive(false); // Oculta el Canvas
        }
        else
        {
            // Si no es bomba, mostramos el número           
            transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =MapGenerator.gen.GetBombsAround(x,y).ToString();
        }
    }
}