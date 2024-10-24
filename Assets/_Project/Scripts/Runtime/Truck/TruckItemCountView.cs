using TMPro;
using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.Truck
{
    public class TruckItemCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _itemsCountView;
        [SerializeField] private Transform _rotateTarget;

        public void UpdateCount(int currentCount)
        {
            _itemsCountView.text = $"Count: {currentCount}";
        }

        private void Update()
        {
            Vector3 directionToTarget = _rotateTarget.position - _itemsCountView.transform.position;
            directionToTarget.y = 0;
    
            if (directionToTarget != Vector3.zero)
            {
                _itemsCountView.transform.rotation = Quaternion.LookRotation(directionToTarget);
    
                _itemsCountView.transform.Rotate(0, 180 , 0);
            }
        }
    }
}
