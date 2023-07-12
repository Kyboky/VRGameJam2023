using UnityEngine;
using DG.Tweening;

public class OpenCloseDoors : MonoBehaviour
{
    [SerializeField] private Transform _leftWing;
    [SerializeField] private Transform _rightWing;
    [SerializeField] private float _doorOpenOffset;
    [SerializeField] private float _doorActionDuration;
    [SerializeField] private bool _initialStateOpen;

    private bool _isOpened;

    void Start()
    {
        if (_initialStateOpen)
        {
            _isOpened = true;
            _leftWing.localPosition = Vector3.right * _doorOpenOffset;
            _rightWing.localPosition = Vector3.left * _doorOpenOffset;
        }
        else
        {
            _isOpened = false;
            _leftWing.localPosition = Vector3.zero;
            _rightWing.localPosition = Vector3.zero;
        }
        
    }

    public void DoorOpenClose()
    {
        if (_isOpened)
        {
            _isOpened = false;
            _leftWing.DOLocalMoveX(0, _doorActionDuration);
            _rightWing.DOLocalMoveX(0, _doorActionDuration);
        }
        else
        {
            _isOpened = true;
            _leftWing.DOLocalMoveX(_doorOpenOffset, _doorActionDuration);
            _rightWing.DOLocalMoveX(-_doorOpenOffset, _doorActionDuration);
        }
    }
}
