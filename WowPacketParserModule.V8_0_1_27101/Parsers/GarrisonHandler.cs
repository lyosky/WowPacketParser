using System.Reflection.Emit;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GarrisonHandler
    {
        public static void ReadUnk3ChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32("Unk1", indexes);
            packet.ReadInt32("Unk2", indexes);
            packet.ReadTime("UnkTime3", indexes);
            packet.ReadTime("UnkTime4", indexes);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadTime("UnkTime5", indexes);
            packet.ResetBitReader();
            packet.ReadBit("UnkBit", indexes);
        }

        public static void ReadUnk2ChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadPackedGuid128("UnkGuid", indexes);
            packet.ReadPackedGuid128("UnkGuid2", indexes);
            packet.ReadUInt32("Unk1", indexes);
            packet.ReadUInt32("Unk2", indexes);
            packet.ReadInt16("Unk3", indexes);
            packet.ReadInt16("Unk4", indexes);
            packet.ReadInt32("Unk5", indexes);
        }

        public static void ReadUnkChallengeModeMapStats(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadInt32("Unk1", indexes);
            packet.ReadUInt32("Unk2", indexes);
            packet.ReadInt32("Unk3", indexes);
            packet.ReadTime("UnkTime", indexes);
            packet.ReadTime("UnkTime", indexes);

            for (int i = 0; i < 4; i++)
                packet.ReadUInt32("Unk6", indexes, i);

            var unkCount = packet.ReadUInt32("UnkCount", indexes);
            for (int i = 0; i < unkCount; i++)
                ReadUnk2ChallengeModeMapStats(packet, indexes, i);

            packet.ResetBitReader();
            packet.ReadBit("UnkBit", indexes);
        }

        [Parser(Opcode.CMSG_REQUEST_CHALLENGE_MODE_AFFIXES)]
        public static void HandleChallengeModeZero(Packet packet) { }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_REWARDS)]
        public static void HandleChallengeModeRewards(Packet packet)
        {
            packet.ReadBit("IsWeeklyRewardAvailable");
            packet.ReadInt32("LastWeekHighestKeyCompleted");
            packet.ReadInt32("LastWeekMapChallengeKeyEntry");
            packet.ReadInt32("CurrentWeekHighestKeyCompleted");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("UnkInt"); // always 13 for me
        }

        [Parser(Opcode.SMSG_CHALLENGE_MODE_ALL_MAP_STATS)]
        public static void HandleChallengeModeAllMapStats(Packet packet)
        {
            var unkCount = packet.ReadUInt32("Unk1");
            var unkCount2 = packet.ReadUInt32("Unk2");
            var unkCount3 = packet.ReadUInt32("Unk3");
            packet.ReadInt32("Unk5");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                packet.ReadInt32("Unk6");

            for (int i = 0; i < unkCount; i++)
                ReadUnkChallengeModeMapStats(packet, i);

            for (int i = 0; i < unkCount2; i++)
            {
                packet.ReadInt32("Unk7", i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_1_0_28724))
                    packet.ReadInt32("Unk8", i);
                ReadUnkChallengeModeMapStats(packet, i);
            }

            for (int i = 0; i < unkCount3; i++)
                ReadUnk3ChallengeModeMapStats(packet, i);
        }

        public static void ReadGarrisonMissionOvermaxRewards(uint rewardsCount, Packet packet, params object[] indexes)
        {
            int[] counts = new int[rewardsCount];
            for (int i = 0; i < rewardsCount; i++)
                counts[i] = packet.ReadInt32("MissionRewardCount", indexes, i);
            for (int i = 0; i < rewardsCount; i++)
                for (int j = 0; j < counts[i]; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMissionReward(packet, indexes, i, j);
        }

        [Parser(Opcode.CMSG_GARRISON_CHECK_UPGRADEABLE)]
        [Parser(Opcode.CMSG_GARRISON_REQUEST_CLASS_SPEC_CATEGORY_INFO)]
        public static void HandleGarrisonGarrSiteID(Packet packet)
        {
            packet.ReadInt32("GarrSiteID");
        }

        [Parser(Opcode.SMSG_GET_GARRISON_INFO_RESULT, ClientVersionBuild.V8_1_5_29683)]
        public static void HandleGetGarrisonInfoResult(Packet packet)
        {
            packet.ReadInt32("FactionIndex");
            var garrisonCount = packet.ReadUInt32("GarrisonCount");

            var followerSoftcapCount = packet.ReadUInt32("FollowerSoftCapCount");
            for (var i = 0u; i < followerSoftcapCount; ++i)
                V7_0_3_22248.Parsers.GarrisonHandler.ReadFollowerSoftCapInfo(packet, i);

            for (int i = 0; i < garrisonCount; i++)
            {
                packet.ReadInt32E<GarrisonType>("GarrTypeID", i);
                packet.ReadInt32E<GarrisonSite>("GarrSiteID", i);
                packet.ReadInt32E<GarrisonSiteLevel>("GarrSiteLevelID", i);

                var garrisonBuildingInfoCount = packet.ReadUInt32("GarrisonBuildingInfoCount", i);
                var garrisonPlotInfoCount = packet.ReadUInt32("GarrisonPlotInfoCount", i);
                var garrisonFollowerCount = packet.ReadUInt32("GarrisonFollowerCount", i);
                var garrisonMissionCount = packet.ReadUInt32("GarrisonMissionCount", i);
                var garrisonMissionRewardsCount = packet.ReadUInt32("GarrisonMissionRewardsCount", i);
                var garrisonMissionOvermaxRewardsCount = packet.ReadUInt32("GarrisonMissionOvermaxRewardsCount", i);
                var areaBonusCount = packet.ReadUInt32("GarrisonMissionAreaBonusCount", i);
                var talentsCount = packet.ReadUInt32("Talents", i);
                var canStartMissionCount = packet.ReadUInt32("CanStartMission", i);
                var archivedMissionsCount = packet.ReadUInt32("ArchivedMissionsCount", i);

                packet.ReadInt32("NumFollowerActivationsRemaining", i);
                packet.ReadUInt32("NumMissionsStartedToday", i);

                for (int j = 0; j < garrisonPlotInfoCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonPlotInfo(packet, "PlotInfo", i, j);

                for (int j = 0; j < garrisonMissionCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonMission(packet, "Mission", i, j);

                ReadGarrisonMissionOvermaxRewards(garrisonMissionRewardsCount, packet, "MissionRewards", i);
                ReadGarrisonMissionOvermaxRewards(garrisonMissionOvermaxRewardsCount, packet, "MissionOvermaxRewards", i);

                for (int j = 0; j < areaBonusCount; j++)
                    V6_0_2_19033.Parsers.GarrisonHandler.ReadGarrisonMissionAreaBonus(packet, "MissionAreaBonus", i, j);

                for (int j = 0; j < talentsCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonTalents(packet, "Talents", i, j);

                for (int j = 0; j < archivedMissionsCount; j++)
                    packet.ReadInt32("ArchivedMissions", i, j);

                for (int j = 0; j < garrisonBuildingInfoCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonBuildingInfo(packet, "BuildingInfo", i, j);

                packet.ResetBitReader();

                for (int j = 0; j < canStartMissionCount; j++)
                    packet.ReadBit("CanStartMission", i, j);

                packet.ResetBitReader();

                for (int j = 0; j < garrisonFollowerCount; j++)
                    V7_0_3_22248.Parsers.GarrisonHandler.ReadGarrisonFollower(packet, "Follower", i, j);
            }
        }
    }
}
