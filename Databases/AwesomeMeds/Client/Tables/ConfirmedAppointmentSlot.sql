CREATE TABLE [Client].[ConfirmedAppointmentSlot] (
    -- the appointment data (PK), can only have 1 reservation against an appt
    [ProviderID] UNIQUEIDENTIFIER,
    [Year] INT,
    [Month] INT,
    [Day] INT,
    [Hour] INT,
    [QuarterHourSegment] INT,

    -- the client that has the appt
    [ClientID] UNIQUEIDENTIFIER,

    -- time the appointment was created so we can confirm in the allowed time window
    [ReservationCreatedUTC] DATETIME,
    -- the latest time that the reservation can be confirmed by
    [ReservationConfirmedByUTC] DATETIME,
    
    -- the time the reservation was confirmed
    [ReservationConfirmedUTC] DATETIME,

    CONSTRAINT PK_Confirmed_PendingReservationAppointmentSlot PRIMARY KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment]),
    CONSTRAINT FK_Confirmed_ClientID FOREIGN KEY ([ClientID]) REFERENCES Client.Client([ClientID]),
    CONSTRAINT FK_Confirmed_ProviderID FOREIGN KEY ([ProviderID]) REFERENCES Provider.AppointmentSlot([ProviderID])
);