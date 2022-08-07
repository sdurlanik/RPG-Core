using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float _weaponRange = 2f;
        private Transform _target;
        private Mover _mover;
        private Animator _animator;
        private ActionScheduler _actionScheduler;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _animator = GetComponent<Animator>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_target == null) return; 

            if (GetDistance())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        private bool GetDistance()
        {
            return Vector3.Distance(transform.position, _target.position) >= _weaponRange;
        }


        public void AttackBehaviour()
        {
            _animator.SetTrigger("attack");
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);

            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }

        void Hit()
        {
            print("Hit");
        }
    }

}