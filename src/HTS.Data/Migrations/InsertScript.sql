--Nationalities Table
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Türkiye', '+90', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Avustralya', '+61', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Singapur', '+65', true,'"2023-03-26 22:49:04.678786+03"', false);

--Genders Table
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (1, 'Kadın', true);
INSERT INTO "Genders"("Id", "Name", "IsActive") VALUES (2, 'Erkek', true);

--Languages Table
INSERT INTO "Languages"("Name", "IsActive", "CreationTime", "IsDeleted") VALUES ('Türkçe', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Languages"("Name", "IsActive", "CreationTime", "IsDeleted") VALUES ('İngilizce', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Languages"("Name", "IsActive", "CreationTime", "IsDeleted") VALUES ('Almanca', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Languages"("Name", "IsActive", "CreationTime", "IsDeleted") VALUES ('Arapça', true,'"2023-03-26 22:49:04.678786+03"', false);

--Document Types
INSERT INTO "DocumentTypes"("Name", "Description", "IsActive", "CreationTime") VALUES ('Radyoloji Görüntüleri', 'MR, Röntgen, Tomografi vb görüntüleri', true, '2023-03-26 22:49:04.678786+03');
INSERT INTO "DocumentTypes"("Name", "Description", "IsActive", "CreationTime") VALUES ('Kan Tahlilleri', 'Kan tahlilleri', true, '2023-03-26 22:49:04.678786+03');

--Patient Note Status
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "PatientNoteStatuses"("Id", "Name") VALUES (2, 'İptal');

--Treatment Process Status
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (2, 'Hastanelere Danışıldı - Cevap Bekleniyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (3, 'Hastanelere Danışıldı - Değerlendirme Bekliyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (4, 'Operasyon Onaylandı - Fiyatlandırma Bekliyor');

--Cities Table
INSERT INTO "Cities"("Name","CreationTime") VALUES ('Ankara','"2023-03-26 22:49:04.678786+03"');
INSERT INTO "Cities"("Name","CreationTime") VALUES ('İstanbul','"2023-03-26 22:49:04.678786+03"');