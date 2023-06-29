using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerHigherControlTable : MonoBehaviour
{
    public bool goUp;
    public bool goDown;

    [SerializeField] float speed;
    public void up(bool on)
    {
        goUp = on;
    }

    public void down(bool on)
    {
        goDown = on;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp || goDown)
        {
            if (goUp)
            {
                this.transform.position += this.transform.up * speed * Time.deltaTime;
            }
            if (goDown)
            {
                this.transform.position -= this.transform.up * speed * Time.deltaTime;
            }
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, Mathf.Clamp(this.transform.localPosition.y, 0, 0.18f), this.transform.localPosition.z);
        }
        

    }
}
