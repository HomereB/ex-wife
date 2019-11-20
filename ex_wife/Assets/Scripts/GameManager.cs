using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cyclops;
    public GameObject Gladiator;
    public float metamorphe = 0.0f;
    public Text progressionBar;

    private GameObject GaladiatorInstance;
    private GameObject CyclopsInstance;
    private float speed = 0.08f;
    private bool isMeta = false;
    void Start()
    {
        GaladiatorInstance = Instantiate(Gladiator, new Vector2(0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Metamorphose();
    }

    void Metamorphose()
    {  
        progressionBar.text = metamorphe.ToString();
        metamorphe += speed;

        if (metamorphe >= 100.0f)
        {
            speed = 0.0f;
            if (!isMeta)
            {
                Transform posGladiator = GaladiatorInstance.transform;
                Destroy(GaladiatorInstance);
                CyclopsInstance = Instantiate(Cyclops, posGladiator.transform.position,Quaternion.identity);
                isMeta = true;
            }
        }
    }
}
