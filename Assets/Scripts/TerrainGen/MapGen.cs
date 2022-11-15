
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    Dictionary<int, GameObject> tileSet;
    Dictionary<int, GameObject> tile_Groups;
    [SerializeField] GameObject Flat, Mountain, Liquid, Forest;

    [SerializeField] int iMapWidth = 16, iMapHeight = 16;
    [SerializeField] float fMagnify = 7;

    [SerializeField] int iXOffset = 0, iYOffset = 0;

    List<List<int>> noiseGrid = new List<List<int>>();
    List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    [SerializeField] GameObject HideScreen;

    public static MapGen Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

        }

    }
    public void StartGenMap()
    {
        HideScreen.SetActive(true);
        iXOffset = Random.Range(-100, 100);
        iYOffset = Random.Range(-100, 100);
        CreateTileSet();
        CreateTileGroups();
        GenerateMap();


        //if spawn is on a mountain
        if (tileGrid[100][100].CompareTag("Mountain") && GameManager.Instance)
        {
            GameManager.Instance.LoadIntoPlanet();
        }



        Invoke(nameof(StartHideScreen), 2f);

    }

    void StartHideScreen()
    {
        HideScreen.SetActive(false);
    }

    private void GenerateMap()
    {
        for (int x = 0; x < iMapWidth; x++)
        {
            noiseGrid.Add(new List<int>());
            tileGrid.Add(new List<GameObject>());
            for (int y = 0; y < iMapHeight; y++)
            {
                int iTileID = GetPerlinID(x, y);
                noiseGrid[x].Add(iTileID);
                CreateTile(iTileID, x, y);
            }
        }



    }

    private void CreateTile(int iTileID, int x, int y)
    {
        GameObject tilePrefab = tileSet[iTileID];
        GameObject tile_Group = tile_Groups[iTileID];
        GameObject Tile = Instantiate(tilePrefab, tile_Group.transform);

        Tile.name = $"X:{x}Y:{y}";
        Tile.transform.localPosition = new Vector3(x, y);

        tileGrid[x].Add(Tile);
    }

    private int GetPerlinID(int x, int y)
    {
        float PerlinProduct = Mathf.PerlinNoise(
            (x - iXOffset) / fMagnify,
            (y - iYOffset) / fMagnify
        );

        float fClampPerlin = Mathf.Clamp01((PerlinProduct));
        float fScaledPerlin = fClampPerlin * tileSet.Count;
        if (fScaledPerlin == 4)
            fScaledPerlin = 3;

        return Mathf.FloorToInt(fScaledPerlin);
    }

    private void CreateTileSet()
    {
        tileSet = new Dictionary<int, GameObject>();
        tileSet.Add(0, Liquid);
        tileSet.Add(1, Flat);
        tileSet.Add(2, Forest);
        tileSet.Add(3, Mountain);
    }

    void CreateTileGroups()
    {
        /** Create empty gameobjects for grouping tiles of the same type, ie
           forest tiles **/

        tile_Groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileSet)
        {
            GameObject tile_Group = new GameObject(prefab_pair.Value.name);
            tile_Group.transform.parent = gameObject.transform;
            tile_Group.transform.localPosition = new Vector3(0, 0);
            tile_Groups.Add(prefab_pair.Key, tile_Group);

        }
    }




}
