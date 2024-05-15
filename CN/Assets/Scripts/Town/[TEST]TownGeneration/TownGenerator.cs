using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpaceType
{
    Building,
    Road,
    Empty
}
public class TownGenerator : MonoBehaviour
{
    
    public SpaceType spaceType;
    public Transform startPos;
    private List<Cell> grid = new List<Cell>();
    public int maxRows;
    public int maxCols;
    public int padding;
    //Pool of building types
    public GameObject[] buildingPrefabs;
    public GameObject roadPrefab;
    //For storing references to buildings in scene
    public List<GameObject> buildings;
    //List of roads


    // Start is called before the first frame update
    void Start()
    {
        GenerateTown();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            GenerateTown();
        }
    }

    private void GenerateTown()
    {
        ClearTown();
          

        for (int i = 0; i < maxRows; i++)
        {
            for (int j = 0; j < maxCols; j++)
            {
                //Default space type
                SpaceType type = SpaceType.Empty;
                bool occupied = false;

                GameObject randomBuilding = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
                GameObject build = null;
                if(Random.Range(0,2) == 0)
                {
                    build = Instantiate(randomBuilding, new Vector3(startPos.position.x + i + (padding * i), startPos.position.y, startPos.position.z - j - (padding * j)), Quaternion.identity);
                    build.transform.SetParent(transform);
                    buildings.Add(build);

                    Cell cell = new Cell(SpaceType.Building, true);
                    type = SpaceType.Building;
                    occupied = true;

                }
               

                grid.Add(new Cell(type,occupied));

            }
        }
    }

    private void ClearTown()
    {
        foreach(GameObject build in buildings)
        {
            Destroy(build);
        }

        buildings.Clear();
        grid.Clear();
    }

    
}
