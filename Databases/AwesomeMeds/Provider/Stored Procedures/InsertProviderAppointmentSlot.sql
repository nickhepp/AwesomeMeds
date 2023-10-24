CREATE PROCEDURE [Provider].[InsertAppointmentSlot]
    @ProviderID UNIQUEIDENTIFIER,
    @Year INT,
    @Month INT,
    @Day INT,
    @Hour INT,
    @QuarterHourSegment INT
AS
BEGIN
    -- Insert data into Provider.AppointmentSlot
    INSERT INTO [Provider].[AppointmentSlot] ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment])
    VALUES (@ProviderID, @Year, @Month, @Day, @Hour, @QuarterHourSegment);
END;

