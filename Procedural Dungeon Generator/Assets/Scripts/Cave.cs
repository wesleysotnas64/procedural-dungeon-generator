using UnityEngine;

public class Cave : MonoBehaviour
{
    [Header("Procedural Generation")]
    [Range(0, 100)]
    [SerializeField] private float percentToBeWall;

    [Range(0, 8)]
    [SerializeField] private int wallAdjacent;

    [Header("Cave Attribuits")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector2Int offset;
    private CellCave[,] cellCave;
    public GameObject cellCavePrefab;

    void Start()
    {
        offset = new Vector2Int(
            -(width/2),
            -(height/2)
        );

        cellCave = new CellCave[height, width];

        InitializeCave();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) ReloadCave();
        if(Input.GetKeyDown(KeyCode.S)) SmoothCave();
    }

    private void InitializeCave()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                Vector3 pos = new(x + offset.x, y + offset.y, 0);

                GameObject newCell = Instantiate(cellCavePrefab, pos, Quaternion.identity, transform);
                cellCave[y, x] = newCell.GetComponent<CellCave>();
            }
        }

        RandomCells();
    }

    private void RandomCells()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0, 101) < percentToBeWall)
                {
                    cellCave[y, x].SetToWall();
                }
                else
                {
                    cellCave[y, x].SetToEmpty();
                }
            }
        }
    }

    private void ReloadCave()
    {
        RandomCells();
    }

    private int GetAdjacentWallCount(int gridX, int gridY) {
        int wallCount = 0;

        for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++) {
            for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++) {
                
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height) {
                    if (neighborX != gridX || neighborY != gridY) {
                        if (cellCave[neighborY, neighborX].IsWall()) {
                            wallCount++;
                        }
                    }
                } else {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void SmoothCave() {
  
        bool[,] tempMap = new bool[height, width];

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                int neighbors = GetAdjacentWallCount(x, y);
                
                if (neighbors > wallAdjacent) tempMap[y, x] = true;
                else if (neighbors < wallAdjacent) tempMap[y, x] = false;
                else tempMap[y, x] = cellCave[y, x].IsWall();
            }
        }

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (tempMap[y, x]) cellCave[y, x].SetToWall();
                else cellCave[y, x].SetToEmpty();
            }
        }

    }
}
