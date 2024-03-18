using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlotPerlin : MonoBehaviour
{
    [Range(1, 8)]
    public int octaves = 2;//number of layers for fBM

    [Range(0, 1000)]
    public int xOffset = 0;

    [Range(0, 1000)]
    public int yOffset = 0;

    [Range(0.001f, 0.01f)]
    public float xScale = 0.003f;

    [Range(0.001f, 0.01f)]
    public float yScale = 0.003f;

   
    [Range(0.0f, 1.0f)]
    public float lowCutoff = 0;

    [Range(0.0f, 1.0f)]
    public float mediumlowCutoff = 0;

    [Range(0.0f, 1.0f)]
    public float mediumCutoff = 0;

    [Range(0.0f, 1.0f)]
    public float mediumhighCutoff = 0;

    [Range(0.0f, 1.0f)]
    public float highCutoff = 0;

   
    void OnValidate()
    {
        Texture2D texture = new Texture2D(1024, 1024);
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;

        float perlin;
        Color colour = Color.white;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                perlin = fBM((x + xOffset) * xScale, (y + yOffset) * yScale, octaves);

                colour = Color.black;

                //cutoff values indicating the density of the regions


                if (perlin < lowCutoff) colour = new Color(0,0,0);
                else if (perlin < mediumlowCutoff) colour = new Color(0.4f, 0.4f, 0.4f);
                else if (perlin < mediumCutoff) colour = new Color(0.6f, 0.6f, 0.6f);
                else if (perlin < mediumhighCutoff) colour = new Color(0.8f, 0.8f, 0.8f);
                else if (perlin < highCutoff) colour = new Color(1, 1, 1);

                texture.SetPixel(x, y, colour);
            }
        }
        texture.Apply();
    }
    // Fractal Brownian Motion to add multiple layers of perlin noise for better and smoother randomization
    public float fBM(float x, float y, int octaves)
    {
        float total = 0;
        float frequency = 1;
        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency);
            frequency *= 2;
        }
        return total / (float)octaves;
    }


}
