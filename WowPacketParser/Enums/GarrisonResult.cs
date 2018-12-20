﻿using System;

namespace WowPacketParser.Enums
{

    public enum GarrisonResult : int
    {
        Success                             = 0,
        NoGarrison                          = 1,
        GarrisonExists                      = 2,
        GarrisonSameTypeExists              = 3,
        InvalidGarrison                     = 4,
        InvalidGarrisonLevel                = 5,
        GarrisonLevelUnchanged              = 6,
        NotInGarrison                       = 7,
        NoBuilding                          = 8,
        BuildingExists                      = 9,
        InvalidPlotInstanceid               = 10,
        InvalidBuildingid                   = 11,
        InvalidUpgradeLevel                 = 12,
        UpgradeLevelExceedsGarrisonLevel    = 13,
        PlotsNotFull = 14,
        InvalidSiteId = 15,
        InvalidPlotBuilding = 16,
        InvalidFaction = 17,
        InvalidSpecialization = 18,
        SpecializationExists = 19,
        SpecializationOnCooldown = 20,
        BlueprintExists = 21,
        RequiresBlueprint = 22,
        InvalidDoodadSetId = 23,
        BuildingTypeExists = 24,
        BuildingNotActive = 25,
        ConstructionComplete = 26,
        FollowerExists = 27,
        InvalidFollower = 28,
        FollowerAlreadyOnMission = 29,
        FollowerInBuilding = 30,
        FollowerInvalidForBuilding = 31,
        InvalidFollowerLevel = 32,
        MissionExists = 33,
        InvalidMission = 34,
        InvalidMissionTime = 35,
        InvalidMissionRewardIndex = 36,
        MissionNotOffered = 37,
        AlreadyOnMission = 38,
        MissionSizeInvalid = 39,
        FollowerSoftCapExceeded = 40,
        NotOnMission = 41,
        AlreadyCompletedMission = 42,
        MissionNotComplete = 43,
        MissionRewardsPending = 44,
        MissionExpired = 45,
        NotEnoughCurrency = 46,
        NotEnoughGold = 47,
        BuildingMissing = 48,
        NoArchitect = 49,
        ArchitectNotAvailable = 50,
        NoMissionNpc = 51,
        MissionNpcNotAvailable = 52,
        InternalError = 53,
        InvalidStaticTableValue = 54,
        InvalidItemLevel = 55,
        InvalidAvailableRecruit = 56,
        FollowerAlreadyRecruited = 57,
        RecruitmentGenerationInProgress = 58,
        RecruitmentOnCooldown = 59,
        RecruitBlockedByGeneration = 60,
        RecruitmentNpcNotAvailable = 61,
        InvalidFollowerQuality = 62,
        ProxyNotOk = 63,
        RecallPortalUsedLessThan24HoursAgo = 64,
        OnRemoveBuildingSpellFailed = 65,
        OperationNotSupported = 66,
        FollowerFatigued = 67,
        UpgradeConditionFailed = 68,
        FollowerInactive = 69,
        FollowerActive = 70,
        FollowerActivationUnavailable = 71,
        FollowerTypeMismatch = 72,
        InvalidGarrisonType = 73,
        MissionStartConditionFailed = 74,
        InvalidFollowerAbility = 75,
        InvalidMissionBonusAbility = 76,
        HigherBuildingTypeExists = 77,
        AtFollowerHardCap = 78,
        FollowerCannotGainXp = 79,
        NoOp = 80,
        AtClassSpecCap = 81,
        MissionRequires100ToStart = 82,
        MissionMissingRequiredFollower = 83,
        InvalidTalent = 84,
        AlreadyResearchingTalent = 85,
        FailedCondition = 86,
        InvalidTier = 87,
        InvalidClass = 88
    }

}
