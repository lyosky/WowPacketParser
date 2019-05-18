﻿using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_actors")]
    public sealed class ConversationActor : IDataModel
    {
        [DBFieldName("ConversationId", true)]
        public uint? ConversationId;

        [DBFieldName("ConversationActorId", true)]
        public uint? ConversationActorId;

        [DBFieldName("ConversationActorNearId", true)]
        public uint? ConversationActorNearId;

        [DBFieldName("Idx", true)]
        public uint? Idx;

        public WowGuid128 Guid;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
