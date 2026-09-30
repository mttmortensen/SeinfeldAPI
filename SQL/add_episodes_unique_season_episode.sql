-- PostgreSQL: only one episode per season + episode number.
-- Matches the HasIndex(...).IsUnique() in Data/SeinfeldDbContext.cs.
--
-- Check for existing duplicates first; the index will fail to create if any exist:
--   SELECT "Season", "EpisodeNumber", COUNT(*) FROM "Episodes"
--   GROUP BY "Season", "EpisodeNumber" HAVING COUNT(*) > 1;

CREATE UNIQUE INDEX IF NOT EXISTS "UX_Episodes_Season_EpisodeNumber"
    ON "Episodes" ("Season", "EpisodeNumber");
