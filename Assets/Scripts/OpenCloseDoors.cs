using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenCloseDoors : MonoBehaviour
{
    bool _isOpened;

    [SerializeField] Transform leftWing;
    [SerializeField] Transform rightWing;

    [SerializeField] float _doorOpenOffset;
    [SerializeField] float _doorActionDuration;

    [SerializeField] bool _initialStateOpen;

    // Start is called before the first frame update
    void Start()
    {
        if (_initialStateOpen)
        {
            _isOpened = true;
            leftWing.localPosition = Vector3.right * _doorOpenOffset;
            rightWing.localPosition = Vector3.left * _doorOpenOffset;
        }
        else
        {
            _isOpened = false;
            leftWing.localPosition = Vector3.zero;
            rightWing.localPosition = Vector3.zero;
        }
        
    }

    public void DoorOpenClose()
    {
        if (_isOpened)
        {
            _isOpened = false;
            leftWing.DOLocalMoveX(0, _doorActionDuration);
            rightWing.DOLocalMoveX(0, _doorActionDuration);
        }
        else
        {
            _isOpened = true;
            leftWing.DOLocalMoveX(_doorOpenOffset, _doorActionDuration);
            rightWing.DOLocalMoveX(-_doorOpenOffset, _doorActionDuration);
        }
    }

}
