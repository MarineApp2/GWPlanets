using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public long score = 0L;
    public int plus = 10;

    private Utils.Timer timer = new Utils.Timer();
    public float second = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Bip(second))
            score += plus;
    }
}
