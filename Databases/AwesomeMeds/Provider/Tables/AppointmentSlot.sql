CREATE TABLE [Provider].[AppointmentSlot] (
    [ProviderID] UNIQUEIDENTIFIER NOT NULL,
    [Year] INT NOT NULL,
    [Month] INT NOT NULL,
    [Day] INT NOT NULL,
    [Hour] INT NOT NULL,
    [QuarterHourSegment] INT NOT NULL,
    CONSTRAINT PK_AppointmentSlot PRIMARY KEY ([ProviderID], [Year], [Month], [Day], [Hour], [QuarterHourSegment]),
    CONSTRAINT FK_ProviderID FOREIGN KEY (ProviderID) REFERENCES Provider.Provider(ProviderID),
    CONSTRAINT FK_QuarterHourSegment FOREIGN KEY ([QuarterHourSegment]) REFERENCES [Scheduling].[QuarterHourSegmentLookup]([SegmentID])
);
GO;
