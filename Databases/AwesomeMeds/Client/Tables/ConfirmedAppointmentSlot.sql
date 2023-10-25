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

    -- a provider's time slot can only be confirmed once
    CONSTRAINT PK_ConfirmedReservationAppointmentSlot PRIMARY KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment]),

    -- need a valid client
    CONSTRAINT FK_ConfirmedReservationAppointmentSlot_ClientID FOREIGN KEY ([ClientID]) REFERENCES Client.Client([ClientID]),

    -- can only confirm a pending reservation
    CONSTRAINT FK_ConfirmedPendingReservationAppointmentSlot_ProviderID_Year_Month_Day_Hour_QuarterHourSegment FOREIGN KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment])
        REFERENCES [Client].[PendingReservationAppointmentSlot] ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment]),

    -- a client cannot confirm multiple appointments during the same time
    CONSTRAINT UQ_ConfirmedReservationAppointmentSlot_Time UNIQUE ([ClientID], [Year], [Month], [Day], [Hour], [QuarterHourSegment])

);
