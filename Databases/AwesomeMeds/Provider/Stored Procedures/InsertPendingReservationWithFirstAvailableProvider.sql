-- Create a stored procedure to insert an entry into [Client].[PendingReservationAppointmentSlot] with the first available [ProviderID]
CREATE PROCEDURE [Client].[InsertPendingReservationWithFirstAvailableProvider]
    @Year INT,
    @Month INT,
    @Day INT,
    @Hour INT,
    @QuarterHourSegment INT,
    @ClientID UNIQUEIDENTIFIER,
    @ReservationCreatedUTC DATETIME,
    @ReservationConfirmedByUTC DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION; -- Start a new transaction

    DECLARE @AvailableProviderID UNIQUEIDENTIFIER;

    -- Get the first available [ProviderID] that doesn't exist in [Client].[PendingReservationAppointmentSlot]
    SELECT TOP 1 @AvailableProviderID = [Provider].[AppointmentSlot].[ProviderID]
    FROM [Provider].[AppointmentSlot]
    WHERE [Provider].[AppointmentSlot].[ProviderID] NOT IN (
        SELECT [ProviderID]
        FROM [Client].[PendingReservationAppointmentSlot]
    )
    ORDER BY [Provider].[AppointmentSlot].[ProviderID];

    -- Check if @AvailableProviderID is NULL, and if so, raise an error
    IF (@AvailableProviderID IS NULL)
    BEGIN
        ROLLBACK; -- Roll back the transaction
        RAISERROR('No available ProviderID found.', 16, 1);
        RETURN;
    END

    -- Insert the entry into [Client].[PendingReservationAppointmentSlot] with the available [ProviderID]
    INSERT INTO [Client].[PendingReservationAppointmentSlot] (
        [ProviderID],
        [Year],
        [Month],
        [Day],
        [Hour],
        [QuarterHourSegment],
        [ClientID],
        [ReservationCreatedUTC],
        [ReservationConfirmedByUTC]
    )
    VALUES (
        @AvailableProviderID,
        @Year,
        @Month,
        @Day,
        @Hour,
        @QuarterHourSegment,
        @ClientID,
        @ReservationCreatedUTC,
        @ReservationConfirmedByUTC
    );
    COMMIT; -- Commit the transaction
END;
