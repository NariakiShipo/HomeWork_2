
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    private int rnd;

    private float time;
    public float interval;
    public float spwantime;

    public Transform generatePoint;
    
    public Material[] materials;
    public GameObject[] g_obj;
        
    // Update is called once per frame
    void Update()
    {
        time=Time.time;
        if(Input.GetKey(KeyCode.G))
        GenerateRandomObject();
        if(Input.GetKey(KeyCode.H))
        {
            if(time>spwantime)
            {
                GenerateRandomObject();
                spwantime+=interval;  
            }
        }
         if(Input.GetKey(KeyCode.J))
        {
            if(time>spwantime)
            {
                for(int j=0;j<5;j++)
                {
                    GenerateRandomObject();
                }
                spwantime+=interval;  
            }
        }
        
         if(Input.GetKey(KeyCode.K))
        {
            if(time>spwantime)
            {
                 for(int i=0;i<5;i++)
                {
                    for(int j=0;j<5;j++)
                    {
                       GenerateRandomObject();
                    }
                }
                spwantime+=interval;  
            }
        }
    }

    private void GenerateRandomObject()
    {
        rnd = (int)Random.Range(0, g_obj.Length);
        // Generate a random index based on the length of the Objects array
        GameObject obj = Instantiate(g_obj[rnd], generatePoint.position, Quaternion.identity);

        // Set the parent of the instantiated object to the generatePoint
        int materialIndex = Random.Range(0, materials.Length);
        Material randomMaterial = materials[materialIndex];

        // Set the material of the instantiated object to the random material
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material = randomMaterial;
        }
        // Add a Rigidbody component if it doesn't exist
        if (obj.GetComponent<Rigidbody>() == null)
        {
            obj.AddComponent<Rigidbody>();
        }
        obj.GetComponent<Rigidbody>().useGravity = true;

        //Destroy the object after 5 seconds
        Destroy(obj, 5);
    }
}
