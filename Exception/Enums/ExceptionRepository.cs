namespace GOfit.MyGOfit.ExceptionMiddleware.Enums
{
    /// <summary>
    /// Exception repository
    /// </summary>
    public enum ExceptionRepository
    {
        Unknown = 0,
        InvalidViewModel,
        InvalidPropertyToSort,
        InvalidPropertyToFilter,
        HttpContextIsNull,
        DbProviderNotConfigured,
        NotFound,
        IsNotActive,
        AlreadyExists,
        CantHaveMoreItems,
        InvalidTimeframe,
        OverlappingWatch,
        NeedMoreInformation,
        CantHaveDiferents,
        ThisTypeCantBeDeleted,
        RoleWithUsersCantBeDeleted,
        AlreadyFareExist,
        ConditionsToBeDeletedFailed,
        cantSendEmail,
        NoDataBaseAmount,
        ChildNotBorn,

        AlreadyRefunded,
        NotImplemented,

        DoesntProgress,
        ProcessTimeout,
        BillingData,
        Booking,
        ChildHistoric,
        PendingPayment,

        PaymentIntent,
    }
}
