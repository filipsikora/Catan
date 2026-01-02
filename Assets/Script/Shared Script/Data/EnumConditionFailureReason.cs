namespace Catan.Shared.Data
{
    public enum ConditionFailureReason
    {
        None,
        CannotAfford,
        PositionOccupied,
        NoAccess,
        TooCloseToSettlement,
        NoBuildingsAvailable,
        NotOwner,
        DoesNotExist,
        NoDevelopmentCardsLeft,
        NoResourceCardsLeft,
        NotRolledYet,
        CardIsNew,
        NotKnightOrAfterRoll,
        AlreadyUsed
    }
}