using TMPro;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator gen;   // Singleton

    [Header ("Variables Generación")]
    [SerializeField] public GameObject piece;
    [SerializeField] public int Width;
    [SerializeField] public int Height;

    [Header("Variables Bombas")]
    [SerializeField] public int BombsNumber;
    public int BombsLeft;
    [SerializeField] public TextMeshProUGUI BombsCounterText;

    public GameObject[][] Map;

    private void Start()
    {
        //inicializamos el contador de minas y el texto del contador
        BombsLeft = BombsNumber;
        BombsCounterText.text = BombsLeft.ToString();

        // Activamos el Singleton
        gen = this;

        // Inicializamos el array de columnas
        Map = new GameObject[Width][];
        for (int i = 0; i < Width; i++) { 
            Map[i] = new GameObject[Height];
        }

        // Centramos la cámara
        Camera.main.transform.position = new Vector3(((float)Width / 2) - 0.5f, ((float)Height / 2) - 0.5f, -10f);

        // Instanciamos las piezas y las guardamos en el array
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Map[i][j]= Instantiate(piece, new Vector2(i,j),Quaternion.identity);
                Map[i][j].GetComponent<Piece>().x=i;
                Map[i][j].GetComponent<Piece>().y=j;
               
            }
        }

        // Colocamos las bombas
        SetBombs();
    }

    private void SetBombs()
    {
        for (int i = 0; i < BombsNumber; i++)
        {
            int x = Random.Range(0, Width);
            int y = Random.Range(0, Height);

            Piece p = Map[x][y].GetComponent<Piece>();

            if (!p.bomb)
            {
                p.bomb = true;
            }
            else
            {
                i--; // Si ya había bomba, repetimos
            }
        }
    }

    public int GetBombsAround(int x, int y)
    {
        int count = 0;

        // Arriba izquierda
        if (x > 0 && y < Height - 1 && Map[x - 1][y + 1].GetComponent<Piece>().bomb) count++;

        // Arriba
        if (y < Height - 1 && Map[x][y + 1].GetComponent<Piece>().bomb) count++;

        // Arriba derecha
        if (x < Width - 1 && y < Height - 1 && Map[x + 1][y + 1].GetComponent<Piece>().bomb) count++;

        // Izquierda
        if (x > 0 && Map[x - 1][y].GetComponent<Piece>().bomb) count++;

        // Derecha
        if (x < Width - 1 && Map[x + 1][y].GetComponent<Piece>().bomb) count++;

        // Abajo izquierda
        if (x > 0 && y > 0 && Map[x - 1][y - 1].GetComponent<Piece>().bomb) count++;

        // Abajo
        if (y > 0 && Map[x][y - 1].GetComponent<Piece>().bomb) count++;

        // Abajo derecha
        if (x < Width - 1 && y > 0 && Map[x + 1][y - 1].GetComponent<Piece>().bomb) count++;

        return count;
    }

    //Métodos para colocar y quitar las banderas
    public void AddFlag()
    {
        BombsLeft--;
        BombsCounterText.text = BombsLeft.ToString();
    }

    public void RemoveFlag()
    {
        BombsLeft++;
        BombsCounterText.text = BombsLeft.ToString();
    }

}
