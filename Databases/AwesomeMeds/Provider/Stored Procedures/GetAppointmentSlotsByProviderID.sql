CREATE PROCEDURE [Provider].[GetAppointmentSlotsByProviderID]
    @ProviderID UNIQUEIDENTIFIER
AS
BEGIN
    SELECT
        [ProviderID],
        [Year],
        [Month],
        [Day],
        [Hour],
        [QuarterHourSegment]
    FROM
        [Provider].[AppointmentSlot]
    WHERE
        [ProviderID] = @ProviderID;
END;
