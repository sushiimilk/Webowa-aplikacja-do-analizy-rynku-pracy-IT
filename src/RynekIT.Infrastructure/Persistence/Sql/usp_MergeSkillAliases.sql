CREATE OR ALTER PROCEDURE dbo.usp_MergeSkillAliases
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Skill, ktorego nazwa jest aliasem innego skilla, to duplikat (np. 'node.js' przy aliasie node.js -> node).
    -- Wszystkie powiazania duplikatu sa przepinane na skill docelowy, a duplikat jest usuwany.
    -- Procedura jest idempotentna: gdy nie ma duplikatow, nic nie robi.

    DECLARE @dup TABLE (DupId int PRIMARY KEY, TargetId int NOT NULL);

    INSERT INTO @dup (DupId, TargetId)
    SELECT dup.Id, a.SkillId
    FROM dbo.Skills dup
    JOIN dbo.SkillAliases a ON a.Alias = dup.Name
    WHERE dup.Id <> a.SkillId;

    IF NOT EXISTS (SELECT 1 FROM @dup) RETURN;

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

    -- #3. aliasy wskazujace na duplikat przechodza na skill docelowy
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
