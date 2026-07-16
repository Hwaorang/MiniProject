using UnityEngine;

public class Background : MonoBehaviour
{
    private Transform[] back;

    [SerializeField] private float scrollSpeed;

    private float backgroundWidth;
    private float total;

    void Start()
    {
        back = new Transform[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            back[i] = transform.GetChild(i);
        }

        SpriteRenderer backgroundRender = back[0].GetComponent<SpriteRenderer>();

        if (back.Length > 0 && backgroundRender != null)
        {
            
            backgroundWidth = backgroundRender.bounds.size.x;
        }

        total = backgroundWidth * back.Length;

        for (int i = 0; i < back.Length;i++)
        {
            if(back[i] != null)
            {
                back[i].position = new Vector3(i * backgroundWidth, transform.position.y, transform.position.z);
            }
        }
    }

    
    void Update()
    {
        for (int i = 0;i < back.Length;i++)
        {
            back[i].position += Vector3.left * scrollSpeed * Time.deltaTime;
        }

        for(int i = 0;i < back.Length; i++)
        {
            if (back[i].position.x <= -backgroundWidth)
            {
                int childIndex = (i == 0) ? 1 : 0;
                Transform child = back[childIndex];

                Vector3 newPos = back[i].position;

                newPos.x = child.position.x + backgroundWidth;

                newPos.x -= 0.02f;

                back[i].position = newPos;
            }
        }
    }
}
