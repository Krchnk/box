using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDrawer : MonoBehaviour
{
    private WorldGrid _worldGrid;
    private int _worldSize;
    private bool _isCube;
    private bool _isSphere;
    private bool _isCylinder;
    [SerializeField] private List <GameObject> _filler;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject cylinder;

    void Start()
    {
        _worldSize = 3;
        _worldGrid = new WorldGrid(_worldSize);

        FillWorldArray();
    }

    private void FillWorldArray()
    {
        for (int i = 0; i < _worldGrid.World.GetLength(0); i++)
        {
            for (int j = 0; j < _worldGrid.World.GetLength(1); j++)
            {
                for (int k = 0; k < _worldGrid.World.GetLength(2); k++)
                {
                    _worldGrid.World[i, j, k] = new Cell();
                    _worldGrid.World[i, j, k].cube = false;
                    _worldGrid.World[i, j, k].sphere = false;
                    _worldGrid.World[i, j, k].cylinder = false;
                }
            }
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Vector3 blockPos = hit.point + hit.normal / 2f;

                blockPos.x = (float)Math.Round(blockPos.x, MidpointRounding.AwayFromZero);
                blockPos.y = (float)Math.Round(blockPos.y, MidpointRounding.AwayFromZero);
                blockPos.z = (float)Math.Round(blockPos.z, MidpointRounding.AwayFromZero);

                if(blockPos.x >= 0 &&
                   blockPos.y >= 0 &&
                   blockPos.z >= 0 &&
                   blockPos.x < _worldSize &&
                   blockPos.y < _worldSize &&
                   blockPos.z < _worldSize)
                {
                    if(_isCube)
                    _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].cube = true;

                    if(_isSphere)
                    _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].sphere = true;

                    if (_isCylinder)
                        _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].cylinder = true;

                    ClearCell();
                    CheckCell();
                }          
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                Vector3 blockPos = hit.point - hit.normal / 2f;

                blockPos.x = (float)Math.Round(blockPos.x, MidpointRounding.AwayFromZero);
                blockPos.y = (float)Math.Round(blockPos.y, MidpointRounding.AwayFromZero);
                blockPos.z = (float)Math.Round(blockPos.z, MidpointRounding.AwayFromZero);

                _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].cube = false;
                _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].sphere = false;
                _worldGrid.World[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].cylinder = false;

                ClearCell();
                CheckCell();
            }
        }

    }

    private void ClearCell()
    {
        foreach (var item in _filler)
        {
            Destroy(item);
        }

        _filler.Clear();
    }

    private void CheckCell()
    {
        for (int i = 0; i < _worldGrid.World.GetLength(0); i++)
        {
            for (int j = 0; j < _worldGrid.World.GetLength(1); j++)
            {
                for (int k = 0; k < _worldGrid.World.GetLength(2); k++)
                {

                    if (_worldGrid.World[i, j, k].cube)
                    {
                        var newCube = Instantiate(cube, new Vector3(i, j, k), Quaternion.identity);
                        _filler.Add(newCube);
                    }

                    if (_worldGrid.World[i, j, k].sphere)
                    {
                        var newCube = Instantiate(sphere, new Vector3(i, j, k), Quaternion.identity);
                        _filler.Add(newCube);
                    }

                    if (_worldGrid.World[i, j, k].cylinder)
                    {
                        var newCube = Instantiate(cylinder, new Vector3(i, j, k), Quaternion.identity);
                        _filler.Add(newCube);
                    }


                }
            }
        }
    }

    public void ChooseCube()
    {
        _isCube = true;
        _isSphere = false;
        _isCylinder = false;
    }

    public void ChooseSphere()
    {
        _isCube = false;
        _isSphere = true;
        _isCylinder = false;
    }

    public void ChooseCylinder()
    {
        _isCube = false;
        _isSphere = false;
        _isCylinder = true;
    }

}
