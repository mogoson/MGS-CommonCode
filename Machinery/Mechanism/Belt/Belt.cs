﻿/*************************************************************************
 *  Copyright © 2017-2018 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  Belt.cs
 *  Description  :  Define Belt component.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  6/22/2017
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Belt with UV animation.
    /// </summary>
    [AddComponentMenu("MGS/Machinery/Belt")]
    [RequireComponent(typeof(Renderer))]
    public class Belt : Mechanism, IEngageMechanism
    {
        #region Field and Property
        /// <summary>
        /// Engage mechanisms.
        /// </summary>
        [Tooltip("Engage mechanisms.")]
        [SerializeField]
        protected List<Mechanism> engages;

        /// <summary>
        /// Coefficient of velocity.
        /// </summary>
        [Tooltip("Coefficient of velocity.")]
        public float coefficient = 1;

        /// <summary>
        /// Mechanism is stuck?
        /// </summary>
        public override bool IsStuck
        {
            get
            {
                if (CheckEngageStuck())
                {
                    return true;
                }
                return base.IsStuck;
            }
        }

        /// <summary>
        /// Renderer of belt.
        /// </summary>
        protected Renderer beltRenderer;
        #endregion

        #region Protected Method
        /// <summary>
        /// Check one of the engages is stuck?
        /// </summary>
        /// <returns>One of the engages is stuck?</returns>
        protected bool CheckEngageStuck()
        {
            foreach (var engage in engages)
            {
                if (engage.IsStuck)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Drive mechanism by velocity.
        /// </summary>
        /// <param name="velocity">Velocity of drive.</param>
        /// <param name="mode">Mode of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected override bool OnDrive(float velocity, DriveMode mode = DriveMode.Ignore)
        {
            beltRenderer.material.mainTextureOffset += new Vector2(velocity * coefficient * Time.deltaTime, 0);
            return DriveEngages(-velocity);
        }

        /// <summary>
        /// Drive engage mechanisms by linear velocity.
        /// </summary>
        /// <param name="velocity">Linear velocity of drive.</param>
        /// <returns>Drive is unrestricted?</returns>
        protected bool DriveEngages(float velocity)
        {
            foreach (var engage in engages)
            {
                if (!engage.Drive(velocity, DriveMode.Linear))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Initialize belt.
        /// </summary>
        public override void Initialize()
        {
            beltRenderer = GetComponent<Renderer>();
        }

        /// <summary>
        /// Link engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void LinkEngage(IMechanism mechanism)
        {
            var engage = mechanism as Mechanism;
            if (engage == null || engages.Contains(engage))
            {
                return;
            }

            engages.Add(engage);
        }

        /// <summary>
        /// Break engage.
        /// </summary>
        /// <param name="mechanism">Target mechanism.</param>
        public void BreakEngage(IMechanism mechanism)
        {
            engages.Remove(mechanism as Mechanism);
        }
        #endregion
    }
}