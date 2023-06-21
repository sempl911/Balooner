using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGparallaxPlane : MonoBehaviour
{
    Transform cam;
    Vector2 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] materials;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.06f)]  public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int BackGroundCount = transform.childCount;
        materials = new Material[BackGroundCount];
        backSpeed = new float[BackGroundCount];
        backgrounds = new GameObject[BackGroundCount];

        for (int i = 0; i < BackGroundCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;

        }

        backSpeedCalculate(BackGroundCount);
    }

    void backSpeedCalculate(int BackCount)
    {
        for (int i = 0; i < BackCount; i++)
        {
            if ((backgrounds[i].transform.position.z -cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < BackCount; i++)
        {
            backSpeed[i] = 1- (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void LateUpdate()
    {
        distance = cam.position.x - camStartPos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
