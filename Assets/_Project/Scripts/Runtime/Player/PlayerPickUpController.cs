using Main.Assets._Project.Scripts.Runtime.Input;
using Main.Assets._Project.Scripts.Runtime.PickUpItem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Assets._Project.Scripts.Runtime.Player
{
    public class PlayerPickUpController : MonoBehaviour
    {
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _maxDistanceRaycast;
        [SerializeField] private LayerMask _pickUpLayers;
        [SerializeField] private float _dropForce;
        private InputHandler _inputHandler;
        private Transform _equippedObject;
        private RaycastHit _hitInfo;

        public void Init(InputHandler inputHandler, Item startEquippedItem = null)
        {
            _inputHandler = inputHandler;

            if (startEquippedItem != null)
                EquipItem(startEquippedItem.transform);

            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _inputHandler.Input.Gameplay.PickUp.performed += ctx => PickUp();
            _inputHandler.Input.Gameplay.Drop.performed += ctx => Drop();
        }

        private void UnsubscribeOnEvents()
        {
            _inputHandler.Input.Gameplay.PickUp.performed -= ctx => PickUp();
            _inputHandler.Input.Gameplay.Drop.performed -= ctx => Drop();
        }

        private void PickUp()
        {
            if (_equippedObject != null)
                throw new System.Exception("Already equipped hand");

            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out _hitInfo, _maxDistanceRaycast, _pickUpLayers))
            {
                EquipItem(_hitInfo.transform);
            }
        }

        private void Drop()
        {
            if (_equippedObject == null)
                throw new System.Exception("No Item in Hand");

            UnequipItem();
        }

        private void EquipItem(Transform itemToEquip)
        {
            _equippedObject = itemToEquip;
            _equippedObject.GetComponent<Rigidbody>().isKinematic = true;
            _equippedObject.SetParent(_itemHolder);
            _equippedObject.SetLocalPositionAndRotation(Vector3.zero, _itemHolder.localRotation);
        }

        private void UnequipItem()
        {
            _equippedObject.SetParent(null);
            var rb = _equippedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;

            rb.AddForce(_equippedObject.forward * _dropForce, ForceMode.VelocityChange);
            _equippedObject = null;
        }
    }
}
