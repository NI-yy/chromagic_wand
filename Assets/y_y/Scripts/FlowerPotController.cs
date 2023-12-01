using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPotController : MonoBehaviour
{
    private string bulletTag = "bullet";
    private string bulletColor;
    [SerializeField] Sprite flowerPlot_1;
    [SerializeField] Sprite flowerPlot_2;
    [SerializeField] GameObject grownedTree;

    private bool flag = false;
    // Start is called before the first frame update

    SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag(bulletTag))
        //{
        //    Debug.Log("Trigger");
        //    bulletColor = collision.gameObject.GetComponent<bulletController>().GetBulletColor();
        //    Debug.Log(bulletColor);
        //    if (bulletColor.Equals("Green"))
        //    {
        //        renderer.sprite = flowerPlot_2;
        //    }
        //}

        if (collision.gameObject.CompareTag("LeafAttack"))
        {
            renderer.sprite = flowerPlot_2;
            flag = true;
        }
        else if (collision.gameObject.CompareTag("WaterAttack") && flag)
        {
            renderer.sprite = flowerPlot_1;
            grownedTree.SetActive(true);
        }
    }
}
