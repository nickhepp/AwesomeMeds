CREATE PROCEDURE [Client].[DeleteUnconfirmedPendingReservations]
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @CurrentDateTime AS DATETIME = GETUTCDATE()

    -- Delete entries from [Client].[PendingReservationAppointmentSlot] that meet the criteria
    DELETE FROM [Client].[PendingReservationAppointmentSlot]
    WHERE 
        [ReservationConfirmedByUTC] <= @CurrentDateTime -- Current datetime is later than [ReservationConfirmedByUTC]
        AND NOT EXISTS (
            SELECT 1
            FROM [Client].[ConfirmedAppointmentSlot]
            WHERE
                [Client].[ConfirmedAppointmentSlot].[ProviderID] = [Client].[PendingReservationAppointmentSlot].[ProviderID] AND
                [Client].[ConfirmedAppointmentSlot].[Year] = [Client].[PendingReservationAppointmentSlot].[Year] AND
                [Client].[ConfirmedAppointmentSlot].[Month] = [Client].[PendingReservationAppointmentSlot].[Month] AND
                [Client].[ConfirmedAppointmentSlot].[Day] = [Client].[PendingReservationAppointmentSlot].[Day] AND
                [Client].[ConfirmedAppointmentSlot].[Hour] = [Client].[PendingReservationAppointmentSlot].[Hour] AND
                [Client].[ConfirmedAppointmentSlot].[QuarterHourSegment] = [Client].[PendingReservationAppointmentSlot].[QuarterHourSegment]
        );

END;
