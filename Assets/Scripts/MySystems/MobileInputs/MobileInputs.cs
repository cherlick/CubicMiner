using UnityEngine;

namespace MobileInputSystem
{
    /// <summary>
    /// System that allows to get the mobile touch inputs like:
    /// Tap, swipes and drag
    /// </summary>
    public class MobileInputs : MonoBehaviour
    {
        [Range(20, 200)]
        [SerializeField] private float _swipeMagnitude = 80;
        private Vector2 _startPos;
        private Vector2 _endPos;
        [SerializeField] private float _holdTrashhold = 1f;
        [SerializeField] private bool _onRelease = true;
        private float _holdcounter;
        private Vector3 _swipeDirection, _touchPosition;
        private bool _istap = false, _isSwipe = false, _isTouchHolding = false;
        
        

        private void Update() {
            if (Input.touches.Length > 0)
            {
                switch (Input.touches[0].phase)
                {
                    case TouchPhase.Began:
                        
                        Tap();
                        break;
                    case TouchPhase.Ended:
                        _endPos = Input.touches[0].position;
                        _touchPosition = Camera.main.ScreenToWorldPoint((_endPos));
                        if(_onRelease) _istap = true;
                        CheckSwipe();
                        Invoke("ResetVars", 0.01f);
                        break;
                    case TouchPhase.Stationary:
                        Hold();
                        break;
                    default:
                        break;
                }
                
            }
        }

        private void Tap()
        {
            //Debug.Log("Tap");
            _startPos = Input.touches[0].position;
            if (!_onRelease)
            {
                _istap = true;
                _touchPosition = Camera.main.ScreenToWorldPoint((_startPos));
            } 

        }
        private void Hold()
        {
            _holdcounter+=Time.deltaTime;
            if (_holdcounter>_holdTrashhold)
            {
                ResetVars();
                //Debug.Log("Hold");
                _isTouchHolding = true;
            }
            
            
        }
        private void ResetVars()
        {
            _istap = false;
            _holdcounter = 0;
            //_swipeDirection = Vector2.zero;
            _isTouchHolding = false;
            _isSwipe = false;
            _istap = false;

        }
        private void CheckSwipe()
        {

            _swipeDirection = _endPos - _startPos;

            if (_swipeDirection.magnitude < _swipeMagnitude)
            {
                _swipeDirection = Vector2.zero;
                return;
            }

            if (Mathf.Abs(_swipeDirection.x) > Mathf.Abs(_swipeDirection.y))
                _swipeDirection.y = 0; //Moving X axis
            else
                _swipeDirection.x = 0; //Moving Y axis
            ResetVars();
            _swipeDirection = _swipeDirection.normalized;
            //Debug.Log(_swipeDirection);
            _isSwipe = true;
            
            //Debug.Log("swipe");
        }

        public Vector3 SwipeDirection { get { return _swipeDirection; } }
        public bool IsSwipe { get { return _isSwipe; } }
        public bool IsTapTouch { get { return _istap; } }
        public Vector2 TouchPosition { get { return _touchPosition; } }
        public bool IsTouchHolding { get { return _isTouchHolding; } }
    }
}


