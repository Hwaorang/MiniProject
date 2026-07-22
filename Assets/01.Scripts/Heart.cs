using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite onHeart;
    public Sprite offHeart;

    private SpriteRenderer sr;

    public int heartNumber;


    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(GameManager.instance.nowLife >= heartNumber)
        {
            sr.sprite = onHeart;
        }
        else
        {
            sr.sprite = offHeart;
        }
    }
}
