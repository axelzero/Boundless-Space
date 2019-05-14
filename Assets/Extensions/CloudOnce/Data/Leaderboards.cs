// <copyright file="Leaderboards.cs" company="Jan Ivar Z. Carlsen, Sindri Jóelsson">
// Copyright (c) 2016 Jan Ivar Z. Carlsen, Sindri Jóelsson. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace CloudOnce
{
    using System.Collections.Generic;
    using Internal;

    /// <summary>
    /// Provides access to leaderboards registered via the CloudOnce Editor.
    /// This file was automatically generated by CloudOnce. Do not edit.
    /// </summary>
    public static class Leaderboards
    {
        private static readonly UnifiedLeaderboard s_richestman = new UnifiedLeaderboard("Richestman",
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_TVOS)
            ""
#elif !UNITY_EDITOR && UNITY_ANDROID && CLOUDONCE_GOOGLE
            "CgkIuM6xmOUFEAIQBw"
#else
            "Richestman"
#endif
            );

        public static UnifiedLeaderboard Richestman
        {
            get { return s_richestman; }
        }

        private static readonly UnifiedLeaderboard s_hightScore = new UnifiedLeaderboard("HightScore",
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_TVOS)
            ""
#elif !UNITY_EDITOR && UNITY_ANDROID && CLOUDONCE_GOOGLE
            "CgkIuM6xmOUFEAIQCA"
#else
            "HightScore"
#endif
            );

        public static UnifiedLeaderboard HightScore
        {
            get { return s_hightScore; }
        }

        public static string GetPlatformID(string internalId)
        {
            return s_leaderboardDictionary.ContainsKey(internalId)
                ? s_leaderboardDictionary[internalId].ID
                : string.Empty;
        }

        private static readonly Dictionary<string, UnifiedLeaderboard> s_leaderboardDictionary = new Dictionary<string, UnifiedLeaderboard>
        {
            { "Richestman", s_richestman },
            { "HightScore", s_hightScore },
        };
    }
}
