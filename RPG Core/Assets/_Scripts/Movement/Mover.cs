using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace RPG.Movement
{
    

    public class Mover : MonoBehaviour, IAction
    {
        private static readonly int ForwardSpeed = Animator.StringToHash("forwardSpeed");

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Fighter _fighter;
        private ActionScheduler _actionScheduler;

        private Health health;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _fighter = GetComponent<Fighter>();
            _actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            _navMeshAgent.enabled = !health.IsDead();
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            // NavMeshin velocitysini alır   
            Vector3 velocity = _navMeshAgent.velocity;

            // Global olan velocity değerini locale çevirir (Animatör üzerinde kullanmak için)
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            // Hız olarak sadece Z eksenindeki hareketi alınır
            float speed = localVelocity.z;

            // Animatörün forwardSpeed değişkenine hız değerini atar
           _animator.SetFloat(ForwardSpeed, speed);
        }

        public void StartMoveAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            _fighter.Cancel();
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        
    }
}
