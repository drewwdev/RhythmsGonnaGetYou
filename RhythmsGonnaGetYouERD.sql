createdb RecordCompany

CREATE TABLE "Album" ("Id" SERIAL PRIMARY KEY, "Title" TEXT, "IsExplicit" BOOLEAN, "ReleaseDate" DATE);

ALTER TABLE "Album" ADD COLUMN "BandId" INT NULL REFERENCES "Band" ("Id");

CREATE TABLE "Band" ("Id" SERIAL PRIMARY KEY, "Name" TEXT, "CountryOfOrigin" TEXT,
"NumberOfMembers" INT, "Website" TEXT, "Style" TEXT,
"IsSigned" BOOLEAN, "ContactName" TEXT, "ContactPhoneNumber" TEXT);

CREATE TABLE "Song" ("Id" SERIAL PRIMARY KEY, "TrackNumber" INT, "Title" TEXT, "Duration" TEXT;

ALTER TABLE "Song" ADD COLUMN "AlbumId" INT NULL REFERENCES "Album" ("Id");

// Add a new band
INSERT INTO "Band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('King Gizzard and the Lizard Wizard', 'Australia', 6, 'kinggizzardandthelizardwizard.com', 'Thrash metal', true, 'Michelle', '123-456-7890');

// View all bands
SELECT * FROM "Band";

// Add an album for a band
INSERT INTO "Album" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('Infest the Rats Nest', true, '2019-08-16');

// Add a song to an album
INSERT INTO "Song" ("TrackNumber", "Title", "Duration")
VALUES (2, 'Mars for the Rich', '4:12');

// Added more bands, albums and songs for examples
INSERT INTO "Band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Whitechapel', 'USA', 5, 'whitechapelband.com', 'Deathcore', true, 'Michael', '555-555-5555');
INSERT INTO "Album" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('The Valley', true, '2019-03-29');
INSERT INTO "Song" ("TrackNumber", "Title", "Duration")
VALUES (4, 'Hickory Creek', '4:57';
INSERT INTO "Band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('MF DOOM', 'UK', 1, 'gasdrawls.com', 'Hip hop', false, 'Daniel Dumile', '1-800-3666');
INSERT INTO "Album" ("Title", "IsExplicit", "ReleaseDate")
VALUES ('Madvillainy', true, '2004-03-23');
INSERT INTO "Song" ("TrackNumber", "Title", "Duration")
VALUES (22, 'Rhinestone Cowboy', '3:59');

// Add Id for tables that will be joined
UPDATE "Album" SET "BandId" = 1 WHERE "Id" IN (1);
UPDATE "Album" SET "BandId" = 2 WHERE "Id" IN (2);
UPDATE "Album" SET "BandId" = 3 WHERE "Id" IN (3);

UPDATE "Song" SET "AlbumId" = 1 WHERE "Id" IN (1);
UPDATE "Song" SET "AlbumId" = 2 WHERE "Id" IN (2);
UPDATE "Song" SET "AlbumId" = 3 WHERE "Id" IN (3);

// View all albums from their joined band
SELECT * FROM "Album" JOIN "Band" ON "Album"."BandId" = "Band"."Id";

// View all songs from their joined album
SELECT * FROM "Song" JOIN "Album" ON "Song"."AlbumId" = "Album"."Id";

// Let a band go (update isSigned to false)
UPDATE "Band" SET "IsSigned" = false WHERE "Band"."Name" = 'King Gizzard and the Lizard Wizard';

// Resign a band (update isSigned to true)
UPDATE "Band" SET "IsSigned" = true WHERE "Band"."Name" = 'King Gizzard and the Lizard Wizard';

// View all albums (and their associated songs) ordered by ReleaseDate
SELECT "Album"."Title", "Song"."Title", "Album"."ReleaseDate"
FROM "Album"
JOIN "Band" ON "Band"."Id" = "Album"."Id"
JOIN "Song" on "Song"."AlbumId" = "Band"."Id"
ORDER BY "ReleaseDate";

// View all bands that are signed
SELECT "Band"."Name" FROM "Band" WHERE "Band"."IsSigned" = true;

// View all bands that are not signed
SELECT "Band"."Name" FROM "Band" WHERE "Band"."IsSigned" = false;
