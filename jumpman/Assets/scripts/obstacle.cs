using UnityEngine;

public class obstacle : MonoBehaviour
{

    private float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }

        


    }
}