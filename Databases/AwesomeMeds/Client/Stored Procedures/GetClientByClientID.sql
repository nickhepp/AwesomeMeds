CREATE PROCEDURE [Client].[GetClientByClientID]
    @ClientID UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [ClientID]
    FROM
        [Client].[Client]
    WHERE
        [ClientID] = @ClientID;
END;
