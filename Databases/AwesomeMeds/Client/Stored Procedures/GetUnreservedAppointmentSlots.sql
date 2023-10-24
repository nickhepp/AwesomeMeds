CREATE PROCEDURE [Client].[GetUnreservedAppointmentSlots]
AS
BEGIN
    SET NOCOUNT ON;

    -- Get appointment slots from [Provider].[AppointmentSlot] that are not in [Client].[PendingReservationAppointmentSlot] or [Client].[ConfirmedAppointmentSlot]
    SELECT 
        [Provider].[AppointmentSlot].[Year],
        [Provider].[AppointmentSlot].[Month],
        [Provider].[AppointmentSlot].[Day],
        [Provider].[AppointmentSlot].[Hour],
        [Provider].[AppointmentSlot].[QuarterHourSegment]
    FROM [Provider].[AppointmentSlot]
    -- look for already pending reservation appts
    LEFT JOIN [Client].[PendingReservationAppointmentSlot] AS Pending ON
        [Provider].[AppointmentSlot].[ProviderID] = Pending.[ProviderID] AND
        [Provider].[AppointmentSlot].[Year] = Pending.[Year] AND
        [Provider].[AppointmentSlot].[Month] = Pending.[Month] AND
        [Provider].[AppointmentSlot].[Day] = Pending.[Day] AND
        [Provider].[AppointmentSlot].[Hour] = Pending.[Hour] AND
        [Provider].[AppointmentSlot].[QuarterHourSegment] = Pending.[QuarterHourSegment]
    -- look for confirmed reservation appts
    LEFT JOIN [Client].[ConfirmedAppointmentSlot] AS Confirmed ON
        [Provider].[AppointmentSlot].[ProviderID] = Confirmed.[ProviderID] AND
        [Provider].[AppointmentSlot].[Year] = Confirmed.[Year] AND
        [Provider].[AppointmentSlot].[Month] = Confirmed.[Month] AND
        [Provider].[AppointmentSlot].[Day] = Confirmed.[Day] AND
        [Provider].[AppointmentSlot].[Hour] = Confirmed.[Hour] AND
        [Provider].[AppointmentSlot].[QuarterHourSegment] = Confirmed.[QuarterHourSegment]
    WHERE Pending.[ProviderID] IS NULL AND Confirmed.[ProviderID] IS NULL -- we only want the APPT if its NOT reserved already
    -- we dont care which provider or how many are available during a specific segment, we just need to know if there are 0 or more available,
    -- hence this group by
    GROUP BY 
        [Provider].[AppointmentSlot].[Year],
        [Provider].[AppointmentSlot].[Month],
        [Provider].[AppointmentSlot].[Day],
        [Provider].[AppointmentSlot].[Hour],
        [Provider].[AppointmentSlot].[QuarterHourSegment]
END;
