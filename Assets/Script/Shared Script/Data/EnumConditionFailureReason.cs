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
        NoPosition,
        NoDevelopmentCardsLeft,
        NoResourceCardsLeft,
        NotRolledYet
    }
}