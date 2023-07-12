using UnityEngine;

public class LowerHigherControlTable : MonoBehaviour
{
    public bool GoUp;
    public bool GoDown;

    [SerializeField] private float _speed = 0.03f;
    public void Up(bool on)
    {
        GoUp = on;
    }

    public void Down(bool on)
    {
        GoDown = on;
    }

    void Update()
    {
        if (GoUp || GoDown)
        {
            if (GoUp)
            {
                this.transform.position += this.transform.up * _speed * Time.deltaTime;
            }
            if (GoDown)
            {
                this.transform.position -= this.transform.up * _speed * Time.deltaTime;
            }
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, Mathf.Clamp(this.transform.localPosition.y, 0, 0.18f), this.transform.localPosition.z);
        }
        

    }
}
