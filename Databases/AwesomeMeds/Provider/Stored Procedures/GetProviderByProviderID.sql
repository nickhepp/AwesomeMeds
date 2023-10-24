CREATE PROCEDURE [Provider].[GetProviderByProviderID]
    @ProviderID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ProviderID
    FROM
        [Provider].[Provider]
    WHERE
        ProviderID = @ProviderID;
END;

