using Main.Assets._Project.Scripts.Runtime.Input;
using Main.Assets._Project.Scripts.Runtime.PickUpItem;
using Main.Assets._Project.Scripts.Runtime.Player;
using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.Level
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Animator _garageAnimator;
        [SerializeField] private AnimationClip _garageOpenAnimation;
        [SerializeField] private PlayerMotor _playerMotor;
        [SerializeField] private PlayerPickUpController _playerPickUpController;
        [SerializeField] private Item _startItem;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _playerMotor.Init(_inputHandler);
            _playerPickUpController.Init(_inputHandler, _startItem);

            _inputHandler.Input.Enable();

            _garageAnimator.SetTrigger("OpenTrigger");
        }
    }
}
