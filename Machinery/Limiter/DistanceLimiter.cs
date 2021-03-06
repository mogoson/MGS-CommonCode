﻿/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DistanceLimiter.cs
 *  Description  :  Distance limiter.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/29/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UCommon.Generic;
using UnityEngine;

namespace MGS.Machinery
{
    /// <summary>
    /// Distance limiter.
    /// </summary>
    [AddComponentMenu("MGS/Machinery/DistanceLimiter")]
    public class DistanceLimiter : Limiter
    {
        #region Field And Property
        /// <summary>
        /// Distance range of limiter.
        /// </summary>
        [Tooltip("Distance range of limiter.")]
        public Range range = new Range(1, 10);

        /// <summary>
        /// First anchor of limiter.
        /// </summary>
        [Tooltip("Distance range of limiter.")]
        public Transform firstAnchor;

        /// <summary>
        /// Second anchor of limiter.
        /// </summary>
        [Tooltip("Distance range of limiter.")]
        public Transform secondAnchor;

        /// <summary>
        /// Limiter is triggered?
        /// </summary>
        public override bool IsTriggered
        {
            get
            {
                var currentDis = CurrentDistance;
                return currentDis <= range.min || currentDis >= range.max;
            }
        }

        /// <summary>
        /// Current distance from first anchor to second anchor.
        /// </summary>
        public float CurrentDistance
        {
            get { return Vector3.Distance(firstAnchor.position, secondAnchor.position); }
        }
        #endregion
    }
}