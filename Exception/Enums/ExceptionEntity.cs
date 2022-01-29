namespace GOfit.MyGOfit.ExceptionMiddleware.Enums
{
    public enum ExceptionEntity
    {
        Unknown = 0,
        DatabaseError,

        // Wallet Service
        WithoutBalance,

        // ExtraServices
        InstallationReserved,
        // User
        User,
        // Camp
        Camp,
        CancelledInscription,
        // Nutrition
        NutritionFields,
        Nutrition,
        AgeGroup,

        Child,
        Center,
        ActivityType,
        Product,
        ClientType,
        Inscription,
        PostalCode,
        ParentUser,
        CampAgeGroup,
        EmailCampInscription,
        Amount,
        OpeningHour,
        Limitation,
        ExtraModifierCamp,
        BillingDataInscription,
        AmountProduct,
        AmountExtra,
        ExtraModifierCampInscriptions,
        InscriptionStepOne,
        InscriptionStepTwo,
        InscriptionStepThree,
        InscriptionStepFour,
        Payment,
        PaymentRefund,

        //Payment,
        PaymentGateway,
        PaymentConfig,
        UserRoleStaff,
        PaymentItem,
        CenterOfPayment,
        //CenterOfPayment,
        //Center,
        BillingDataUsers,
        Transactions,
        //User,
        Wallet,
        UserToken,
        HttpContext,

        SportFacility,
        ChildHistoric,
        Booking,
        ProcessTimeout,
        DoesntProgress,
        BillingData,
        PendingPayment,
        BillingDataUser,
    }
}
