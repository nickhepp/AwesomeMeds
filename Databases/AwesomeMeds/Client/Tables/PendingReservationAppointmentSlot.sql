CREATE TABLE [Client].[PendingReservationAppointmentSlot] (
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

    CONSTRAINT PK_PendingReservationAppointmentSlot PRIMARY KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment]),
    CONSTRAINT FK_PendingReservationAppointmentSlot_ClientID FOREIGN KEY ([ClientID]) REFERENCES Client.Client([ClientID]),
    CONSTRAINT FK_PendingReservationAppointmentSlot_ProviderID_Year_Month_Day_Hour_QuarterHourSegment FOREIGN KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment])
        REFERENCES [Provider].[AppointmentSlot] ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment])
);