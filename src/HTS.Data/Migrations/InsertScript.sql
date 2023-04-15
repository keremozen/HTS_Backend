--Nationalities Table
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Türkiye', '+90', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Avustralya', '+61', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Singapur', '+65', true,'"2023-03-26 22:49:04.678786+03"', false);

--Genders Table
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (1, 'Kadın', true);
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (2, 'Erkek', true);

--Languages Table
INSERT INTO "Languages"("Name", "Code", "IsActive", "CreationTime", "IsDeleted") VALUES ('Türkçe', 'TR', true,'"2023-03-26 22:49:04.678786+03"', false);

--Patient Note Status
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (2, 'İptal');

--Treatment Process Status
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (2, 'Hastanelere Danışıldı - Cevap Bekleniyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (3, 'Hastanelere Danışıldı - Değerlendirme Bekliyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (4, 'Operasyon Onaylandı - Fiyatlandırma Bekliyor');

--Cities Table
INSERT INTO "Cities"("Name") VALUES ('Ankara');
INSERT INTO "Cities"("Name") VALUES ('İstanbul');