using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("world_quest")] 
    public sealed class WorldQuest : IDataModel
    {
        [DBFieldName("id", true)]
        public uint? id;

        [DBFieldName("duration")]
        public uint? duration;

        [DBFieldName("variable")]
        public int? variable;

        [DBFieldName("value")]
        public int? value;
    }
}
