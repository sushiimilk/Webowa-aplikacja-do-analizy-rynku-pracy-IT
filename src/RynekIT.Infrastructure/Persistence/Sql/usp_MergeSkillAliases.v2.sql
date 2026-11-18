CREATE OR ALTER PROCEDURE dbo.usp_MergeSkillAliases
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Skill, ktorego nazwa jest aliasem innego skilla, to duplikat (np. 'node.js' przy aliasie node.js -> node).
    -- Wszystkie powiazania duplikatu sa przepinane na skill docelowy, a duplikat jest usuwany.
    -- Procedura jest idempotentna: gdy nie ma duplikatow, nic nie robi.
    -- Od v2: jesli alias wskazuje na skill, ktory sam jest duplikatem, idziemy dalej az do ostatniego skilla.
    -- Gdy aliasy tworza cykl (np. a -> b i b -> a), procedura konczy sie bledem, zeby niczego nie skasowac.

    DECLARE @dup TABLE (DupId int PRIMARY KEY, TargetId int NOT NULL);

    INSERT INTO @dup (DupId, TargetId)
    SELECT dup.Id, a.SkillId
    FROM dbo.Skills dup
    JOIN dbo.SkillAliases a ON a.Alias = dup.Name
    WHERE dup.Id <> a.SkillId;

    IF NOT EXISTS (SELECT 1 FROM @dup) RETURN;

    -- #0. jesli cel duplikatu tez jest duplikatem, bierzemy jego cel (i tak dalej).
    --     Bez cyklu wystarczy tyle krokow, ile jest duplikatow, wiec wiecej krokow oznacza cykl.
    DECLARE @steps int = 0;
    DECLARE @maxSteps int = (SELECT COUNT(*) FROM @dup);

    WHILE EXISTS (SELECT 1 FROM @dup d JOIN @dup d2 ON d2.DupId = d.TargetId)
    BEGIN
        IF @steps >= @maxSteps
        BEGIN
            DECLARE @cycle nvarchar(2048) = N'usp_MergeSkillAliases: aliasy tworza cykl, popraw dbo.SkillAliases. Dotyczy skilli: '
                + (SELECT STRING_AGG(s.Name, N', ') WITHIN GROUP (ORDER BY s.Name)
                   FROM @dup d
                   JOIN dbo.Skills s ON s.Id = d.DupId
                   WHERE EXISTS (SELECT 1 FROM @dup d2 WHERE d2.DupId = d.TargetId));
            THROW 50001, @cycle, 1;
        END;

        UPDATE d
        SET TargetId = d2.TargetId
        FROM @dup d
        JOIN @dup d2 ON d2.DupId = d.TargetId;

        SET @steps += 1;
    END;

    BEGIN TRANSACTION;

    -- #1. oferty
    INSERT INTO dbo.OfferSkills (OfferId, SkillId)
    SELECT DISTINCT os.OfferId, d.TargetId
    FROM dbo.OfferSkills os
    JOIN @dup d ON d.DupId = os.SkillId
    WHERE NOT EXISTS (SELECT 1 FROM dbo.OfferSkills x
                      WHERE x.OfferId = os.OfferId AND x.SkillId = d.TargetId);

    DELETE os
    FROM dbo.OfferSkills os
    JOIN @dup d ON d.DupId = os.SkillId;

    -- #2. CV
    INSERT INTO dbo.CvSkills (CvId, SkillId)
    SELECT DISTINCT cs.CvId, d.TargetId
    FROM dbo.CvSkills cs
    JOIN @dup d ON d.DupId = cs.SkillId
    WHERE NOT EXISTS (SELECT 1 FROM dbo.CvSkills x
                      WHERE x.CvId = cs.CvId AND x.SkillId = d.TargetId);

    DELETE cs
    FROM dbo.CvSkills cs
    JOIN @dup d ON d.DupId = cs.SkillId;

    -- #3. aliasy wskazujace na duplikat przechodza na ostatni skill w lancuchu
    UPDATE a
    SET SkillId = d.TargetId
    FROM dbo.SkillAliases a
    JOIN @dup d ON d.DupId = a.SkillId;

    -- #4. usuniecie duplikatow
    DELETE s
    FROM dbo.Skills s
    JOIN @dup d ON d.DupId = s.Id;

    COMMIT TRANSACTION;
END;
