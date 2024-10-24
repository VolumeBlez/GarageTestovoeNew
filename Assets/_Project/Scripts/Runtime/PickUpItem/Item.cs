using UnityEngine;

namespace Main.Assets._Project.Scripts.Runtime.PickUpItem
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour
    {
        void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("PickUpItem");
        }
    }
}
