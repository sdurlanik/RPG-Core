using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Mover : MonoBehaviour
{
    
    // Hedef transform
    [SerializeField] private Transform _target;


    void Update()
    {
        // Fare sol tık
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }

    }
    
    // Ekranda tıklandığı noktaya hareket
    private void MoveToCursor()
    {
        // Yeni bir ışın oluşturulur
        // Camera.main : MainCamera
        // ScreenPointToRay : 2D ekrandaki tıklanan noktayı Ray tipine çevirir
        // Input.mousePosition : Mouse'un pozisyonu
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        // Işın bir collidera çarptıysa hasHit true olur
        // out hit : çarpışan colliderın bilgilerini alır
        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            // Playerın navmesh komponentini alır ve hedef noktasını hit.point olarak ayarlar
            // hit.point : çarpışan colliderın kordinatı 
            GetComponent<NavMeshAgent>().destination = hit.point;
        }

    }
}
