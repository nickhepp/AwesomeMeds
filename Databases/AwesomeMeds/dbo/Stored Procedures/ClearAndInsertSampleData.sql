
CREATE PROCEDURE [dbo].[ClearAndInsertSampleData]
AS
BEGIN
    SET NOCOUNT ON;

    -- Do all the deletes first
    DELETE FROM [Client].[ConfirmedAppointmentSlot];
    DELETE FROM [Client].[PendingReservationAppointmentSlot];
    DELETE FROM [Provider].[AppointmentSlot];
    DELETE FROM [Provider].[Provider];
    DELETE FROM [Client].[Client];
    DELETE FROM [Scheduling].[QuarterHourSegmentLookup];


    -- Insert sample clients with easy-to-read GUIDs
    INSERT INTO Client.Client (ClientID)
    VALUES
        ('11111111-1111-1111-1111-111111111111'),
        ('22222222-2222-2222-2222-222222222222');

    -- Insert sample providers with easy-to-read GUIDs
    INSERT INTO Provider.Provider (ProviderID)
    VALUES
        ('33333333-3333-3333-3333-333333333333'),
        ('44444444-4444-4444-4444-444444444444');

    -- Seed the lookup data
    INSERT INTO [Scheduling].[QuarterHourSegmentLookup] ([SegmentID], [SegmentDescription])
    VALUES
        (0, 'First Quarter'),
        (1, 'Second Quarter'),
        (2, 'Third Quarter'),
        (3, 'Fourth Quarter');

    PRINT 'Sample data inserted successfully.';
END;


