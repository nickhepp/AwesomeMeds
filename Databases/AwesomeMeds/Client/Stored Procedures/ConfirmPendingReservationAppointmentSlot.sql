CREATE PROCEDURE [Client].[ConfirmPendingReservationAppointmentSlot]
    @ClientID UNIQUEIDENTIFIER,
    @ConfirmationDateTime DATETIME,
    @Year INT,
    @Month INT,
    @Day INT,
    @Hour INT,
    @QuarterHourSegment INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Added INT = 0; -- Initialize the return value to 0

    BEGIN TRANSACTION; -- Start a new transaction

    -- Check if there is a pending reservation that can be confirmed
    IF EXISTS (
        SELECT TOP 1 1
        FROM [Client].[PendingReservationAppointmentSlot] AS PR
        WHERE PR.[ClientID] = @ClientID
        AND @ConfirmationDateTime <= PR.[ReservationConfirmedByUTC]
        AND PR.[Year] = @Year
        AND PR.[Month] = @Month
        AND PR.[Day] = @Day
        AND PR.[Hour] = @Hour
        AND PR.[QuarterHourSegment] = @QuarterHourSegment
        AND NOT EXISTS (
            SELECT 1
            FROM [Client].[ConfirmedAppointmentSlot] AS CA
            WHERE CA.[ProviderID] = PR.[ProviderID]
            AND CA.[Year] = PR.[Year]
            AND CA.[Month] = PR.[Month]
            AND CA.[Day] = PR.[Hour]
            AND CA.[Hour] = PR.[Hour]
            AND CA.[QuarterHourSegment] = PR.[QuarterHourSegment]
        )
    )
    BEGIN
        -- Insert the pending reservation into [Client].[ConfirmedAppointmentSlot]
        INSERT INTO [Client].[ConfirmedAppointmentSlot] (
            [ProviderID],
            [Year],
            [Month],
            [Day],
            [Hour],
            [QuarterHourSegment],
            [ClientID],
            [ReservationCreatedUTC],
            [ReservationConfirmedByUTC],
            [ReservationConfirmedUTC]
        )
        SELECT TOP 1
            PR.[ProviderID],
            PR.[Year],
            PR.[Month],
            PR.[Day],
            PR.[Hour],
            PR.[QuarterHourSegment],
            PR.[ClientID],
            PR.[ReservationCreatedUTC],
            PR.[ReservationConfirmedByUTC],
            @ConfirmationDateTime
        FROM [Client].[PendingReservationAppointmentSlot] AS PR
        WHERE PR.[ClientID] = @ClientID
        AND @ConfirmationDateTime <= PR.[ReservationConfirmedByUTC]
        AND PR.[Year] = @Year
        AND PR.[Month] = @Month
        AND PR.[Day] = @Day
        AND PR.[Hour] = @Hour
        AND PR.[QuarterHourSegment] = @QuarterHourSegment
        AND NOT EXISTS (
            SELECT 1
            FROM [Client].[ConfirmedAppointmentSlot] AS CA
            WHERE CA.[ProviderID] = PR.[ProviderID]
            AND CA.[Year] = PR.[Year]
            AND CA.[Month] = PR.[Month]
            AND CA.[Day] = PR.[Hour]
            AND CA.[Hour] = PR.[Hour]
            AND CA.[QuarterHourSegment] = PR.[QuarterHourSegment]
        );

        -- Set the return value to 1 to indicate success
        SET @Added = 1;
    END

    IF @Added = 1
    BEGIN
        COMMIT; -- Commit the transaction if an appointment is successfully confirmed and added
    END
    ELSE
    BEGIN
        ROLLBACK; -- Roll back the transaction if no appointment is added
    END

    -- Return the result (1 if added, 0 if not added)
    SELECT @Added;
END;