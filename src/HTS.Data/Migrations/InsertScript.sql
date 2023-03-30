--Nationalities Table
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Turkey', '+90', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Australia', '+61', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Singapure', '+65', true,'"2023-03-26 22:49:04.678786+03"', false);

--Genders Table
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (1, 'Women', true);
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (2, 'Men', true);

--Languages Table
INSERT INTO "Languages"("Name", "Code", "IsActive", "CreationTime", "IsDeleted") VALUES ('Turkish', 'TR', true,'"2023-03-26 22:49:04.678786+03"', false);

--Patient Note Status
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (1, 'New Record');
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (2, 'Revoked');
