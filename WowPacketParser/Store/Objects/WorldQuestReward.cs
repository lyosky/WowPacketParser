using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("world_quest_reward")]
    public sealed class WorldQuestReward : IDataModel
    {
        [DBFieldName("id", true)]
        public int? id;

        [DBFieldName("questType", true)]
        public int? questType;

        //'ITEM','CURRENCY','GOLD'
        [DBFieldName("rewardType", true)]
        public byte? rewardType;

        [DBFieldName("rewardId", true)]
        public int? rewardId;

        [DBFieldName("rewardCount")]
        public int? rewardCount;

        [DBFieldName("rewardContext")]
        public int? rewardContext;
    }
}
