using System.Collections;
using UnityEngine;

public class Cave : MonoBehaviour
{
    [Header("Procedural Generation")]
    [Range(0, 100)]
    [SerializeField] private float percentToBeWall;

    [Range(0, 8)]
    [SerializeField] private int wallAdjacent;

    [Range(1, 10)]
    [SerializeField] private int cicleVerification;

    [Header("Cave Attribuits")]
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector2Int offset;
    private CellCave[,] cellCave;
    public GameObject cellCavePrefab;
    private bool isSmoothing;

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
        if(Input.GetKeyDown(KeyCode.R) && !isSmoothing) ReloadCave();
    }

    public void InitializeCave()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                Vector3 pos = new Vector3(x + offset.x, y + offset.y, 0);

                GameObject newCell = Instantiate(cellCavePrefab, pos, Quaternion.identity, transform);
                cellCave[y, x] = newCell.GetComponent<CellCave>();

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

        StartCoroutine(SmoothCave());
    }

    public void CleanCellCave()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Destroy(cellCave[y, x].gameObject);
                cellCave[y, x] = null;
            }
        }
    }

    private void ReloadCave()
    {
        CleanCellCave();
        InitializeCave();
    }

    int GetAdjacentWallCount(int gridX, int gridY) {
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

    private IEnumerator SmoothCave() {
        isSmoothing = true;
        for (int i = 0; i < cicleVerification; i++) {
            
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

            yield return new WaitForSeconds(1.0f);
        }
        isSmoothing = false;
    }
}
