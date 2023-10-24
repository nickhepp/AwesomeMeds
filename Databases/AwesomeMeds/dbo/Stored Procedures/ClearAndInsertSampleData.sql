
CREATE PROCEDURE [dbo].[ClearAndInsertSampleData]
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete all records from Appointments table
    --DELETE FROM Appointments;

    -- Reset identity seed
    --DBCC CHECKIDENT ('Appointments', RESEED, 0);

    -- Delete all records from Client.Client and Provider.Provider tables
    DELETE FROM [Client].[Client];
    DELETE FROM [Provider].[Provider];

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

    PRINT 'Sample data inserted successfully.';
END;


