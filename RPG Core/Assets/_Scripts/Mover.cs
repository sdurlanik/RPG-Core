using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Mover : MonoBehaviour
{
    private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");


    void Update()
    {
        // Fare sol tık
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimation();
        

    }

    private void UpdateAnimation()
    {
        // NavMeshin velocitysini alır   
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // Global olan velocity değerini locale çevirir (Animatör üzerinde kullanmak için)
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        
        // Hız olarak sadece Z eksenindeki hareketi alınır
        float speed = localVelocity.z;
        
        // Animatörün forwardSpeed değişkenine hız değerini atar
        GetComponent<Animator>().SetFloat(ForwardSpeed, speed);
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
