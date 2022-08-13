using System;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{

    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (InterractWithCombat()) return;
            if (InterractWithMovement()) return;
        }

        private bool InterractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit raycastHit in hits)
            {
                CombatTarget target = raycastHit.transform.GetComponent<CombatTarget>();
                if (!GetComponent<Fighter>().CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }

                return true;
            }

            return false;
        }

        private bool InterractWithMovement()
        {

            // Yeni bir ışın oluşturulur
            // Camera.main : MainCamera
            // ScreenPointToRay : 2D ekrandaki tıklanan noktayı Ray tipine çevirir
            // Input.mousePosition : Mouse'un pozisyonu
            RaycastHit hit;

            // Işın bir collidera çarptıysa hasHit true olur
            // out hit : çarpışan colliderın bilgilerini alır
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                // Playerın navmesh komponentini alır ve hedef noktasını hit.point olarak ayarlar
                // hit.point : çarpışan colliderın kordinatı 

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);

                }

                return true;
            }

            return false;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}