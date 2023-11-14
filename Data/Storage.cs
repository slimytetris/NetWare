﻿using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NetWare
{
    public class Storage : MonoBehaviour
    {
        public static PlayerController[] players = { };

        public static IEnumerator Update()
        {
            while (true)
            {
                players = FindObjectsOfType<PlayerController>();

                yield return new WaitForSeconds(1);
            }
        }
    }
}
