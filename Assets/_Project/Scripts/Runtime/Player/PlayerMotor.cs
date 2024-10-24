using Main.Assets._Project.Scripts.Runtime.Input;
using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Camera _camera;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _smoothMoveDeltaTime;

        private InputHandler _inputHandler;
        private Transform _motorObject;
        private Vector3 _currentMoveDirection;
        private Vector3 _newMoveDirection;
        private Vector3 _currentVelocity;
        private float _yRotation;
        private bool _isMoveActive;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Init(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _isMoveActive = true;
            _motorObject = transform;

            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _inputHandler.Input.Gameplay.Look.performed += ctx => SetRotationDirection(ctx.ReadValue<Vector2>());
            _inputHandler.Input.Gameplay.Move.performed += ctx => SetMoveDirection(ctx.ReadValue<Vector2>());
            _inputHandler.Input.Gameplay.Move.canceled += ctx => SetMoveDirection(Vector2.zero);
        }

        private void UnsubscribeOnEvents()
        {
            if (_inputHandler == null)
                return;

            _inputHandler.Input.Gameplay.Look.performed -= ctx => SetRotationDirection(ctx.ReadValue<Vector2>());
            _inputHandler.Input.Gameplay.Move.performed -= ctx => SetMoveDirection(ctx.ReadValue<Vector2>());
            _inputHandler.Input.Gameplay.Move.canceled -= ctx => SetMoveDirection(Vector2.zero);
        }

        private void Update()
        {
            if (!_isMoveActive)
                return;

            Vector3 moveVector = transform.TransformDirection(new Vector3(_newMoveDirection.x, 0, _newMoveDirection.y))
                .normalized;

            _currentMoveDirection = Vector3.SmoothDamp(_currentMoveDirection, moveVector * _moveSpeed, ref _currentVelocity,
                _smoothMoveDeltaTime);

            _controller.Move(_currentMoveDirection * Time.deltaTime);
        }


        private void SetRotationDirection(Vector2 rotation)
        {
            rotation = rotation * _rotateSpeed * Time.deltaTime;

            _yRotation -= rotation.y;
            _yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

            _camera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
            _motorObject.Rotate(Vector3.up * rotation.x);
        }

        private void SetMoveDirection(Vector2 moveDirection)
        {
            _newMoveDirection = moveDirection;
        }
    }
}