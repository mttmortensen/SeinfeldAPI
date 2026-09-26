-- Adds the Role column to an existing "Users" table (fresh installs get it from postgres_schema.sql).
-- Every existing and newly registered user is a plain 'User'; only 'Admin' can create, update or delete
-- episodes and quotes. Promote an account by hand:
--   UPDATE "Users" SET "Role" = 'Admin' WHERE "Username" = 'your_username';
-- The user must log in again afterwards, since the role is baked into the JWT.

ALTER TABLE "Users" ADD COLUMN "Role" VARCHAR(20) NOT NULL DEFAULT 'User';
