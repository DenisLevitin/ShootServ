  INSERT INTO dbo.Roles VALUES ( 1, 'Организатор', 'Organization')
  INSERT INTO dbo.Roles VALUES ( 2, 'Стрелок', 'Shooter')

  INSERT INTO dbo.Countries VALUES (1, 'Russia', 'Россия')

  INSERT INTO dbo.Regions ( Name, FederationAddress, FederationTelefon, IdCountry ) VALUES ('Москва', '', '', 1)

  INSERT INTO dbo.WeaponTypes VALUES (1, 'Винтовка', 'Rifle')
  INSERT INTO dbo.WeaponTypes VALUES (2, 'Пистолет', 'Pistol')
  INSERT INTO dbo.WeaponTypes VALUES (3, 'Винтовка, движущаяся мишень', 'RifleMovingTarget')

  -- разряды
  INSERT INTO dbo.ShooterCategory VALUES (1, 'МСМК', 'MSMC', 1)
  INSERT INTO dbo.ShooterCategory VALUES (2, 'МС', 'MS', 2)
  INSERT INTO dbo.ShooterCategory VALUES (3, 'КМС', 'CMS', 3)
  INSERT INTO dbo.ShooterCategory VALUES (4, 'Первый разряд', 'First', 4)
  INSERT INTO dbo.ShooterCategory VALUES (5, 'Второй разряд', 'Second', 5)
  INSERT INTO dbo.ShooterCategory VALUES (6, 'Третий', 'Third', 6)

  -- упражнения
  INSERT INTO dbo.CompetitionType (Name, IdWeaponType, SeriesCount) VALUES ('ВП-4', 1, 4)
  INSERT INTO dbo.CompetitionType (Name, IdWeaponType, SeriesCount) VALUES ('ВП-6', 1, 6)
  INSERT INTO dbo.CompetitionType (Name, IdWeaponType, SeriesCount) VALUES ('ПП-2', 2, 4)
  INSERT INTO dbo.CompetitionType (Name, IdWeaponType, SeriesCount) VALUES ('ПП-2', 2, 6)

  -- типы соревнований
  INSERT INTO dbo.CupTypes VALUES (1, 'Городские', 'City')
  INSERT INTO dbo.CupTypes VALUES (2, 'Региональные', 'Region')
  INSERT INTO dbo.CupTypes VALUES (3, 'Всероссийские', 'Country')
  INSERT INTO dbo.CupTypes VALUES (4, 'Международные', 'World')

  -- Типы заявок
  INSERT INTO EntryStatus (Name, Keychar) VALUES ('Принята', 'Accepted')
  INSERT INTO EntryStatus (Name, Keychar) VALUES ('Отклонена', 'Denied')
  INSERT INTO EntryStatus (Name, Keychar) VALUES ('Ожидает рассмотрения', 'Waiting')

