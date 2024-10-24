using Main.Assets._Project.Scripts.Runtime.PickUpItem;
using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.Truck
{
    [RequireComponent(typeof(Collider))]
    public class TruckItemCountController : MonoBehaviour
    {
        [SerializeField] private TruckItemCountView _view;
        private int _countItemsInTruck;

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<Item>() != null)
            {
                _countItemsInTruck++;
                _view?.UpdateCount(_countItemsInTruck);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<Item>() != null)
            {
                _countItemsInTruck--;
                _view?.UpdateCount(_countItemsInTruck);
            }
        }
    }
}
