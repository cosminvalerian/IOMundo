CREATE PROCEDURE SP_Offers_Save
(
    @ID INT,
    @CheckInDate DATETIME2(7),
    @StayDurationNights BIGINT,
    @PersonCombination NVARCHAR(MAX),
    @ServiceCode NVARCHAR(MAX),
    @Price DECIMAL(18, 2),
    @PricePerAdult DECIMAL(18, 2),
    @PricePerChild DECIMAL(18, 2),
    @StrikePrice DECIMAL(18, 2),
    @StrikePricePerAdult DECIMAL(18, 2),
    @StrikePricePerChild DECIMAL(18, 2),
    @ShowStrikePrice BIT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @LastUpdated DATETIME2(7) = SYSDATETIME();

    BEGIN TRANSACTION;
    
    UPDATE Offers WITH (ROWLOCK, UPDLOCK)
    SET LastUpdated = @LastUpdated
    WHERE ID = @ID;

    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO Offers (
            ID, CheckInDate, StayDurationNights, PersonCombination, ServiceCode,
            Price, PricePerAdult, PricePerChild, StrikePrice, StrikePricePerAdult,
            StrikePricePerChild, ShowStrikePrice, LastUpdated
        )
        VALUES (
            @ID, @CheckInDate, @StayDurationNights, @PersonCombination, @ServiceCode,
            @Price, @PricePerAdult, @PricePerChild, @StrikePrice, @StrikePricePerAdult,
            @StrikePricePerChild, @ShowStrikePrice, @LastUpdated
        );
    END
    ELSE
    BEGIN
        UPDATE Offers
        SET 
            CheckInDate = @CheckInDate,
            StayDurationNights = @StayDurationNights,
            PersonCombination = @PersonCombination,
            ServiceCode = @ServiceCode,
            Price = @Price,
            PricePerAdult = @PricePerAdult,
            PricePerChild = @PricePerChild,
            StrikePrice = @StrikePrice,
            StrikePricePerAdult = @StrikePricePerAdult,
            StrikePricePerChild = @StrikePricePerChild,
            ShowStrikePrice = @ShowStrikePrice
        WHERE ID = @ID AND LastUpdated = @LastUpdated;
    END

    COMMIT TRANSACTION;
END
