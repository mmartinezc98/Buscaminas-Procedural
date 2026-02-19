using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Piece : MonoBehaviour
{
    public bool revealed;//variable para ver si la casilla es revelada
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

        RevealPieces();
    }

    private void RevealPieces()
    {
        if (revealed) return; //no se hace nada si la casilla ya ha sido revelada
        revealed = true;

        //cambiamos el color de fondo de la casilla clickada
        GetComponent<SpriteRenderer>().color = Color.gray;


        if (bomb) //tampoco se hace nada si la casilla contiene una bomba
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            return;
        }
    
            
        //calculamos cuantas bombas hay alrededor de la casilla en la que hemos clickado
        int bombs = MapGenerator.gen.GetBombsAround(x, y);

        // Si es cero, expandimos
        if (bombs == 0)
        {
            ExpandZeroArea();
        }
    }


    private void ExpandZeroArea()
    {
        //recorremos las casillas vecinas en el cuadrado 3x3 de alrededor
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue; //si dx y y dy son 0 esque estamos en la casilla actual (se ignora)

                //calculamos la posicion del vecino
                int nx = x + dx;
                int ny = y + dy;

                //comprovamos que el vecino se encuentra dentro del mapa
                if (nx >= 0 && nx < MapGenerator.gen.Width && ny >= 0 && ny < MapGenerator.gen.Height)
                {
                    //obtenemos la piza vecina
                    Piece neighbor = MapGenerator.gen.Map[nx][ny].GetComponent<Piece>();

                    if (!neighbor.revealed) //si la casilla vecina que hemos obtenido no esta revelada la revelamos con el metodo OnMouseDown
                    {
                        neighbor.OnMouseDown();
                    }
                }
            }
        }
    }

}