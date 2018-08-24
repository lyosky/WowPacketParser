﻿using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_line_template")]
    public sealed class ConversationLineTemplate : IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("StartTime")]
        public uint? StartTime;

        [DBFieldName("UiCameraID")]
        public uint? UiCameraID;

        [DBFieldName("ActorIdx")]
        public byte? ActorIdx;

        [DBFieldName("Unk")]
        public byte? Flags;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
