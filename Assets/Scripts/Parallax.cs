using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;

    public GameObject cam;
    public float parallaxCoefficient;

    private void Awake()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1f - parallaxCoefficient);

        float distance = (cam.transform.position.x * parallaxCoefficient);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}
