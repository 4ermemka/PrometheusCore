using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Shared.View
{
    public class UnityMainThread : MonoBehaviour
    {
        public static UnityMainThread wkr;
        Queue<Action> jobs = new Queue<Action>();

        void Awake()
        {
            wkr = this;
        }

        void Update()
        {
            while (jobs.Count > 0)
                jobs.Dequeue().Invoke();
        }

        internal void AddJob(Action newJob)
        {
            jobs.Enqueue(newJob);
        }
    }
}

