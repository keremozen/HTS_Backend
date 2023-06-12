--Nationalities Table
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Türkiye', '+90', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Avustralya', '+61', true,'"2023-03-26 22:49:04.678786+03"', false);
INSERT INTO "Nationalities"("Name", "PhoneCode", "IsActive", "CreationTime", "IsDeleted") VALUES ('Singapur', '+65', true,'"2023-03-26 22:49:04.678786+03"', false);

--Genders Table
INSERT INTO "Genders"("Id", "Name") VALUES (1, 'Kadın');
INSERT INTO "Genders"("Id", "Name") VALUES (2, 'Erkek');

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

--Patient Document Status
INSERT INTO "PatientDocumentStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "PatientDocumentStatuses"("Id", "Name") VALUES (2, 'İptal');

--Treatment Process Status
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (1, 'Yeni Kayıt');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (2, 'Hastanelere Danışıldı - Cevap Bekleniyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (3, 'Hastanelere Danışıldı - Değerlendirme Bekliyor');
INSERT INTO "TreatmentProcessStatuses"("Id", "Name") VALUES (4, 'Operasyon Onaylandı - Fiyatlandırma Bekliyor');

--Cities Table
INSERT INTO "Cities"("Name","CreationTime") VALUES ('Ankara','"2023-03-26 22:49:04.678786+03"');
INSERT INTO "Cities"("Name","CreationTime") VALUES ('İstanbul','"2023-03-26 22:49:04.678786+03"');

--Hospital Consultations Status
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (1, 'Cevap Bekleniyor');
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (2, 'Tedaviye Uygundur');
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (3, 'Tedaviye Uygun Değildir');
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (4, 'Tanı İçin Muayene Gerekli');
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (5, 'Operasyon Onaylandı');
INSERT INTO "HospitalConsultationStatuses"("Id", "Name") VALUES (6, 'Operasyon Reddedildi');

--Hospital response type Table
INSERT INTO "HospitalResponseTypes"("Id", "Name") VALUES (1, 'Tedaviye Uygundur');
INSERT INTO "HospitalResponseTypes"("Id", "Name") VALUES (2, 'Tedaviye Uygun Değildir');
INSERT INTO "HospitalResponseTypes"("Id", "Name") VALUES (3, 'Tanı İçin Muayene Gerekli');

--Hospitalization type Table
INSERT INTO "HospitalizationTypes"("Id", "Name") VALUES (1, 'Medikal Tedavi');
INSERT INTO "HospitalizationTypes"("Id", "Name") VALUES (2, 'Yatış');
INSERT INTO "HospitalizationTypes"("Id", "Name") VALUES (3, 'Cerrahi Yatış');

--Currency Table
INSERT INTO "Currencies"("Id", "Name") VALUES (1, 'TL');
INSERT INTO "Currencies"("Id", "Name") VALUES (2, 'USD');

--AdditionalService Table
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (1, 'Transfer hizmeti',false,false,false,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (2, 'Medikal İkinci Muayene',false,false,false,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (3, 'Tercümanlık',false,false,false,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (4, 'Koordinasyon Hizmeti',false,false,false,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (5, 'Servis Yatışı',true,false,true,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (6, 'Yoğun Bakım',true,false,false,false);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (7, 'Konaklama',true,false,false,true);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (8, 'Seyahat',false,false,false,true);
INSERT INTO "AdditionalServices"("Id", "Name", "Day","Piece","RoomType","Companion") VALUES (9, 'Fiziki Muayene',false,true,false,false);

--Operation type Table
INSERT INTO "OperationTypes"("Id", "Name") VALUES (1, 'Hastane danışma');
INSERT INTO "OperationTypes"("Id", "Name") VALUES (2, 'Elle giriş');

--Operation status Table
INSERT INTO "OperationStatuses"("Id", "Name") VALUES (1, 'Fiyatlandırma Bekleniyor');
INSERT INTO "OperationStatuses"("Id", "Name") VALUES (2, 'Proforma Oluşturuldu');

--Process type Table
INSERT INTO "ProcessTypes"("Id", "Name", "IsActive") VALUES (1, 'Sut Kodu', true);
INSERT INTO "ProcessTypes"("Id", "Name", "IsActive") VALUES (2, 'Sarf Malzeme', true);