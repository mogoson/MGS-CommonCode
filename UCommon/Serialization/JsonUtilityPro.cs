﻿/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  JsonUtilityPro.cs
 *  Description  :  JsonUtility Pro.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  4/20/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace MGS.UCommon.Serialization
{
    /// <summary>
    /// JsonUtility Pro.
    /// </summary>
    public sealed class JsonUtilityPro
    {
        /// <summary>
        /// Deserialize List from json.
        /// </summary>
        /// <typeparam name="T">Type of List item.</typeparam>
        /// <param name="json">Json text.</param>
        /// <returns>List object.</returns>
        public static List<T> FromJson<T>(string json)
        {
            var avatar = JsonUtility.FromJson<ListAvatar<T>>(json);
            if (avatar == null)
            {
                return null;
            }

            return avatar.Source;
        }

        /// <summary>
        /// Serialize List to json.
        /// </summary>
        /// <typeparam name="T">Type of List item.</typeparam>
        /// <param name="list">Source list.</param>
        /// <returns>Json text.</returns>
        public static string ToJson<T>(List<T> list)
        {
            var avatar = new ListAvatar<T>(list);
            return JsonUtility.ToJson(avatar);
        }

        /// <summary>
        /// Deserialize Dictionary from json.
        /// </summary>
        /// <typeparam name="TKey">Type of Dictionary key.</typeparam>
        /// <typeparam name="TValue">Type of Dictionary velue.</typeparam>
        /// <param name="json">Json text.</param>
        /// <returns>Dictionary object.</returns>
        public static Dictionary<TKey, TValue> FromJson<TKey, TValue>(string json)
        {
            var avatar = JsonUtility.FromJson<DictionaryAvatar<TKey, TValue>>(json);
            if (avatar == null)
            {
                return null;
            }

            return avatar.Source;
        }

        /// <summary>
        /// Serialize Dictionary to json.
        /// </summary>
        /// <typeparam name="TKey">Type of Dictionary key.</typeparam>
        /// <typeparam name="TValue">Type of Dictionary velue.</typeparam>
        /// <param name="dictionary">Source dictionary.</param>
        /// <returns>Json text.</returns>
        public static string ToJson<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
        {
            var avatar = new DictionaryAvatar<TKey, TValue>(dictionary);
            return JsonUtility.ToJson(avatar);
        }
    }
}